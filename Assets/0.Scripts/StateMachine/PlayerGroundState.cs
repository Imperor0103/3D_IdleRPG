using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Ground상태에 진입하면 Ground 애니메이션이 실행되어야한다
        // Ground SubState에 진입한다
        /// GroundParameterHash를 통해 SubState에 진입
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        // 상태가 끝나면 애니메이션이 끝난다
        // Ground SubState에서 빠져나와야 한다
        /// GroundParameterHash를 통해 SubState에서 빠져나온다
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
