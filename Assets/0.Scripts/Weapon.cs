using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ����, �÷��̾��� ���⿡�� �ۿ밡��
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCollider;   // �ڽ��� collider (character controller�� ���� collider)

    private int damage;     
    private float knockback;    // ���ݹ����� �ڷ� �з�����

    /// Weapon�� �ִ� Collider�� ���� �ٸ� Collider���� ����
    private List<Collider> alreadyCollider = new List<Collider>();


    private void OnEnable()
    {
        alreadyCollider.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;    // �ڽ��� collider�� ������ �������� �ʴ´�
        if (alreadyCollider.Contains(other)) return;

        alreadyCollider.Add(other);

        /// ����� collider�� ������� �ش� ������Ʈ�� ���� Health ������Ʈ�� damage�� �ش�
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }

        // �ǰ�ü���� �˹� ȿ��
        if (other.TryGetComponent(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}