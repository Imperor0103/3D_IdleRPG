using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputs playerInput { get; private set; }
    public PlayerInputs.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        // playerInput은 OnEnable보다 먼저 생성되어야하므로 Awake에서 할당
        playerInput = new PlayerInputs();
        playerActions = playerInput.Player; // Action Map의 Player(PlayerInputs 에디터에서 확인한다)
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