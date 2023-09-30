namespace DataPersistence {
    public interface IDataPersistence {
        public void LoadPersistentData(GameData gameData);
        public void SavePersistentData(ref GameData gameData);
    }
}