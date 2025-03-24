using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedCombo;   // �޺��� �������ΰ�?
    private bool alreadyApplyForce;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        /// �޺��� ���� ���� ������ �� ���¿��� ���� �޺��� �����ߴ��� ���ߴ����� üũ������ϹǷ� �ϴ� false�� �ʱ�ȭ
        alreadyAppliedCombo = false;
        alreadyApplyForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttakData.GetAttackInfo(comboIndex);

        // ó������ comboIndex�� 0�̾��ٰ�,
        // �޺��� �������̾��ٸ� attackInfoData��stateMachine�� �ִ� comboIndex�� ���´�        
        // �װ� attackInfoData�� �����Ѵ�
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        // �޺��� ���������� �ʴٸ�
        if (!alreadyAppliedCombo)
            stateMachine.ComboIndex = 0;    // �޺� �ʱ�ȭ
    }

    /// <summary>
    /// �޺� ���¸� ������Ʈ
    /// </summary>
    public override void Update()
    {
        base.Update();

        ForceMove();

        // ���� �������� �ִϸ��̼��� ���¸� �޾ƿ´�
        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        // �ִϸ��̼��� ���� �������̶��(1f: ����)
        if (normalizedTime < 1f)
        {
            // �޺��� ������ �� �ִ� �Ӱ�ð��� �����ٸ�
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
                TryApplyForce();  /// �޺� �õ�, ���� Update�� �����Ѵ�
            

            // damping �õ�
            if (normalizedTime >= attackInfoData.ComboTransitionTime)
                TryComboAttack(); /// Damping �õ�, ���� Update�� �����Ѵ�
        }
        /// Update �����ϴ� �ִϸ��̼��� ������ ���´�
        else // �ִϸ��̼��� ������
        {
            // �޺��� �������̶��
            if (alreadyAppliedCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex; // ComboIndex�� ���� �޺� �����ϰ�
                stateMachine.ChangeState(stateMachine.ComboAttackState); // ���� �޺��� �Ѿ��
            }
            else // �׷��� �ʴٸ�
            {
                // ���� �ִϸ��̼��� ������ Idle�� �ٲ۴�
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    private void TryComboAttack()
    {
        if (alreadyAppliedCombo) return;    // �޺��� �̹� �������̴�

        if (attackInfoData.ComboStateIndex == -1) return;   // ������ �޺��̹Ƿ� �Ѿ �ʿ䰡 ����

        if (!stateMachine.IsAttacking) return;  // ������ �ϰ� ���� ���� ���

        // �� �ܴ� �޺� ������ �� �� �ִ�
        alreadyAppliedCombo = true; 
    }

    // Damping: ���� �����ϴ� ���� �̶�� �ؼ��ϸ� �� �� 
    private void TryApplyForce()
    {
        // �̹� Force�� �������̶��
        if (alreadyApplyForce) return;

        // ���������� �ʴٸ�
        alreadyApplyForce = true;

        // ���� �ִ� ������ ����(������ ���ۿ� ������ ���� �ʱ� ����)
        stateMachine.Player.ForceReceiver.Reset();  

        // �����Ҷ� ������ ���ư��ϱ� forward�������� Force�� �ָ� �ȴ�
        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }
}