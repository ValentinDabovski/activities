namespace SyncWorker;

public interface ISync
{
    Task Sync(CancellationToken cancellationToken);
}