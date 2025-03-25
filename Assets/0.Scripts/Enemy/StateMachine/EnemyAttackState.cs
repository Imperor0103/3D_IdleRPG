using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 구체적인 공격은 PlayerComboAttackState를 참고
/// </summary>
public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyApplyForce;
    private bool alreadyAppliedDealing; // 공격할때 딜 넣기 타이밍을 맞춘다

    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);

        alreadyApplyForce = false;
        alreadyAppliedDealing = false;

        stateMachine.MovementSpeedModifier = 0;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Enemy.Data.ForceTransitionTime)
                TryApplyForce();

            // alreadyAppliedDealing이 false일 때
            // normalizedTime이 Dealing_Start_TransitionTime보다 더 흘렀다면
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
            {
                // Weapon 켜준다
                stateMachine.Enemy.Weapon.SetAttack(stateMachine.Enemy.Data.Damage, stateMachine.Enemy.Data.Force);
                stateMachine.Enemy.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;                
            }

            if (alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_End_TransitionTime)
            {
                /// 왜 여기로 들어오는지 모르겠다

                // Weapon 꺼준다
                stateMachine.Enemy.Weapon.gameObject.SetActive(false);
            }

        }
        else // 공격 애니메이션이 끝났을 때
        {
            /// 공격 중에 플레이어가 멀리 도망갈 수 있으므로 추적 가능한 범위인지 먼저 살펴야한다
            /// 플레이어가 추적 가능한 범위에 있다면
            if (IsInChaseRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else // 플레이어가 도망갔다면
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }

    }

    private void TryApplyForce()
    {
        if (alreadyApplyForce) return;
        alreadyApplyForce = true;

        stateMachine.Enemy.ForceReceiver.Reset();

        stateMachine.Enemy.ForceReceiver.AddForce(stateMachine.Enemy.transform.forward * stateMachine.Enemy.Data.Force);

    }
}