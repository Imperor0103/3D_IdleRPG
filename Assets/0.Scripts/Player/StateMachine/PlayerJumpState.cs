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
        // �������¿� ����
        // stateMachine.JumpForce�� ������ �� ������ ���⿡�� �߰��صд�
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

    // Animator Controller �����ϰ� ���ƿ���
    public override void Update()
    {
        base.Update();

        /// stateMachine.Player.Controller.isGrounded�� ������ ������ �ʴ� ������
        /// isGrounded�� true���� false�� �ٲ�� ���� �ִϸ��̼��� �ݺ� ����� �� �ֱ� �����̴�
        /// 
        /// stateMachine.Player.Controller.velocity: �ӷ��� ��ȭ"��"
        /// �ְ������� ������ �� 0�� �ǰ� �׋����ʹ� ��ȭ���� <= 0 �̴�
        /// ��, ���� ��ġ���� �������� ���� ���� ���Ѵ�
        if (stateMachine.Player.Controller.velocity.y <= 0) 
        {
            /// �ְ����� ������ �׶����ʹ� Fall ����
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }
}
