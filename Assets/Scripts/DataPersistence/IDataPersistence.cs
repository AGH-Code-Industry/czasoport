namespace DataPersistence {
    public interface IDataPersistence {
        public bool SceneObject { get; }

        public void LoadPersistentData(GameData gameData);
        public void SavePersistentData(ref GameData gameData);
    }
}