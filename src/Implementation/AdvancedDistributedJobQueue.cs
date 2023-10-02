using ConsistentHashing;

namespace DistributedJobQueue.Implementation
{
    public class AdvancedDistributedJobQueue<TJob> : IDistributedJobQueue<TJob> where TJob : TaskJob
    {
        private readonly List<string> workerNodes = new List<string>();
        private readonly Dictionary<string, Queue<TJob>> jobQueues = new Dictionary<string, Queue<TJob>>();
        private readonly ConsistentHash<string> hashRing = new ConsistentHash<string>();

        // Retry settings for failed jobs
        private readonly Dictionary<string, int> jobRetries = new Dictionary<string, int>();
        private readonly int maxRetries = 3;

        public void AddWorkerNode(string node)
        {
            workerNodes.Add(node);
            hashRing.AddNode(node);
            jobQueues[node] = new Queue<TJob>();
        }

        public void RemoveWorkerNode(string node)
        {
            workerNodes.Remove(node);
            hashRing.RemoveNode(node);

            // Redistribute jobs from the removed node
            RedistributeJobs(node);
        }

        public void EnqueueJob(TJob job)
        {
            if (workerNodes.Count == 0)
            {
                throw new InvalidOperationException("No worker nodes available to enqueue the job.");
            }

            string node = hashRing.GetNode(job.JobId.ToString());
            jobQueues[node].Enqueue(job);
        }

        public TJob DequeueJob(string workerNode)
        {
            if (jobQueues.ContainsKey(workerNode) && jobQueues[workerNode].Count > 0)
            {
                return jobQueues[workerNode].Dequeue();
            }
            throw new InvalidOperationException($"No jobs available for worker node: {workerNode}");
        }

        // Add a method to redistribute jobs from a lost node
        private void RedistributeJobs(string lostNode)
        {
            if (jobQueues.ContainsKey(lostNode))
            {
                var lostNodeJobs = jobQueues[lostNode].ToList();
                jobQueues.Remove(lostNode);

                foreach (var job in lostNodeJobs)
                {
                    string newNode = hashRing.GetNode(job.JobId.ToString());
                    if (!jobQueues.ContainsKey(newNode))
                    {
                        jobQueues[newNode] = new Queue<TJob>();
                    }
                    jobQueues[newNode].Enqueue(job);
                }
            }
        }

        // Mark a job as failed and handle retries
        public void MarkJobAsFailed(string workerNode, TJob job)
        {
            if (jobRetries.ContainsKey(job.JobId.ToString()))
            {
                jobRetries[job.JobId.ToString()]++;
                if (jobRetries[job.JobId.ToString()] <= maxRetries)
                {
                    // Retry the job
                    EnqueueJob(job);
                    return;
                }
            }
            // Max retries reached, job failed
            Console.WriteLine($"Job failed after {maxRetries} retries: {job}");
        }
    }
}