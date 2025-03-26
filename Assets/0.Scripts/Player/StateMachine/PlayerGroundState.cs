using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ���� �پ����� �� ������ �� �ִ�
/// </summary>
public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Ground���¿� �����ϸ� Ground �ִϸ��̼��� ����Ǿ���Ѵ�
        // Ground SubState�� �����Ѵ�
        /// GroundParameterHash�� ���� SubState�� ����
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        // ���°� ������ �ִϸ��̼��� ������
        // Ground SubState���� �������;� �Ѵ�
        /// GroundParameterHash�� ���� SubState���� �������´�
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    /// <summary>
    /// �� ó�� ComboAttackState�� ���� �ִ� ���� ����
    /// </summary>
    public override void Update()
    {
        base.Update();
        if (stateMachine.Target != null)
            OnAttack();

        /// �Է��� �� ����
        //if (stateMachine.IsAttacking)
        //{
        //    // ComboAttackState�� ���� ����
        //    OnAttack();
        //    return;
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();


        /// stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime: ����� y���� �������� �ִ�
        /// FixedUpdate������ Time.fixedDeltaTime�� ����ؾ� �Ѵ�
        if (!stateMachine.Player.Controller.isGrounded
      && stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }

    // Ground���� Movement ���¿��� ��
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // ������ �Է��� �����ٸ� �Է��� �̺�Ʈ�� �������Ƿ� ȣ��Ǵ� �̺�Ʈ�� ����
        if (stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        // ������ �Է��� �־��ٸ�
        // ���¸� Idle�� �ٲ۴�
        stateMachine.ChangeState(stateMachine.IdleState);
        /// ����� �̺�Ʈ�� ȣ���Ѵ�
        base.OnMovementCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        base.OnJumpStarted(context);

        // ���⼭�� State�� �ٲ۴�
        // ForceReceiver�� �����ؼ� ȣ���ϴ� Jump�Լ��� ForceReceiver�� �����Ѵ�
        stateMachine.ChangeState(stateMachine.JumpState);
    }

    // ���ݽ� 
    protected virtual void OnAttack()
    {
        Debug.Log(stateMachine.Target.name);
        // Ÿ���� ������ Idle���·� �ٲپ� ���ο� Ÿ���� ã�´�
        if (stateMachine.Target.IsDie)
        {
            stateMachine.Target = null; // Ÿ���� ����
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
