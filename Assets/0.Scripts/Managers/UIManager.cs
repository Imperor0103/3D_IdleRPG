using System;
using System.Collections;
using System.Collections.Generic;
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


    private void Start()
    {
        // �ν����Ϳ��� ĳ���Ѵ�
        uiGameOver.gameObject.SetActive(false); // ó������ ��Ȱ��ȭ
    }

}
