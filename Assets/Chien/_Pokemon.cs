using System.Collections.Generic;
using UnityEngine;

public class _Pokemon 
{
    public PokemonBase Base { get; set; }
    public int Level{ get; set; }
    public int HP { get; set; }
    public int Exp { get; set; }
    public List<Move> Moves { get; set; }
    public _Pokemon(PokemonBase pokemonBase, int level)
    {
        this.Base = pokemonBase;
        this.Level = level;
        HP = MaxHP;
        Exp = MaxExp;

        // Generate Moves
        Moves = new List<Move>();
        foreach (var move in pokemonBase.LearnableMoves)
        {
            if (level >= move.Level)
                Moves.Add(new Move(move.Base));
            if (Moves.Count >= 4)
                break;
        }
    }

    public Bar HpBar;


    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    }

    public int MaxHP
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; }
    }
    public int MaxExp
    {
        get { return Mathf.FloorToInt((Base.MaxExp * Level) / 100f) + 10; }
    }

    public bool TakeDamage(Move move, _Pokemon attacker, _Pokemon defender)
    {
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / defender.Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;

        if (HP < 0)
        {
            HP = 0;
            return true;
        }
        return false;
    }

}
