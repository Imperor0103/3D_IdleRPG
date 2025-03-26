using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 플레이어의 조작, UI띄우기 등 한다
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// C# 스크립트로 만들었다
    /// 이동, UI띄우기 등을 한다
    /// </summary>
    public PlayerInputs playerInput { get; private set; }  
    public PlayerInputs.PlayerActions playerActions { get; private set; }

    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    // Monster
    private List<GameObject> monsters;

    public UIManager uiManager { get; set; }

    private void Awake()
    {
        // playerInput은 OnEnable보다 먼저 생성되어야하므로 Awake에서 할당
        playerInput = new PlayerInputs();
        playerActions = playerInput.Player; // Action Map의 Player(PlayerInputs 에디터에서 확인한다)

        Agent = gameObject.GetOrAddComponent<NavMeshAgent>();

        /// tab키 입력 감지 후 UI 토글
        playerInput.Player.Inventory.performed += _ => ToggleInventoryUI();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    // 입력 받으면 UI 띄우거나 닫는다
    private void ToggleInventoryUI()
    {
        if (uiManager.inventory != null)
        {
            bool isActive = uiManager.inventory.gameObject.activeSelf;
            uiManager.inventory.gameObject.SetActive(!isActive);
        }
    }
}