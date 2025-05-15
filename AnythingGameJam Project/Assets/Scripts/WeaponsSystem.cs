using System;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    public static Camera cam;
    

    private void Start()
    {
        cam = Camera.main;
    }
    [System.Serializable]
    public class WeaponSettings
    {
        public string name;
        public string type;

        public float damage;
        public float range;
        public float fireRate;
        public float reloadTime;
        
        public void shoot()
        {
            Vector3 origin = Weapon.cam.transform.position;
            Vector3 direction = Weapon.cam.transform.forward;
            Debug.Log("atirou");
            Debug.DrawRay(origin, direction * range, Color.red, 1f);
            if (Physics.Raycast(Weapon.cam.transform.position, Weapon.cam.transform.forward, out RaycastHit hitInfo, range))
            {
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

    }

    public Weapon.WeaponSettings StdRifle;
    public Weapon.WeaponSettings Pistol;
    public Weapon.WeaponSettings Fists;
    
}






