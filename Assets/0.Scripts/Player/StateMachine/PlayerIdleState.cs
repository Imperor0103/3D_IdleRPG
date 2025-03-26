using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ground�� �ִ� Idle ����
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
        // Idle�� �Ǳ� ���ؼ��� �����ؾ��Ѵ�
        /// NavMeshAgent�� ����� ���� Agent.Speed�� �����ؾ� �Ѵ�
        stateMachine.Player.Agent.speed = 0f;


        /// �Ʒ��� �Է����� �� �� 
        //stateMachine.MovementSpeedModifier = 0f;
        base.Enter();

        /// Ground�� ���� �⺻ Idle ����
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

        // Ÿ���� �������� Ÿ�� ã��
        if (stateMachine.Target == null)
        {
            FindEnemy();
        }

        //// �Է°��� �ִٸ�
        //if (stateMachine.MovementInput != Vector2.zero)
        //{
        //    // PlayerWalkState ���� ���� �� ����!
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