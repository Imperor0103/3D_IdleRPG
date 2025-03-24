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

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
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
}
