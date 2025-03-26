using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ������ ���� ������, ���߿� Ŭ���� �ٽ� �����ٺ��� ����� ��
/// <summary>
/// ���ӸŴ����� �ʹ� �ʰ� �������
/// ���ӿ��� ���� ������ �ٷ��
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public bool isGameOver;
    public UIManager uiManager;


    public void GameOver()
    {
        isGameOver = true;

        // ���� �� UI ����
        Time.timeScale = 0f;
        uiManager.uiGameOver.gameObject.SetActive(true);
        // Ŀ�� ��� ����    
        Cursor.lockState = CursorLockMode.None;
    }
    public void Restart()
    {
        // ���� ���� �ٽ� �ε�
        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // health �ٽ� �������


        // �κ��丮 �ʱ�ȭ
        uiManager.inventory.ClearInventory();  // ����
        uiManager.inventory.gameObject.SetActive(true);    // Ȱ��ȭ
        // �κ��丮 ���ݴ� Toggle �޼��带 Action�� ����
        //CharacterManager.Instance.Player.controller.inventoryAction += UIManager.Instance.inventory.Toggle;
        uiManager.inventory.inventoryWindow.SetActive(false);


        // Ŀ�� �ٽ� ���
        //Cursor.lockState = CursorLockMode.Locked;
    }
}
