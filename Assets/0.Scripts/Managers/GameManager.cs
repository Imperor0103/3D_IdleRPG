using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ������ ���� ������, ���߿� Ŭ���� �ٽ� �����ٺ��� ����� ��
/// <summary>
/// ���ӸŴ����� �ʹ� �ʰ� �������
/// ���ӿ��� ���� ������ �ٷ��
/// </summary>
public class GameManager : MonoBehaviour
{
    public bool isGameOver;

    public void GameOver()
    {
        isGameOver = true;

        // ���� �� UI ����
        Time.timeScale = 0f;
        Managers.Instance.ui.uiGameOver.gameObject.SetActive(true);
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
        Managers.Instance.ui.inventory.ClearInventory();  // ����
        Managers.Instance.ui.inventory.gameObject.SetActive(true);    // Ȱ��ȭ
        // �κ��丮 ���ݴ� Toggle �޼��带 Action�� ����
        //CharacterManager.Instance.Player.controller.inventoryAction += UIManager.Instance.inventory.Toggle;
        Managers.Instance.ui.inventory.inventoryWindow.SetActive(false);


        // Ŀ�� �ٽ� ���
        //Cursor.lockState = CursorLockMode.Locked;
    }
}
