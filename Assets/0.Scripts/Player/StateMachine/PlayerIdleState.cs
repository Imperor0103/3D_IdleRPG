using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ground에 있는 Idle 상태
/// </summary>
public class PlayerIdleState : PlayerGroundState
{
    private readonly LayerMask enemyLayer;


    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");

    }

    public override void Enter()
    {
        // Idle이 되기 위해서는 정지해야한다
        /// NavMeshAgent를 사용할 떄는 Agent.Speed를 조절해야 한다
        stateMachine.Player.Agent.speed = 0f;


        /// 아래는 입력으로 할 때 
        //stateMachine.MovementSpeedModifier = 0f;
        base.Enter();

        /// Ground에 들어가면 기본 Idle 상태
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        // 타겟이 없을때만 타겟 찾기
        if (stateMachine.Target == null)
        {
            FindEnemy();
        }

        //// 입력값이 있다면
        //if (stateMachine.MovementInput != Vector2.zero)
        //{
        //    // PlayerWalkState 먼저 생성 후 복귀!
        //    stateMachine.ChangeState(stateMachine.WalkState);
        //    return;
        //}
    }

    private void FindEnemy()
    {
        var enemies = Physics.OverlapSphere(stateMachine.Player.transform.position, 15f, enemyLayer);

        if (enemies.Length > 0)
        {
            stateMachine.Target = enemies[0].GetComponent<Health>();
            stateMachine.Player.Agent.SetDestination(stateMachine.Target.transform.position);
            stateMachine.ChangeState(stateMachine.RunState);
        }
    }

}