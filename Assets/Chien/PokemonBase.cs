using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] string id;
    [SerializeField] string name;
    [SerializeField] int level;

    [TextArea]
    [SerializeField] string description;
    [SerializeField] Sprite sprite;
    [SerializeField] PokemonTypes type;

    // Base Stats
    [SerializeField] int maxHp;
    [SerializeField] int maxExp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int lucky;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;
    public string Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int Level { get => level; set => level = value; }
    public string Description { get => description; set => description = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public PokemonTypes Type { get => type; set => type = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Lucky { get => lucky; set => lucky = value; }
    public int Speed { get => speed; set => speed = value; }
    public List<LearnableMove> LearnableMoves { get => learnableMoves;}
    public int MaxExp { get => maxExp; set => maxExp = value; }
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base { get { return moveBase; } }
    public int Level { get { return level; } }
}

public enum PokemonTypes
{
    Grass,
    Water,
    Fire,
    Earth,
    Electric,
    Ghost,
    Flying
}
