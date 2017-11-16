
namespace FilesWatcher
{
    public interface ICustomWatcher
    {
        void SubscribeToChanges();
        void UnSubscribe();
    }
}
