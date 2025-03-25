using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public PlayerInputs playerInput { get; private set; }
    public PlayerInputs.PlayerActions playerActions { get; private set; }

    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    // Monster
    private List<GameObject> monsters;




    private void Awake()
    {
        // playerInput�� OnEnable���� ���� �����Ǿ���ϹǷ� Awake���� �Ҵ�
        playerInput = new PlayerInputs();
        playerActions = playerInput.Player; // Action Map�� Player(PlayerInputs �����Ϳ��� Ȯ���Ѵ�)

        Agent = gameObject.GetOrAddComponent<NavMeshAgent>();


    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    /// NavMeshAgent�� �̿��Ͽ� Ÿ���� ã�µ�, ������Ʈ PlayerBaseState�� ������Ʈ�� �ƴ϶� ���� �� ����
    /// �̰����� ���� Ÿ���� ã�´�
    


}