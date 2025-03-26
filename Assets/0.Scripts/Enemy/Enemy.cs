using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: Header("Reference")]
    // EnemySO����� ����
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    private EnemyStateMachine stateMachine;


    [field: SerializeField] public Weapon Weapon { get; private set; }

    public Health health { get; private set; }

    public StageManager stageManager;

    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        stateMachine = new EnemyStateMachine(this);

        health = GetComponent<Health>();

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);

        health.OnDie += OnDie;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;


        // 3�� ��ٸ�
        // 3�� �Ŀ� ��Ȱ��ȭ
        StartCoroutine(ReturnToPool(3f));
    }

    private IEnumerator ReturnToPool(float delay)
    {
        // 3�� ��ٸ�
        yield return new WaitForSeconds(delay);

        // ��Ȱ��ȭ
        gameObject.SetActive(false);
        stageManager.enemyPool.Add(gameObject);

    }


}
