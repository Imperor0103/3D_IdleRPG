using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ground에 있는 Idle 상태
/// </summary>
public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        // Idle이 되기 위해서는 정지해야한다
        stateMachine.MovementSpeedModifier = 0f;
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
    }
}