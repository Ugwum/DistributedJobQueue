namespace DistributedJobQueue
{
    public interface IDistributedJobQueue<TJob> where TJob : TaskJob
    {
        void AddWorkerNode(string node);
        TJob DequeueJob(string workerNode);
        void EnqueueJob(TJob job);
        void MarkJobAsFailed(string workerNode, TJob job);
        void RemoveWorkerNode(string node);
    }
}