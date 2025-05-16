using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string id;
    public string name;
    public string description;
    public int price;
    public string currency;
    public string icon;
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
