using System;
using CoinPackage.Debugging;
using Items;
using UnityEngine;

namespace InventorySystem.Tests {
    public class InventoryGeneralTests : MonoBehaviour {
        public void Start() {
            Item item;
            CDebug.Log(Inventory.Instance.GetSelectedItem(out item));
        }
    }
}