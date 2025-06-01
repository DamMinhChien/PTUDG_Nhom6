using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    public Transform moveButtonContainer; // Đối tượng để chứa các button sinh ra
    public GameObject moveButtonPrefab;   // Prefab của nút kỹ năng

    public TextMeshProUGUI infoText;
    public TextMeshProUGUI ppText;

    private int currentSelection = 0;
    private _Pokemon pokemon;
    private List<Button> moveButtons = new List<Button>();

    public void SetMoveNames(_Pokemon pokemon)
    {
        this.pokemon = pokemon;

        // Xóa các button cũ nếu có
        foreach (var btn in moveButtons)
        {
            Destroy(btn.gameObject);
        }
        moveButtons.Clear();

        // Tạo các button mới, kiểm tra null
        for (int i = 0; i < pokemon.Moves.Count; i++)
        {

            var move = pokemon.Moves[i];

            var newButtonGO = Instantiate(moveButtonPrefab, moveButtonContainer);

            var txt = newButtonGO.GetComponentInChildren<Text>();

            txt.text = move.Base.MoveName;

            Button button = newButtonGO.GetComponent<Button>();

            moveButtons.Add(button);
        }


        UpdateMoveSelection(0);
    }


    public void HandleMoveSelectionInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            int newIndex = (currentSelection - 1 + moveButtons.Count) % moveButtons.Count;
            UpdateMoveSelection(newIndex);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            int newIndex = (currentSelection + 1) % moveButtons.Count;
            UpdateMoveSelection(newIndex);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnMoveChosen(currentSelection);
        }
    }

    void UpdateMoveSelection(int newIndex)
    {
        currentSelection = newIndex;

        for (int i = 0; i < moveButtons.Count; i++)
        {
            ColorBlock cb = moveButtons[i].colors;
            cb.normalColor = (i == newIndex) ? Color.yellow : Color.white;
            moveButtons[i].colors = cb;
        }

        var move = pokemon.Moves[currentSelection];
        infoText.text = move.Base.Description;
        ppText.text = $"{move.PP} / {move.Base.PP}";
    }

    public void OnMoveChosen(int index)
    {
        Debug.Log("Chiêu được chọn: " + pokemon.Moves[index].Base.MoveName);
        battleSystem.OnMoveSelected(index);
        gameObject.SetActive(false);
    }


    private BattleSystem battleSystem;

    public void Init(BattleSystem system)
    {
        battleSystem = system;
    }

}
