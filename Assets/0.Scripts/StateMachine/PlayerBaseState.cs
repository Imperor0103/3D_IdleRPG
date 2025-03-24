using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 모든 State들의 Base
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

        /// 이동을 멈추었을 때 이벤트 등록
        input.playerActions.Movement.canceled += OnMovementCanceled;

        /// 달리기 시작했을 때 이벤트 등록
        input.playerActions.Run.started += OnRunStarted;

        input.playerActions.Jump.started += OnJumpStarted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        /// 나갈때는 이벤트를 모두 해제
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
        // StartAnimation 함수 먼저 작성
        Move();
    }

    // Movement.canceled 이벤트에 등록할 함수
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }
    // Run.started 이벤트에 등록할 함수
    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }
    // Jump
    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }



    /// <summary>
    /// 모든 상태에는 애니메이션이 필요하다
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
    /// 움직임 관련 로직
    /// BaseState에 작성해도 되고
    /// GroundState, AttackState, AirState 등 카테고리별로 나누어 작성해도 된다
    /// 
    /// 이번에는 BaseState에 1번만 작성하여 사용하는법을 해보자
    /// 모든 경우에 사용할 수 있도록 예외상황을 제외한다
    /// </summary>
    private void Move()
    {
        // GetMoveMentDirection 함수 먼저 작성
        Vector3 movementDirection = GetMovementDirection();

        Move(movementDirection);

        // Rotate 함수 먼저 작성
        Rotate(movementDirection);
    }

    // 방향 구하기
    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        // y방향으로 흔들리지 않게 고정
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // forward * stateMachine.MovementInput.y: y방향
        // right * stateMachine.MovementInput.x: x방향
        // 메인카메라와 플레이어가 바라보는 방향을 같게 만든다
        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    // 실제로 움직인다
    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();

        // y방향(중력)을 추가한다
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

            // Slerp: 보간함수
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }
}