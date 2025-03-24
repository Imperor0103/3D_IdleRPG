using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedCombo;   // 콤보가 진행중인가?
    private bool alreadyApplyForce;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        /// 콤보를 진행 중일 때에도 그 상태에서 다음 콤보로 진행했는지 안했는지를 체크해줘야하므로 일단 false로 초기화
        alreadyAppliedCombo = false;
        alreadyApplyForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttakData.GetAttackInfo(comboIndex);

        // 처음에는 comboIndex가 0이었다가,
        // 콤보가 진행중이었다면 attackInfoData에stateMachine에 있던 comboIndex가 들어온다        
        // 그걸 attackInfoData에 저장한다
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        // 콤보가 진행중이지 않다면
        if (!alreadyAppliedCombo)
            stateMachine.ComboIndex = 0;    // 콤보 초기화
    }

    /// <summary>
    /// 콤보 상태를 업데이트
    /// </summary>
    public override void Update()
    {
        base.Update();

        ForceMove();

        // 현재 진행중인 애니메이션의 상태를 받아온다
        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        // 애니메이션이 아직 진행중이라면(1f: 종료)
        if (normalizedTime < 1f)
        {
            // 콤보를 진행할 수 있는 임계시간이 끝났다면
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
                TryApplyForce();  /// 콤보 시도, 이후 Update를 진행한다
            

            // damping 시도
            if (normalizedTime >= attackInfoData.ComboTransitionTime)
                TryComboAttack(); /// Damping 시도, 이후 Update를 진행한다
        }
        /// Update 진행하다 애니메이션이 끝나면 들어온다
        else // 애니메이션이 끝났다
        {
            // 콤보가 진행중이라면
            if (alreadyAppliedCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex; // ComboIndex에 현재 콤보 저장하고
                stateMachine.ChangeState(stateMachine.ComboAttackState); // 다음 콤보로 넘어간다
            }
            else // 그렇지 않다면
            {
                // 공격 애니메이션을 끝내고 Idle로 바꾼다
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    private void TryComboAttack()
    {
        if (alreadyAppliedCombo) return;    // 콤보가 이미 진행중이다

        if (attackInfoData.ComboStateIndex == -1) return;   // 마지막 콤보이므로 넘어갈 필요가 없다

        if (!stateMachine.IsAttacking) return;  // 공격을 하고 있지 않은 경우

        // 그 외는 콤보 공격을 할 수 있다
        alreadyAppliedCombo = true; 
    }

    // Damping: 힘을 전달하는 과정 이라고 해석하면 될 듯 
    private void TryApplyForce()
    {
        // 이미 Force가 진행중이라면
        if (alreadyApplyForce) return;

        // 진행중이지 않다면
        alreadyApplyForce = true;

        // 남아 있는 정보를 리셋(이후의 동작에 영향을 주지 않기 위해)
        stateMachine.Player.ForceReceiver.Reset();  

        // 공격할때 앞으로 나아가니까 forward방향으로 Force를 주면 된다
        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }
}