using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // Ground에서 Movement 상태에서 끝
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // 기존에 입력이 없었다면 입력한 이벤트가 없었으므로 호출되는 이벤트도 없다
        if (stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        // 기존에 입력이 있었다면
        // 상태를 Idle로 바꾼다
        stateMachine.ChangeState(stateMachine.IdleState);
        /// 저장된 이벤트를 호출한다
        base.OnMovementCanceled(context);
    }
}
