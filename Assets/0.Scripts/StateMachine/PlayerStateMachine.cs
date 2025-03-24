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

    public Transform MainCameraTransform { get; set; }  // Rotation�� �� ī�޶� ���� ȸ���ؾ��Ѵ�

    // ��������
    public PlayerIdleState IdleState { get; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        MainCameraTransform = Camera.main.transform;

        // ��������
        IdleState = new PlayerIdleState(this);

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;
    }
}