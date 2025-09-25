using UnityEngine;

public enum ItemType { Consumable, Equipment, Material, Quest }

[CreateAssetMenu(fileName = "Item_", menuName = "Game/Inventory/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string id;               // ID único y estable
    public string Id => id;

    [Header("Visual")]
    public string displayName;
    [TextArea] public string description;
    public Sprite icon;                               // Para la UI
    public GameObject worldPrefab;                    // (Opcional) Prefab para el mundo

    [Header("Cantidad")]
    public int quantity = 1;

    [Header("Clasificación")]
    public ItemType type = ItemType.Material;

}