using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 모든 State들의 Base
/// 
/// 플레이어가 입력하면 Walk, Run
/// 입력이 없다면 자동으로 몬스터를 추격한다
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

        input.playerActions.Attack.performed += OnAttackPerformed;
        input.playerActions.Attack.canceled += OnAttackCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        /// 나갈때는 이벤트를 모두 해제
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
        // StartAnimation 함수 먼저 작성
        //Move();
    }

    #region 입력할 때 호출되는 이벤트
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
    // 공격 시작할때 등록할 함수
    protected virtual void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = true;
    }
    // 공격이 끝날때 등록할 함수
    protected virtual void OnAttackCanceled(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = false;
    }
    #endregion

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

    #region 입력을 받을 때 이동로직
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

    /// <summary>
    /// 실제로 움직인다(위의 Move와 구분)
    /// </summary>
    /// <param name="direction"></param>
    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();

        // Jump를 호출했다면 수직이동(y방향)이고
        // AddForce를 호출했다면 수평방향
        // 둘 다 아닌 경우에는 수평이동, 수직이동 모두 없다
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

    // 수평이동
    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }
    #endregion 

    /// <summary>
    /// 애니메이션의 진행도를 받아오는 메서드
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        /// 전환되고 있을 때 && 다음 애니매이션이 tag
        /// 전환되고 있다면, 다음 애니메이션의 tag를 체크
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime; // 이 값을 가져와서 콤보 공격이 가능한 시점인지 판단한다
        }
        /// 전환되고 있지 않을 때 && 현재 애니메이션이 tag
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;  // 이 값을 가져와서 공격이 가능한 시점인지 판단한다
        }
        else
        {
            return 0f;
        }
    }


    /// <summary>
    /// 주변에 적이 있는지 판단
    /// </summary>
    /// <returns></returns>
    protected bool IsInChaseRange()
    {
        // 타겟이 죽으면 더이상 공격하지 않는다
        if (stateMachine.Target.IsDie) return false;

        // sqrMagnitude: 제곱상태
        // 루트를 씌우는 것도 연산 부하를 줄 수 있어서, 그냥 제곱상태로 둔다
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Player.Data.AttakData.GetAttackInfo(stateMachine.ComboIndex).EnemyChasingRange * stateMachine.Player.Data.AttakData.GetAttackInfo(stateMachine.ComboIndex).EnemyChasingRange;
    }

}