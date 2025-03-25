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
        // playerInput은 OnEnable보다 먼저 생성되어야하므로 Awake에서 할당
        playerInput = new PlayerInputs();
        playerActions = playerInput.Player; // Action Map의 Player(PlayerInputs 에디터에서 확인한다)

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

    /// NavMeshAgent를 이용하여 타겟을 찾는데, 컴포넌트 PlayerBaseState는 컴포넌트가 아니라서 붙일 수 없다
    /// 이곳에서 몬스터 타겟을 찾는다
    


}