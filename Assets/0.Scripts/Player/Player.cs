using UnityEngine;
using UnityEngine.AI;

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

    /// <summary>
    /// �ڵ������ ���� NavMesh
    /// </summary>
    public NavMeshAgent Agent { get; private set; }


    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }

    public CharacterController Controller { get; private set; }

    // y�ӵ��� �߷� ����
    public ForceReceiver ForceReceiver { get; private set; }


    private PlayerStateMachine stateMachine;
    [field: SerializeField] public Weapon Weapon { get; private set; }

    public Health health { get; private set; }


    private void Awake()
    {
        AnimationData.Initialize();


        Agent = GetComponent<NavMeshAgent>();

        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        stateMachine = new PlayerStateMachine(this);

        health = GetComponent<Health>();

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // ��������
        stateMachine.ChangeState(stateMachine.IdleState);   /// ���� ó������ ���� State�� Idle

        health.OnDie += OnDie;
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

    void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
    }
}