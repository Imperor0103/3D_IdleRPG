using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    // ���ӽ��۹�ư, ���������ư �����ϰ� �̺�Ʈ �޼��� �����Ѵ�
    public Button restartButton;
    public Button exitButton;

    public Action OnClickRestart;
    public Action OnClickExit;


    // Start is called before the first frame update
    void Start()
    {
        // ��ư ����(�ν�����)

        // �̺�Ʈ �޼��� ����
        OnClickRestart += Restart;
        OnClickExit += ExitGame;

    }


    // ���ӿ��� UI 

    public void Restart()
    {
        Debug.Log("���� �����");
        // ���� �ε����� �ʴ´ٸ�....
        // �÷��̾� ������� ���������� �� ��? 
        // ���߿� �غ���
    }
    public void ExitGame()
    {
        Debug.Log("���� ����");

        Application.Quit();
    }


}