using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemData> items = new List<ItemData>();
    private Dictionary<string, ItemData> byId;

    private void OnEnable() => RebuildIndex();

    public void RebuildIndex()
    {
        byId = new Dictionary<string, ItemData>();
        foreach (var it in items)
        {
            if (it == null) continue;
            if (!byId.TryAdd(it.Id, it))
                Debug.LogWarning($"ID duplicado en ItemDatabase '{name}': {it.Id}");
        }
    }

    public ItemData GetById(string id)
        => (!string.IsNullOrEmpty(id) && byId != null && byId.TryGetValue(id, out var item)) ? item : null;

    public IEnumerable<ItemData> GetByType(ItemType type)
        => items.Where(i => i != null && i.type == type);

    public IReadOnlyList<ItemData> All => items;
}