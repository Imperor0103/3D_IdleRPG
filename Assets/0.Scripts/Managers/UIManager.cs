using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI�Ŵ����� �ʹ� �ʰ� �������
/// ���ӿ���UI��ŭ�� �Ŵ����� ���� �����غ���
/// </summary>
public class UIManager : MonoBehaviour
{
    public UIGameOver uiGameOver;
    public GameManager gameManager;
    public UIInventory inventory;
    public TextMeshProUGUI currentStageText;

    public StageManager stageManager;
    private void Start()
    {
        // �ν����Ϳ��� ĳ���Ѵ�
        uiGameOver.gameObject.SetActive(false); // ó������ ��Ȱ��ȭ
    }

    // stageManager���� currentStage�� �޾ƿͼ� ǥ���Ѵ�
    public void ShowCurrentStage()
    {
        if (currentStageText != null)
        {
            //currentStageText.text = ""; // �ʱ�ȭ
            currentStageText.text = $"�������� {stageManager.GetCurrentStageNumber()}";
        }
    }

}
