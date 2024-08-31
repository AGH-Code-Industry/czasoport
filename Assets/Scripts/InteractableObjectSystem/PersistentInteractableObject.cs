using System;
using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using UnityEngine;
using Utils;

namespace InteractableObjectSystem {
    public abstract class PersistentInteractableObject : InteractableObject, IPersistentObject {
        public bool SceneObject { get; } = true;
        [field: SerializeField] public SerializableGuid ID { get; set; }

        public abstract void LoadPersistentData(GameData gameData);
        public abstract void SavePersistentData(ref GameData gameData);
    }
}