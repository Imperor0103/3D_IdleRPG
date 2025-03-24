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

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    // 플레이어 데이터 선언
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerAirData AirData { get; private set; }

}
