using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public Vector2 MovementInput { get; set; }  // ����(State)���� �޾ư� �� �ִ� ��
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }

    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }


    public Transform MainCameraTransform { get; set; }  // Rotation�� �� ī�޶� ���� ȸ���ؾ��Ѵ�

    // ��������
    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }

    public PlayerComboAttackState ComboAttackState { get; }


    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        MainCameraTransform = Camera.main.transform;

        // ��������
        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);

        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;

        ComboAttackState = new PlayerComboAttackState(this);
    }
}