using System;
using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using UnityEngine;
using Utils;

namespace InteractableObjectSystem {
    public abstract class PersistentInteractableObject : InteractableObject, IDataPersistence {
        [SerializeField] protected SerializableGuid id = Guid.NewGuid();

        public abstract void LoadPersistentData(GameData gameData);
        public abstract void SavePersistentData(ref GameData gameData);
    }
}

