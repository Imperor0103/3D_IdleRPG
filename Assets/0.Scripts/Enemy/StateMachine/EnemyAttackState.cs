using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ü���� ������ PlayerComboAttackState�� ����
/// </summary>
public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyApplyForce;
    private bool alreadyAppliedDealing; // �����Ҷ� �� �ֱ� Ÿ�̹��� �����

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

            // alreadyAppliedDealing�� false�� ��
            // normalizedTime�� Dealing_Start_TransitionTime���� �� �귶�ٸ�
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
            {
                // Weapon ���ش�
                stateMachine.Enemy.Weapon.SetAttack(stateMachine.Enemy.Data.Damage, stateMachine.Enemy.Data.Force);
                stateMachine.Enemy.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;                
            }

            if (alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_End_TransitionTime)
            {
                /// �� ����� �������� �𸣰ڴ�

                // Weapon ���ش�
                stateMachine.Enemy.Weapon.gameObject.SetActive(false);
            }

        }
        else // ���� �ִϸ��̼��� ������ ��
        {
            /// ���� �߿� �÷��̾ �ָ� ������ �� �����Ƿ� ���� ������ �������� ���� ������Ѵ�
            /// �÷��̾ ���� ������ ������ �ִٸ�
            if (IsInChaseRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else // �÷��̾ �������ٸ�
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