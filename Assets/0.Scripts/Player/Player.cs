using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// field: �ʵ忡 �����شٴ� �ǹ�
    /// field �Ⱦ��� Header, SerializeField �� ���� ����� ������ ��ģ��
    /// ���� ���� field: ���δ�
    /// 
    /// PlayerAnimationData�� Seria  lizable ������ �־�� �Ѵ�
    /// SerializeField�����δ� �Ⱥ���
    /// </summary>
    [field: SerializeField] public PlayerSO Data { get; private set; }  // �÷��̾��� SO

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
        // ��������
        stateMachine.ChangeState(stateMachine.IdleState);   /// ���� ó������ ���� State�� Idle
    }

    // �����ֱ� �Լ�
    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    // ���� ����
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}