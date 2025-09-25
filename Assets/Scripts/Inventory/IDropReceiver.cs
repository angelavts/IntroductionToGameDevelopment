using UnityEngine;

public interface IDropReceiver
{
    bool TryEquip(InventoryItemUI item);
    Transform GetSnapTransform();
    void OnEquipVisual(InventoryItemUI item);
}