using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using Zenject.SpaceFighter;


public class EnemyHealth : MonoBehaviour, IHealth
{
    public float health = 100f;
    public float armor = 0f;
    public float maxHealth = 100f;
    public float maxArmor = 0f;
    public float regen = 0f;
    public float regenSpeed = 0f;

    public float Health { get => health; set => health = value; }
    public float Armor { get => armor; set => armor = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float MaxArmor { get => maxArmor; set => maxArmor = value; }
    public float Regen { get => regen; set => regen = value; }
    public float RegenSpeed { get => regenSpeed; set => regenSpeed = value; }

    float Deathtimer = 3f;
    public bool IsDead => health <= 0;

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
    }
    public void TakeArmorDamage(float Damage)
    {
        Armor -= Damage;
    }
    public void DecreaseMaxHealth(float Amount) { }
    public void DecreaseMaxArmor(float Amount) { }
    public void IncreaseMaxHealth(float Amount) { }
    public void IncreaseMaxArmor(float Amount) { }
    public void RegenHealth(float Amount, float speed) { }
    public void RegenArmor() { }

    public void OnDeath()
    {
        if (IsDead)
        {
            Deathtimer -= Time.deltaTime;
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
            if (Deathtimer <= 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
    public void Update()
    {
        OnDeath();
    }
}

