using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerWalkState�� ��ǻ� ����
/// </summary>
public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();

        // �÷��̾ �������� �����
        if (!IsInChaseRange())
        {
            // ���ʹ� �ٽ� Idle���·�
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        // ���ݹ��� �ȿ� ���Դٸ�
        else if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }

    // ���ݹ��� �ȿ� ���Դ��� üũ
    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }
}