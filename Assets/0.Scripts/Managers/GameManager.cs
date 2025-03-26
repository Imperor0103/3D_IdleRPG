using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 아직은 쓰지 않지만, 나중에 클래스 다시 나누다보면 사용할 듯
/// <summary>
/// 게임매니저를 너무 늦게 만들었다
/// 게임오버 관련 로직을 다룬다
/// </summary>
public class GameManager : MonoBehaviour
{
    public bool isGameOver;

    public void GameOver()
    {
        isGameOver = true;

        // 정지 후 UI 띄운다
        Time.timeScale = 0f;
        Managers.Instance.ui.uiGameOver.gameObject.SetActive(true);
        // 커서 잠금 해제    
        Cursor.lockState = CursorLockMode.None;
    }
    public void Restart()
    {
        // 현재 씬을 다시 로드
        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // health 다시 원래대로


        // 인벤토리 초기화
        Managers.Instance.ui.inventory.ClearInventory();  // 비우고
        Managers.Instance.ui.inventory.gameObject.SetActive(true);    // 활성화
        // 인벤토리 여닫는 Toggle 메서드를 Action에 연결
        //CharacterManager.Instance.Player.controller.inventoryAction += UIManager.Instance.inventory.Toggle;
        Managers.Instance.ui.inventory.inventoryWindow.SetActive(false);


        // 커서 다시 잠금
        //Cursor.lockState = CursorLockMode.Locked;
    }
}
