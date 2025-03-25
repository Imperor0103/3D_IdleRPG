using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// ��� State���� Base
/// 
/// �÷��̾ �Է��ϸ� Walk, Run
/// �Է��� ���ٸ� �ڵ����� ���͸� �߰��Ѵ�
/// </summary>
public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;


    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }
    protected virtual void AddInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;

        /// �̵��� ���߾��� �� �̺�Ʈ ���
        input.playerActions.Movement.canceled += OnMovementCanceled;

        /// �޸��� �������� �� �̺�Ʈ ���
        input.playerActions.Run.started += OnRunStarted;

        input.playerActions.Jump.started += OnJumpStarted;

        input.playerActions.Attack.performed += OnAttackPerformed;
        input.playerActions.Attack.canceled += OnAttackCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        /// �������� �̺�Ʈ�� ��� ����
        input.playerActions.Movement.canceled -= OnMovementCanceled;
        input.playerActions.Jump.canceled -= OnJumpStarted;

        input.playerActions.Run.started -= OnRunStarted;

        input.playerActions.Attack.performed -= OnAttackPerformed;
        input.playerActions.Attack.canceled -= OnAttackCanceled;
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        // StartAnimation �Լ� ���� �ۼ�
        //Move();
    }

    #region �Է��� �� ȣ��Ǵ� �̺�Ʈ
    // Movement.canceled �̺�Ʈ�� ����� �Լ�
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }
    // Run.started �̺�Ʈ�� ����� �Լ�
    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }
    // Jump
    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }
    // ���� �����Ҷ� ����� �Լ�
    protected virtual void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = true;
    }
    // ������ ������ ����� �Լ�
    protected virtual void OnAttackCanceled(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = false;
    }
    #endregion

    /// <summary>
    /// ��� ���¿��� �ִϸ��̼��� �ʿ��ϴ�
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.playerActions.Movement.ReadValue<Vector2>();
    }

    #region �Է��� ���� �� �̵�����
    /// <summary>
    /// ������ ���� ����
    /// BaseState�� �ۼ��ص� �ǰ�
    /// GroundState, AttackState, AirState �� ī�װ����� ������ �ۼ��ص� �ȴ�
    /// 
    /// �̹����� BaseState�� 1���� �ۼ��Ͽ� ����ϴ¹��� �غ���
    /// ��� ��쿡 ����� �� �ֵ��� ���ܻ�Ȳ�� �����Ѵ�
    /// </summary>
    private void Move()
    {
        // GetMoveMentDirection �Լ� ���� �ۼ�
        Vector3 movementDirection = GetMovementDirection();

        Move(movementDirection);

        // Rotate �Լ� ���� �ۼ�
        Rotate(movementDirection);
    }

    // ���� ���ϱ�
    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        // y�������� ��鸮�� �ʰ� ����
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // forward * stateMachine.MovementInput.y: y����
        // right * stateMachine.MovementInput.x: x����
        // ����ī�޶�� �÷��̾ �ٶ󺸴� ������ ���� �����
        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    /// <summary>
    /// ������ �����δ�(���� Move�� ����)
    /// </summary>
    /// <param name="direction"></param>
    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();

        // Jump�� ȣ���ߴٸ� �����̵�(y����)�̰�
        // AddForce�� ȣ���ߴٸ� �������
        // �� �� �ƴ� ��쿡�� �����̵�, �����̵� ��� ����
        stateMachine.Player.Controller.Move(((direction * movementSpeed) + stateMachine.Player.ForceReceiver.Movement) * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return moveSpeed;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Slerp: �����Լ�
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    // �����̵�
    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }
    #endregion 

    /// <summary>
    /// �ִϸ��̼��� ���൵�� �޾ƿ��� �޼���
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        /// ��ȯ�ǰ� ���� �� && ���� �ִϸ��̼��� tag
        /// ��ȯ�ǰ� �ִٸ�, ���� �ִϸ��̼��� tag�� üũ
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime; // �� ���� �����ͼ� �޺� ������ ������ �������� �Ǵ��Ѵ�
        }
        /// ��ȯ�ǰ� ���� ���� �� && ���� �ִϸ��̼��� tag
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;  // �� ���� �����ͼ� ������ ������ �������� �Ǵ��Ѵ�
        }
        else
        {
            return 0f;
        }
    }


    /// <summary>
    /// �ֺ��� ���� �ִ��� �Ǵ�
    /// </summary>
    /// <returns></returns>
    protected bool IsInChaseRange()
    {
        // Ÿ���� ������ ���̻� �������� �ʴ´�
        if (stateMachine.Target.IsDie) return false;

        // sqrMagnitude: ��������
        // ��Ʈ�� ����� �͵� ���� ���ϸ� �� �� �־, �׳� �������·� �д�
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Player.Data.AttakData.GetAttackInfo(stateMachine.ComboIndex).EnemyChasingRange * stateMachine.Player.Data.AttakData.GetAttackInfo(stateMachine.ComboIndex).EnemyChasingRange;
    }

}