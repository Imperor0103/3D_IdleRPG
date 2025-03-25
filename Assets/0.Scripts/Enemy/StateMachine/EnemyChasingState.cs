using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerWalkState와 사실상 같다
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

        // 플레이어가 추적에서 벗어나면
        if (!IsInChaseRange())
        {
            // 몬스터는 다시 Idle상태로
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        // 공격범위 안에 들어왔다면
        else if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }

    // 공격범위 안에 들어왔는지 체크
    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }
}