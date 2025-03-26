using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �÷��̾��� ����, UI���� �� �Ѵ�
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// C# ��ũ��Ʈ�� �������
    /// �̵�, UI���� ���� �Ѵ�
    /// </summary>
    public PlayerInputs playerInput { get; private set; }  
    public PlayerInputs.PlayerActions playerActions { get; private set; }

    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    // Monster
    private List<GameObject> monsters;

    public UIManager uiManager { get; set; }

    private void Awake()
    {
        // playerInput�� OnEnable���� ���� �����Ǿ���ϹǷ� Awake���� �Ҵ�
        playerInput = new PlayerInputs();
        playerActions = playerInput.Player; // Action Map�� Player(PlayerInputs �����Ϳ��� Ȯ���Ѵ�)

        Agent = gameObject.GetOrAddComponent<NavMeshAgent>();

        /// tabŰ �Է� ���� �� UI ���
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

    // �Է� ������ UI ���ų� �ݴ´�
    private void ToggleInventoryUI()
    {
        if (uiManager.inventory != null)
        {
            bool isActive = uiManager.inventory.gameObject.activeSelf;
            uiManager.inventory.gameObject.SetActive(!isActive);
        }
    }
}