using UnityEngine;

public interface IHealth
{
    float MaxHealth { get; set; }
    float MaxArmor { get; set; }
    float Health { get; set; }
    float Armor { get; set; }
    float Regen { get; set; }
    float RegenSpeed { get; set; }

    void TakeDamage(float Damage);
    void TakeArmorDamage(float Damage);
    void RegenHealth(float Amount, float speed);
    void RegenArmor();
    void IncreaseMaxHealth(float Amount);
    void IncreaseMaxArmor(float Amount);
    void DecreaseMaxHealth(float Amount);
    void DecreaseMaxArmor(float Amount);
    void OnDeath();
}
