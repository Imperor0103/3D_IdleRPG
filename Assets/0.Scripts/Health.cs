using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health; // 현재 체력
    public event Action OnDie;  // 죽으면 호출될 delegate
    public int curHealth;
    public Image hpBar;


    public bool IsDie = false;

    private void Start()
    {
        health = maxHealth;
        curHealth = maxHealth;
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }


    public void TakeDamage(int damage)
    {
        if (health == 0) return;

        health = Mathf.Max(health - damage, 0);

        curHealth -= damage;
        hpBar.fillAmount = (float)curHealth / maxHealth;


        if (health == 0)
        {
            IsDie = true;
            OnDie?.Invoke();
        }


        Debug.Log(health);
    }
}
