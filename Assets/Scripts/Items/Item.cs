using System;
using UnityEngine;
using Interactions.Interfaces;

public class Item : MonoBehaviour, IInteractableHand, ILongInteractableHand
{
    //[SerializeField] private ItemSO _itemSO;

    public void ToogleHighlight() {
        throw new NotImplementedException();
    }
    
    public void InteractionHand() {
        throw new NotImplementedException();
    }
    
    public void LongInteractionHand() {
        throw new NotImplementedException();
    }
}
