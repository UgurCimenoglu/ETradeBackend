namespace ETradeBackend.Application.Abstracts.Storage
{
    public interface IStorageService : IStorage
    {
        public string StorageName { get; }
    }
}
