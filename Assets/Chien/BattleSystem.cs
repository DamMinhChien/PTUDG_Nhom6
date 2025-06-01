using System.Collections;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] GameObject dialogAttack;
    PlayerMovement movement;

    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;

    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;

    [SerializeField] BattleDialogBox battleDialogBox;

    private void Start()
    {
        SetupBattle();
        state = BattleState.PlayerAction;
    }

    public void SetupBattle()
    {
        playerUnit.Setup();
        Debug.Log("Player Pokemon: " + (playerUnit.Pokemon == null ? "null" : playerUnit.Pokemon.Base.Name));
        playerHud.SetData(playerUnit.Pokemon);

        enemyUnit.Setup();
        enemyHud.SetData(enemyUnit.Pokemon);

        battleDialogBox.SetMoveNames(playerUnit.Pokemon);
        battleDialogBox.Init(this); // G�n BattleSystem cho DialogBox

    }

    public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }
    BattleState state;
    void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            battleDialogBox.HandleMoveSelectionInput();
        }
    }

    public void Run()
    {
        gameObject.SetActive(false);
        movement.isEnableMove = true;
    }

    public void Attack()
    {
        var attackDetails = transform.Find("DialogAttack");
        if (attackDetails != null)
        {
            bool isActive = attackDetails.gameObject.activeSelf;
            attackDetails.gameObject.SetActive(!isActive); 
        }
    }
    

    public void OnMoveSelected(int moveIndex)
    {
        currentMove = moveIndex;

        // ?n khung ch?n chi�u
        if (dialogAttack != null)
            dialogAttack.SetActive(false);

        state = BattleState.Busy;
        StartCoroutine(PerformPlayerMove());
    }

    int currentMove;
    IEnumerator PerformPlayerMove()
    {
        var move = playerUnit.Pokemon.Moves[currentMove];

        // G�y s�t th??ng v� ki?m tra ??i th? c� b? h? g?c kh�ng
        bool isFainted = enemyUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon, enemyUnit.Pokemon);
        enemyHud.UpdateHP();

        // N?u ??i th? b? h? g?c th� d?ng l?i
        if (isFainted)
            yield break;

        // N?u kh�ng, cho ??i th? t?n c�ng l?i
        StartCoroutine(EnemyMove());
    }

    IEnumerator EnemyMove()
    {
        yield return new WaitForSeconds(1f); // Ch? 1 gi�y ?? t?o c?m gi�c c� th?i gian

        // Ch?n chi�u ng?u nhi�n t? enemy
        int randomMoveIndex = Random.Range(0, enemyUnit.Pokemon.Moves.Count);
        var move = enemyUnit.Pokemon.Moves[randomMoveIndex];

        // G�y s�t th??ng l�n ng??i ch?i
        bool isFainted = playerUnit.Pokemon.TakeDamage(move, enemyUnit.Pokemon, playerUnit.Pokemon);
        playerHud.UpdateHP() ;

        Debug.Log($"??ch d�ng chi�u: {move.Base.MoveName}");

        // N?u ng??i ch?i b? h? g?c th� k?t th�c
        if (isFainted)
        {
            Debug.Log("Ng??i ch?i thua cu?c!");
            yield break;
        }

        // N?u kh�ng th� quay l?i l??t ng??i ch?i
        state = BattleState.PlayerAction;
    }


}
