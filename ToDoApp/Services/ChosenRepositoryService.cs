using ToDoApp.Enums;

namespace ToDoApp.Services
{
    public class ChosenRepositoryService
    {
        public StorageType StorageType { get; private set; } = StorageType.Sql;

        public void SetStorageType(StorageType newStorageType) 
        {
            StorageType = newStorageType;
        }
    }
}
