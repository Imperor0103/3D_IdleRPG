using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// field: 필드에 보여준다는 의미
    /// field 안쓰고 Header, SerializeField 만 쓰면 헤더의 선언이 겹친다
    /// 따라서 각각 field: 붙인다
    /// 
    /// PlayerAnimationData는 Seria  lizable 선언이 있어야 한다
    /// SerializeField만으로는 안보여
    /// </summary>
    [field: SerializeField] public PlayerSO Data { get; private set; }  // 플레이어의 SO

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }  


    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
       
    public CharacterController Controller { get; private set; }

    private PlayerStateMachine stateMachine;


    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();

        stateMachine = new PlayerStateMachine(this);

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // 수정사항
        stateMachine.ChangeState(stateMachine.IdleState);   /// 가장 처음으로 들어가는 State는 Idle
    }

    // 생명주기 함수
    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    // 물리 관련
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}