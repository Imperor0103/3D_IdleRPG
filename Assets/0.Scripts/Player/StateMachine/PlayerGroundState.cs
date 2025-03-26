using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 땅에 붙어있을 때 점프할 수 있다
/// </summary>
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

    /// <summary>
    /// 맨 처음 ComboAttackState로 들어와 주는 것을 세팅
    /// </summary>
    public override void Update()
    {
        base.Update();
        if (stateMachine.Target != null)
            OnAttack();

        /// 입력할 때 가능
        //if (stateMachine.IsAttacking)
        //{
        //    // ComboAttackState에 최초 진입
        //    OnAttack();
        //    return;
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();


        /// stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime: 평소의 y보다 내려가고 있다
        /// FixedUpdate에서는 Time.fixedDeltaTime을 사용해야 한다
        if (!stateMachine.Player.Controller.isGrounded
      && stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
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

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        base.OnJumpStarted(context);

        // 여기서는 State만 바꾼다
        // ForceReceiver에 접근해서 호출하는 Jump함수는 ForceReceiver에 구현한다
        stateMachine.ChangeState(stateMachine.JumpState);
    }

    // 공격시 
    protected virtual void OnAttack()
    {
        Debug.Log(stateMachine.Target.name);
        // 타겟이 죽으면 Idle상태로 바꾸어 새로운 타겟을 찾는다
        if (stateMachine.Target.IsDie)
        {
            stateMachine.Target = null; // 타겟을 비운다
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        float distanace = Vector3.Distance(stateMachine.Target.transform.position, stateMachine.Player.transform.position);
        if (distanace <= stateMachine.Player.Data.AttakData.GetAttackInfo(stateMachine.ComboIndex).AttackRange)
        {
            stateMachine.ChangeState(stateMachine.ComboAttackState);
        }


    }

}
