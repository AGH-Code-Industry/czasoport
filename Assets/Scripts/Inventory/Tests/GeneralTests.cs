using System;
using CoinPackage.Debugging;
using Items;
using UnityEngine;

namespace Inventory.Tests {
    public class GeneralTests : MonoBehaviour {
        public void Start() {
            Item item;
            CDebug.Log(Inventory.Instance.GetSelectedItem(out item));
        }
    }
}