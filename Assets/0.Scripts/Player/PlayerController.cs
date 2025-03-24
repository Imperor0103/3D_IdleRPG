using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputs playerInput { get; private set; }
    public PlayerInputs.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        // playerInput�� OnEnable���� ���� �����Ǿ���ϹǷ� Awake���� �Ҵ�
        playerInput = new PlayerInputs();
        playerActions = playerInput.Player; // Action Map�� Player(PlayerInputs �����Ϳ��� Ȯ���Ѵ�)
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}