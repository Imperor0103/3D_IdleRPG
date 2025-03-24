using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ground�� �ִ� Idle ����
/// </summary>
public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        // Idle�� �Ǳ� ���ؼ��� �����ؾ��Ѵ�
        stateMachine.MovementSpeedModifier = 0f;
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
    }
}