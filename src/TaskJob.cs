namespace DistributedJobQueue
{
    public class TaskJob : ITaskJob
    {
        public string JobId { get; set; } = Guid.NewGuid().ToString();
    }
}