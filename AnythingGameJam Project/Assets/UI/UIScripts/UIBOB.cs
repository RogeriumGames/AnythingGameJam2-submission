using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FirstPersonMove : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 cameraInitPosition;
    public Movimento movimento;
    private float bobTimer = 0f;
    public float bobFrequency = 5f;
    public float bobAmplitude = 0.05f;
   
    public UnityEngine.UI.Image gunUIImage;
    public Sprite idleSprite;
    public Sprite shootSprite;
    private bool isShooting = false;
    private float cooldown;
    private float cooldownTime;
    private float shootSpriteDuration = 0.1f; // Tempo visível do sprite de tiro
    private float shootSpriteTimer = 0f;
    public Weapon weapon;
    
    void Start()
    {
        cooldownTime = 1f;
        initialPosition = transform.position;
    }
    void Update()
    {
        float offsetY;
        //float clampedY = Mathf.Clamp(transform.position.y, initialPosition.y - 2f, initialPosition.y + 2f);
        //transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        if (movimento.isWalking)
        {
            if (movimento.isRunning && !movimento.crouchingBool)
            {
                bobTimer += Time.deltaTime * (bobFrequency * 2f);
                offsetY = (Mathf.Sin(bobTimer)) * bobAmplitude;
            }
            else if (movimento.crouchingBool)
            {
                bobTimer += Time.deltaTime * (bobFrequency / 2f);
                offsetY = (Mathf.Sin(bobTimer)) * bobAmplitude;
            }
            else
            { 
                bobTimer += Time.deltaTime * (bobFrequency);
                offsetY = (Mathf.Sin(bobTimer)) * bobAmplitude;
                
            }
            transform.position = initialPosition + new Vector3(0, offsetY, 0);
        }
        if (Input.GetMouseButtonDown(0) && weapon.StdRifle.fireRateCooldown <= 0)
        {
            weapon.StdRifle.shoot();
            gunUIImage.sprite = shootSprite;
            isShooting = true;
            shootSpriteTimer = shootSpriteDuration;
        }

        // Atualiza o timer do sprite de tiro
        if (isShooting)
        {
            shootSpriteTimer -= Time.deltaTime;

            if (shootSpriteTimer <= 0f)
            {
                gunUIImage.sprite = idleSprite;
                isShooting = false;
            }
        }
    }
}


