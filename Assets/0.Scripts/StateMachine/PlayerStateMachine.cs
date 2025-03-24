using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public Vector2 MovementInput { get; set; }  // 상태(State)들이 받아갈 수 있는 값
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }

    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }


    public Transform MainCameraTransform { get; set; }  // Rotation할 때 카메라도 같이 회전해야한다

    // 수정사항
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

        // 수정사항
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