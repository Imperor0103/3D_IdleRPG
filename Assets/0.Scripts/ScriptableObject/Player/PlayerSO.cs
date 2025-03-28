using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class PlayerGroundData     // ���� �پ����� �� �ʿ��� ������
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;   // �⺻ �̵��ӵ�
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f; // �⺻ ȸ���ӵ�


    [field: Header("IdleData")] // Idle�� �ʿ��� ������


    [field: Header("WalkData")] // Walk�� �ʿ��� ������
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f; // BaseSpeed�� �����ִ� ��    


    [field: Header("RunData")]
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
}



[Serializable]
public class PlayerAirData      // ���߿� ���� �� �ʿ��� ������
{
    [field: Header("JumpData")]
    [field: SerializeField][field: Range(0f, 25f)] public float JumpForce { get; private set; } = 5f;

}

// �÷��̾��� �޺� ���� ����
[Serializable]
public class PlayerAttackData   // �÷��̾ ������ �� �ʿ��� ������
{
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }   // ������ ������ �ؾ��ϴϱ� List�� ��Ҵ�
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }   // ���� ������ �����´�
    public AttackInfoData GetAttackInfo(int index) { return AttackInfoDatas[index]; }   // ������ ������ �����´�
}

/// <summary>
/// �޺� ������ ���� �ʿ��� ������
/// </summary>
[Serializable]
public class AttackInfoData
{
    /// �÷��̾ ���͸� �ڵ������ϱ� ���� �ʿ��� ������
    // ����
    [field: SerializeField] public float EnemyChasingRange { get; private set; } = 30f;
    // ���ݹ���
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;



    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; }    // �޺��� �̾� ���� �� �ִ� �Ӱ�ð�
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }    // Ư�� �ð� ���� �����ϸ� ���Ͱ� ��¦ �ڷ� �и���
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }   // ���� �󸶳� �ٰ�����
    [field: SerializeField] public int Damage;  // ������
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }   // 
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
}




[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    // �÷��̾� ������ ����
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerAirData AirData { get; private set; }
    [field: SerializeField] public PlayerAttackData AttakData { get; private set; } // ��������
}
