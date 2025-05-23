using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, IHealth
{


    public float health = 100f;
    public float armor = 0f;
    public float maxHealth = 100f;
    public float maxArmor = 0f;
    public float regen = 0f;
    public float regenSpeed = 0f;
    public UnityEvent<int> onHealthChanged = new UnityEvent<int>();
    public float uihealth;

    public float Health { get => health; set
        {
            health = Mathf.Clamp(value, 0, MaxHealth);
            onHealthChanged.Invoke(Mathf.RoundToInt(health));
        }
    }
    public float Armor { get => armor; set => armor = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float MaxArmor { get => maxArmor; set => maxArmor = value; }
    public float Regen { get => regen; set => regen = value; }
    public float RegenSpeed { get => regenSpeed; set => regenSpeed = value; }

    public bool IsDead;
    public bool HasArmor;

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        onHealthChanged.Invoke(Mathf.RoundToInt(Health));
    }
    public void TakeArmorDamage(float Damage)
    {
        Armor -= Damage;
    }
    public void DecreaseMaxHealth(float Amount) 
    {
        MaxHealth -= Amount;
        Mathf.Clamp(Health, 0, MaxHealth);
        Mathf.Clamp(health, 0, maxHealth);
    }
    public void DecreaseMaxArmor(float Amount) { }
    public void IncreaseMaxHealth(float Amount) 
    {
        MaxHealth += Amount;
        Mathf.Clamp(Health, 0, MaxHealth);
        Mathf.Clamp(health, 0, maxHealth);
        onHealthChanged.Invoke(Mathf.RoundToInt(Health));
    }
    public void IncreaseMaxArmor(float Amount) { }
    public void RegenHealth(float Amount, float speed) { }
    public void RegenArmor() { }
    public void IsDying()
    {
        transform.position += new Vector3(0, 1 * Time.deltaTime, 1);
    }
    void Start()
    {
        Mathf.Clamp(Health, 0, MaxHealth);
        Mathf.Clamp(health, 0, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        HasArmor = Armor > 0;
        IsDead = Health <= 0;
        if (IsDead)
        {
            IsDying();
        }
    }
}
