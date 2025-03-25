using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Characters/Enemy")]

public class EnemySO : ScriptableObject
{
    // 몬스터 데이터 선언
    // 추적
    [field: SerializeField] public float PlayerChasingRange { get; private set; } = 10f;
    // 공격범위
    [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;

    // 공격
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public int Damage;


    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
}
