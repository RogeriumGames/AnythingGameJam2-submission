using System;
using UnityEngine;
using Zenject;



public class Weapon : MonoBehaviour
{
    public static Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        StdRifle.UpdateCooldown();
        Pistol.UpdateCooldown();
        Fists.UpdateCooldown();
    }
    [System.Serializable]
    public class WeaponSettings
    {
        public FirstPersonMove firstPersonMove;

        
        public string name;
        public string type;

        public float damage;
        public float range;
        public float fireRate;
        public float fireRateCooldown = 0f;
        public float reloadTime;
        
      
        public void shoot()
        {
            Vector3 origin = Weapon.cam.transform.position;
            Vector3 direction = Weapon.cam.transform.forward;
            Debug.Log("atirou");
            Debug.DrawRay(origin, direction * range, Color.red, 1f);

            if (fireRateCooldown > 0f)
            {
                Debug.Log("em cooldown");
                return;
            }

            if (Physics.Raycast(Weapon.cam.transform.position, Weapon.cam.transform.forward, out RaycastHit hitInfo, range))
            {
                fireRateCooldown = fireRate;
                Debug.Log("atirou fez raycast");
                RaycastHit[] hits = Physics.RaycastAll(origin, direction, range);
                foreach (var hit in hits)
                {
                    if (hit.collider.CompareTag("PlayerBody")) continue;

                    if (hitInfo.collider.GetComponent<EnemyHealth>() is EnemyHealth enemy)
                    {
                        Debug.Log("atirou e acertou");
                        enemy.TakeDamage(damage);
                        break;
                    }
                    
                }
            }

        }
        public void UpdateCooldown()
        {
            if (fireRateCooldown > 0f )
                fireRateCooldown -= Time.deltaTime;
        }

    }
   

    public Weapon.WeaponSettings StdRifle;
    public Weapon.WeaponSettings Pistol;
    public Weapon.WeaponSettings Fists;
    
}






