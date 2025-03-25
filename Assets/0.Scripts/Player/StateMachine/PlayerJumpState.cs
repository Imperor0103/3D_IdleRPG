using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        // 점프상태에 진입
        // stateMachine.JumpForce에 접근할 수 있으니 여기에서 추가해둔다
        stateMachine.JumpForce = stateMachine.Player.Data.AirData.JumpForce;
        stateMachine.Player.ForceReceiver.Jump(stateMachine.JumpForce);

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    // Animator Controller 수정하고 돌아오기
    public override void Update()
    {
        base.Update();

        /// stateMachine.Player.Controller.isGrounded로 조건을 만들지 않는 이유는
        /// isGrounded가 true에서 false로 바뀌는 동안 애니메이션이 반복 재생될 수 있기 때문이다
        /// 
        /// stateMachine.Player.Controller.velocity: 속력의 변화"량"
        /// 최고점에서 멈췄을 때 0이 되고 그떄부터는 변화량이 <= 0 이다
        /// 즉, 이전 위치보다 내려가고 있을 때를 뜻한다
        if (stateMachine.Player.Controller.velocity.y <= 0) 
        {
            /// 최고점을 지나면 그때부터는 Fall 상태
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }
}
