using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ��� State���� Base
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
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        /// �������� �̺�Ʈ�� ��� ����
        input.playerActions.Movement.canceled -= OnMovementCanceled;
        input.playerActions.Jump.canceled -= OnJumpStarted;

        input.playerActions.Run.started -= OnRunStarted;
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
        Move();
    }

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

    // ������ �����δ�
    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();

        // y����(�߷�)�� �߰��Ѵ�
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
}