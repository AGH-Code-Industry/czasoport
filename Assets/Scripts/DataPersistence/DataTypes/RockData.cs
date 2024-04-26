using System;
using InteractableObjectSystem.Objects;

namespace DataPersistence.DataTypes {
    [Serializable]
    public class RockData : Data {
        public RockState state;
    }
}