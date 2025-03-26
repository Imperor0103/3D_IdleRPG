using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    // 게임시작버튼, 게임종료버튼 연결하고 이벤트 메서드 연결한다
    public Button restartButton;
    public Button exitButton;

    public Action OnClickRestart;
    public Action OnClickExit;


    // Start is called before the first frame update
    void Start()
    {
        // 버튼 연결(인스펙터)

        // 이벤트 메서드 연결
        OnClickRestart += Restart;
        OnClickExit += ExitGame;

    }


    // 게임오버 UI 

    public void Restart()
    {
        Debug.Log("게임 재시작");
        // 씬을 로드하지 않는다면....
        // 플레이어 원래대로 돌려놓으면 될 듯? 
        // 나중에 해보자
    }
    public void ExitGame()
    {
        Debug.Log("게임 종료");

        Application.Quit();
    }


}