using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string id;
    public string name;
    public string description;
    public int price;
    public string currency;
    public string icon;

    [System.NonSerialized]
    public Sprite iconSprite;
}

[System.Serializable]
public class Item
{
    public ItemData data;
    public int quantity;

    public Item(ItemData data, int quantity)
    {
        this.data = data;
        this.quantity = quantity;
    }
}

[System.Serializable]
public class Skill
{
    public string id;
    public string name;
    public string description;
    public float power;
    public int level;
}

[System.Serializable]
public class PokemonType
{
    public string id;
    public string name;
    public string icon;
    public List<Skill> skills;
}

[System.Serializable]
public class Pokemon
{
    public string id;
    public string name;
    public string description;
    public string type;
    public int level;
    public int max_level = 40;
    public int hp;
    public int attack;
    public int defense;
    public int lucky;
}

[System.Serializable]
public class GameData
{
    public List<Item> items;
    public List<PokemonType> pokemon_types;
    public List<Pokemon> pokemon;
}

[System.Serializable]
public class ItemDataList
{
    public List<ItemData> items;
}

public class GameDataLoader : MonoBehaviour
{
    public static GameDataLoader Instance;

    private Dictionary<string, ItemData> itemDataDict = new Dictionary<string, ItemData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadItemData();
    }

    private void LoadItemData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("items");
        if (jsonFile == null)
        {
            Debug.LogError("❌ Không tìm thấy file items.json trong Resources!");
            return;
        }

        ItemDataList itemList = JsonUtility.FromJson<ItemDataList>(jsonFile.text);
        if (itemList == null || itemList.items == null)
        {
            Debug.LogError("❌ Không thể parse danh sách ItemData từ JSON!");
            return;
        }

        foreach (var item in itemList.items)
        {
            // Load sprite nếu có
            if (!string.IsNullOrEmpty(item.icon))
            {
                item.iconSprite = Resources.Load<Sprite>("Icons/" + item.icon);
                if (item.iconSprite == null)
                    Debug.LogWarning($"⚠ Không tìm thấy icon sprite: {item.icon}");
            }

            itemDataDict[item.id] = item;
        }

        Debug.Log($"✅ Đã load {itemDataDict.Count} ItemData.");
    }

    public ItemData GetItemDataById(string id)
    {
        if (itemDataDict.TryGetValue(id, out var item))
            return item;
        return null;
    }
}