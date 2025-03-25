using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�� ���� ����
/// </summary>
public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    /// <summary>
    /// �ڵ������� ���� Target
    /// </summary>
    public Health Target { get; private set; }  // Ÿ���� Player�� Health
    public EnemyIdleState IdleState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }


    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;
        //Target = GameObject.FindGameObjectWithTag("Player");    // Ÿ���� �÷��̾�
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        // ���� �ʱ�ȭ
        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);

        MovementSpeed = Enemy.Data.GroundData.BaseSpeed;
        RotationDamping = Enemy.Data.GroundData.BaseRotationDamping;

    }
}