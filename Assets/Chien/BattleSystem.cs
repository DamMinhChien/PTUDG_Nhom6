using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Đảm bảo đã import DOTween

public class BattleSystem : MonoBehaviour
{
    [SerializeField] GameObject dialogAttack;
    //PlayerMovement movement;

    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;

    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;

    [SerializeField] BattleDialogBox battleDialogBox;

    [SerializeField] private GameController gameController;

    [SerializeField] private GameObject pokeballPrefab; // Thêm prefab Pokéball

    public SaveDataManager saveManager;

    private void Start()
    {
        SetupBattle();
        state = BattleState.PlayerAction;
    }

    public void HandleUpdate()
    {

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
        if (gameController != null)
            gameController.EndBattle();
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

        playerUnit.PlayAttackAnimation(enemyUnit.transform.position);
        yield return new WaitForSeconds(0.4f);

        bool isFainted = enemyUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon, enemyUnit.Pokemon);
        enemyHud.UpdateHP();

        enemyUnit.PlayDefendAnimation();
        yield return new WaitForSeconds(0.3f);

        if (isFainted)
        {
            enemyUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(0.5f);
            if (gameController != null)
            {
                Debug.Log("Thoát!");
                gameController.EndBattle();
            }
                
            yield break;
        }

        StartCoroutine(EnemyMove());
    }

    IEnumerator EnemyMove()
    {
        yield return new WaitForSeconds(1f); // Ch? 1 giây ?? t?o c?m giác có th?i gian

        // Ch?n chiêu ng?u nhiên t? enemy
        int randomMoveIndex = Random.Range(0, enemyUnit.Pokemon.Moves.Count);
        var move = enemyUnit.Pokemon.Moves[randomMoveIndex];

        // Hi?u ?ng t?n công c?a ??i th?
        enemyUnit.PlayAttackAnimation(playerUnit.transform.position);
        yield return new WaitForSeconds(0.4f);

        // Gây sát th??ng lên ng??i ch?i
        bool isFainted = playerUnit.Pokemon.TakeDamage(move, enemyUnit.Pokemon, playerUnit.Pokemon);
        playerHud.UpdateHP();

        // Hi?u ?ng phòng th? cho ng??i ch?i
        playerUnit.PlayDefendAnimation();
        yield return new WaitForSeconds(0.3f);

        Debug.Log($"??ch dùng chiêu: {move.Base.MoveName}");

        // N?u ng??i ch?i b? h? g?c thì k?t thúc
        if (isFainted)
        {
            playerUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Ng??i ch?i thua cu?c!");
            yield break;
        }

        // N?u không thì quay l?i l??t ng??i ch?i
        state = BattleState.PlayerAction;
    }

    public void TryCatchPokemon()
    {
        state = BattleState.Busy;
        StartCoroutine(CatchPokemonCoroutine());
    }

    IEnumerator CatchPokemonCoroutine()
    {
        // Hiệu ứng ném Pokéball
        GameObject pokeball = Instantiate(pokeballPrefab, playerUnit.transform.position, Quaternion.identity);
        pokeball.transform.DOMove(enemyUnit.transform.position, 0.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.5f);

        // Tính tỉ lệ bắt dựa trên HP
        float catchRate = CalculateCatchRate(enemyUnit.Pokemon);
        bool isCaught = Random.value < catchRate;

        Vector3 originalEnemyScale = enemyUnit.transform.localScale;
        Vector3 originalEnemyPos = enemyUnit.transform.position;

        if (isCaught)
        {
            // Hiệu ứng: Pokémon bị hút vào Pokéball rồi biến mất
            Sequence catchSeq = DOTween.Sequence();
            catchSeq.Append(enemyUnit.transform.DOScale(0.1f, 0.4f).SetEase(Ease.InBack));
            catchSeq.Join(enemyUnit.transform.DOMove(pokeball.transform.position, 0.4f).SetEase(Ease.InBack));
            yield return catchSeq.WaitForCompletion();

            enemyUnit.gameObject.SetActive(false); // Ẩn Pokémon địch
            pokeball.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack); // Quả cầu biến mất
            yield return new WaitForSeconds(0.3f);

            Destroy(pokeball);

            // Lưu Pokémon
            AddPokemonToPlayer(enemyUnit.Pokemon);

            // Thoát chiến đấu
            if (gameController != null)
                gameController.EndBattle();
        }
        else
        {
            // Hiệu ứng thất bại: Pokéball rơi xuống, rung lắc, rồi biến mất
            float dropDistance = 0.5f;
            Vector3 dropTarget = pokeball.transform.position + Vector3.down * dropDistance;

            Sequence failSeq = DOTween.Sequence();
            failSeq.Append(pokeball.transform.DOMove(dropTarget, 0.2f).SetEase(Ease.InQuad));
            failSeq.Append(pokeball.transform.DOShakePosition(0.4f, 0.2f, 10, 90, false, true));
            failSeq.Append(pokeball.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
            yield return failSeq.WaitForCompletion();

            Destroy(pokeball);

            // Phục hồi lại hình ảnh Pokémon địch
            enemyUnit.gameObject.SetActive(true);
            enemyUnit.transform.localScale = originalEnemyScale;
            enemyUnit.transform.position = originalEnemyPos;

            // Địch tấn công lại
            StartCoroutine(EnemyMove());
        }
    }

    private float CalculateCatchRate(_Pokemon enemy)
    {
        // Tỉ lệ càng cao khi HP càng thấp, ví dụ:
        float hpRate = 1f - (float)enemy.HP / enemy.MaxHP;
        float baseRate = 0.2f; // Tỉ lệ cơ bản
        float maxBonus = 0.7f; // Tối đa cộng thêm
        return Mathf.Clamp(baseRate + hpRate * maxBonus, 0f, 0.95f);
    }

    private void AddPokemonToPlayer(_Pokemon pokemon)
    {
        if (saveManager != null)
        {
            var saved = new SavedPokemon
            {
                id = pokemon.Base.Id, // Đảm bảo PokemonBase có thuộc tính Id
                level = pokemon.Level,
                currentHP = pokemon.HP,
                learnedSkillIds = new List<string>() // Có thể lấy từ pokemon.Moves nếu muốn
            };
            saveManager.saveData.myPokemons.Add(saved);
            saveManager.SaveGame();
        }
    }
}