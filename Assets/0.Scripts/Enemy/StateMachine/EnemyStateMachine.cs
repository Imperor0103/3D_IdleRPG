using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 거의 같다
/// </summary>
public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    /// <summary>
    /// 자동공격을 위한 Target
    /// </summary>
    public Health Target { get; private set; }  // 타겟인 Player의 Health
    public EnemyIdleState IdleState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }


    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;
        //Target = GameObject.FindGameObjectWithTag("Player");    // 타겟은 플레이어
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        // 상태 초기화
        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);

        MovementSpeed = Enemy.Data.GroundData.BaseSpeed;
        RotationDamping = Enemy.Data.GroundData.BaseRotationDamping;

    }
}