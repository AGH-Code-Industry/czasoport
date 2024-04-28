using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using UnityEngine;
using Utils;

namespace DataPersistence {
    public interface IPersistentObject : IDataPersistence
    {
        public SerializableGuid ID { get; set; }
    }
}
