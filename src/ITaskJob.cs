namespace DistributedJobQueue
{
    public interface ITaskJob
    {
        string JobId { get; set; }
    }
}