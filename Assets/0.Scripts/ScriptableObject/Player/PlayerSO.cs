using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class PlayerGroundData     // 땅에 붙어있을 떄 필요한 데이터
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;   // 기본 이동속도
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f; // 기본 회전속도


    [field: Header("IdleData")] // Idle에 필요한 데이터


    [field: Header("WalkData")] // Walk에 필요한 데이터
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f; // BaseSpeed에 곱해주는 값    


    [field: Header("RunData")]
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
}



[Serializable]
public class PlayerAirData      // 공중에 있을 때 필요한 데이터
{
    [field: Header("JumpData")]
    [field: SerializeField][field: Range(0f, 25f)] public float JumpForce { get; private set; } = 5f;

}

// 플레이어의 콤보 어택 구현
[Serializable]
public class PlayerAttackData   // 플레이어가 공격할 때 필요한 데이터
{
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }   // 공격을 여러번 해야하니까 List에 담았다
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }   // 공격 개수를 가져온다
    public AttackInfoData GetAttackInfo(int index) { return AttackInfoDatas[index]; }   // 공격의 정보를 가져온다
}

/// <summary>
/// 콤보 어택을 위해 필요한 데이터
/// </summary>
[Serializable]
public class AttackInfoData
{
    /// 플레이어가 몬스터를 자동공격하기 위해 필요한 데이터
    // 추적
    [field: SerializeField] public float EnemyChasingRange { get; private set; } = 30f;
    // 공격범위
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;



    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; }    // 콤보를 이어 나갈 수 있는 임계시간
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }    // 특정 시간 내에 공격하면 몬스터가 살짝 뒤로 밀린다
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }   // 힘을 얼마나 줄것인지
    [field: SerializeField] public int Damage;  // 데미지
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }   // 
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
}




[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    // 플레이어 데이터 선언
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerAirData AirData { get; private set; }
    [field: SerializeField] public PlayerAttackData AttakData { get; private set; } // 공격정보
}
