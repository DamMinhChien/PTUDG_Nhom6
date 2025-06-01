using UnityEngine;

public enum GameState
{
    FreeRoam,
    Battle
}

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerController;
    [SerializeField] private BattleSystem battleSystem;

    private void Start()
    {
        playerController.OnEncountered += StartBattle;
    }

    private GameState state;

    private void Update()
    {
        switch (state)
        {
            case GameState.FreeRoam:
                playerController.HandleUpdate();
                break;

            case GameState.Battle:
                battleSystem.HandleUpdate();
                break;
        }
    }

    // HÀM CHUYỂN SANG TRẠNG THÁI CHIẾN ĐẤU
    public void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        // Có thể gọi battleSystem.StartBattle() nếu muốn khởi tạo giao diện
    }

    // HÀM THOÁT TRẠNG THÁI CHIẾN ĐẤU
    public void EndBattle()
    {
        Debug.Log("EndBattle() được gọi!");
        state = GameState.FreeRoam;
        if (battleSystem != null)
            battleSystem.gameObject.SetActive(false);
        // Các xử lý khác...
    }
}

