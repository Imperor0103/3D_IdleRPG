using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터터의 무기, 플레이어의 무기에도 작용가능
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCollider;   // 자신의 collider (character controller에 붙은 collider)

    private int damage;     
    private float knockback;    // 공격받으면 뒤로 밀려난다

    /// Weapon에 있는 Collider에 닿은 다른 Collider들을 저장
    private List<Collider> alreadyCollider = new List<Collider>();


    private void OnEnable()
    {
        alreadyCollider.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;    // 자신의 collider가 닿으면 공격하지 않는다
        if (alreadyCollider.Contains(other)) return;

        alreadyCollider.Add(other);

        /// 상대의 collider에 닿았을때 해당 오브젝트가 가진 Health 컴포넌트에 damage를 준다
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }

        // 피격체에게 넉백 효과
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