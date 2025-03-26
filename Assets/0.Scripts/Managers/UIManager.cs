using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI매니저를 너무 늦게 만들었다
/// 게임오버UI만큼은 매니저를 통해 관리해보자
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
        // 인스펙터에서 캐싱한다
        uiGameOver.gameObject.SetActive(false); // 처음에는 비활성화
    }

    // stageManager에서 currentStage를 받아와서 표시한다
    public void ShowCurrentStage()
    {
        if (currentStageText != null)
        {
            //currentStageText.text = ""; // 초기화
            currentStageText.text = $"스테이지 {stageManager.GetCurrentStageNumber()}";
        }
    }

}
