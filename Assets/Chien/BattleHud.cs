using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Bar hpBar;
    public Bar expBar;

    private _Pokemon pokemon;

    public void SetData(_Pokemon pokemon)
    {
        this.pokemon = pokemon;
        nameText.text = pokemon.Base.Name;
        levelText.text = "Lv " + pokemon.Base.Level;
        hpBar.UpdateHpBar(pokemon.HP, pokemon.MaxHP);
        expBar.UpdateHpBar(pokemon.Exp, pokemon.MaxExp);
    }

    public void UpdateHP()
    {
        hpBar.UpdateHpBar(pokemon.HP, pokemon.MaxHP);
    }

}
