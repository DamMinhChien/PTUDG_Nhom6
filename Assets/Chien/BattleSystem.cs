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
        battleDialogBox.Init(this); // Gán BattleSystem cho DialogBox

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

        // ?n khung ch?n chiêu
        if (dialogAttack != null)
            dialogAttack.SetActive(false);

        state = BattleState.Busy;
        StartCoroutine(PerformPlayerMove());
    }

    int currentMove;
    IEnumerator PerformPlayerMove()
    {
        var move = playerUnit.Pokemon.Moves[currentMove];

        // Gây sát th??ng và ki?m tra ??i th? có b? h? g?c không
        bool isFainted = enemyUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon, enemyUnit.Pokemon);
        enemyHud.UpdateHP();

        // N?u ??i th? b? h? g?c thì d?ng l?i
        if (isFainted)
            yield break;

        // N?u không, cho ??i th? t?n công l?i
        StartCoroutine(EnemyMove());
    }

    IEnumerator EnemyMove()
    {
        yield return new WaitForSeconds(1f); // Ch? 1 giây ?? t?o c?m giác có th?i gian

        // Ch?n chiêu ng?u nhiên t? enemy
        int randomMoveIndex = Random.Range(0, enemyUnit.Pokemon.Moves.Count);
        var move = enemyUnit.Pokemon.Moves[randomMoveIndex];

        // Gây sát th??ng lên ng??i ch?i
        bool isFainted = playerUnit.Pokemon.TakeDamage(move, enemyUnit.Pokemon, playerUnit.Pokemon);
        playerHud.UpdateHP() ;

        Debug.Log($"??ch dùng chiêu: {move.Base.MoveName}");

        // N?u ng??i ch?i b? h? g?c thì k?t thúc
        if (isFainted)
        {
            Debug.Log("Ng??i ch?i thua cu?c!");
            yield break;
        }

        // N?u không thì quay l?i l??t ng??i ch?i
        state = BattleState.PlayerAction;
    }


}
