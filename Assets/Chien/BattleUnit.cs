using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;

    public _Pokemon Pokemon { get; set; }

    public void Setup()
    {
        Pokemon = new _Pokemon(_base, level);
    }
}
