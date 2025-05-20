using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;




public class EnemyActions : MonoBehaviour
{
    private EnemyHealth _enemyHealth;

    public PlayerStats _playerStats { get; private set; }

    public pCamera playerCameraT { get; private set; }

    public Transform playerTransform;

    [Inject]
    public SpriteRenderer _spriteRenderer; 

    private Vector3 enemyStartPosition;
    private Quaternion enemyStartRotation;

    Vector3 rayOrigin;
    public float rayDistance;
    public float coneAngle;
    public float AlertTimer;
    private float AlertDuration = 3f;


    public float alertToAttackTimer;
    public float alertToAttackDuration = 3f;
    private Vector3 currentDirection;
    private Vector3 targetDirection;
    public float enemyDamage;
    public float enemyAttackCooldown = 3f;
    public float enemyAttackTimer;


    Vector3 direction;
    Vector3 pDirection;

    float distanceZ;
    float distanceX;
    float distanciaPlana;
    float angulo;
    float anguloPraPlayer;
    public float rayYOffset = 0.9f;
    public float rayOriginYOffset = 0.5f;
    public float returnSpeed = 45f;

    public bool playerIsSeen;
    bool isReturning;
    public bool isAlert = false;
    public bool isShooting = false;

    [Inject]
    void Construct(PlayerStats _playerstats, EnemyHealth _enemyhealth, pCamera _pCamera)
    {
        playerCameraT = _pCamera;
        _playerStats = _playerstats;

    }

    private void HandleEnemyDeath()
    {
       enabled = false;
    }
    void Start()
    {
        Mathf.Clamp(enemyAttackTimer, 0, enemyAttackCooldown);
        _enemyHealth = GetComponent<EnemyHealth>();

        if (_enemyHealth != null)
            _enemyHealth.onDeath += HandleEnemyDeath;

        playerTransform = _playerStats.transform;
        enemyStartPosition = transform.position;
        enemyStartRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyHealth == null)
        {
            Debug.LogError("EnemyActions: _enemyHealth está null!");
            enabled = false;
            return;
        }
        if (playerTransform == null)
            Debug.LogError("playerTransform está null!");

        if (playerCameraT == null)
            Debug.LogError("playerCameraT está null!");

        if (_playerStats == null)
            Debug.LogError("_playerStats está null!");

        if (_enemyHealth.IsDead)
            return;

        if (!_enemyHealth.IsDead)
        {
            rayOrigin = transform.position + new Vector3(0, rayOriginYOffset, 0);
            Vector3 positionOffset = playerCameraT.transform.position;

            pDirection = (playerTransform.position + Vector3.up * 1.0f - rayOrigin).normalized;

            anguloPraPlayer = Vector3.Angle(transform.forward, pDirection);

            distanceX = playerTransform.position.x - transform.position.x;
            distanceZ = playerTransform.position.z - transform.position.z;


            distanciaPlana = Mathf.Sqrt(distanceX * distanceX + distanceZ * distanceZ);

            direction = playerTransform.position - transform.position;
            direction.y = 0;

            angulo = Mathf.Atan2(distanceX, distanceZ) * Mathf.Rad2Deg;

            if (angulo < 0)
            {
                angulo += 360;
            }
            float pAngulos = Mathf.RoundToInt(angulo % 45);

            if (isReturning)
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, enemyStartRotation, returnSpeed * Time.deltaTime);
                if (Quaternion.Angle(transform.rotation, enemyStartRotation) < 0.1f)
                {
                    transform.rotation = enemyStartRotation;
                    isReturning = false;
                    Debug.Log("retorno completo");
                }
            }
            enemyIdle();
        }else if (_enemyHealth.IsDead) 
        {
            Destroy(gameObject);
        }

    }
    
    void enemyIdle()
    {
        if (Physics.Raycast(rayOrigin, pDirection, out RaycastHit hitInfo, rayDistance))
        {
            Debug.DrawRay(rayOrigin, pDirection * rayDistance, Color.red);
            if (anguloPraPlayer <= coneAngle / 2 &&
                distanciaPlana <= rayDistance &&
                (hitInfo.collider.CompareTag("Player")))
            {
                playerIsSeen = true;
                enemyAlert();
                Debug.Log("player visto, em alerta!");
            }
            else
            {
                playerIsSeen = false;
            }
        }
        if(!playerIsSeen && isAlert)
        {
            Debug.Log("player esta fora de vista");
            AlertTimer -= Time.deltaTime;
            if (AlertTimer <= 0f)
            {
                
                enemyReturn();
            }
        }

    }
    void enemyAlert()
    {
        Debug.Log("alerta!");
        AlertTimer = AlertDuration;
        Vector3 lookDirection = playerTransform.position - transform.position;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
        transform.rotation = Quaternion.LookRotation(lookDirection);
        isAlert = true;
        isReturning = false;
        alertToAttackTimer -= Time.deltaTime;
        Mathf.Clamp(alertToAttackTimer, 0, alertToAttackDuration);

        if (alertToAttackTimer <= 0f)
        {
            Debug.Log("tempo esgotou");
            enemyAttack();
        }
        else if (alertToAttackTimer > 0f && !playerIsSeen)
        {
            alertToAttackTimer += Time.deltaTime;
        }
    }
    void enemyAttack()
    {
        Debug.Log("ataque");


        Vector3 attackDirection = (playerTransform.position - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(direction);
        enemyAttackTimer -= Time.deltaTime;

        Debug.DrawRay(rayOrigin, attackDirection * rayDistance, Color.blue);
        if(Physics.Raycast(rayOrigin, direction , out RaycastHit hitInfo, rayDistance) )
        {
            Debug.Log("raycast foi feito");
            if (hitInfo.collider.CompareTag("Player"))
            {
                Debug.Log("raycast atingiu player");
                if (enemyAttackTimer <= 0f)
                {
                    Debug.Log("atirou");
                    enemyAttackTimer = enemyAttackCooldown;
                    isShooting = true;
                    _playerStats.TakeDamage(enemyDamage);
                }
                else
                {
                    isShooting = false;
                }
            }
        }
    }
    void enemySearch()
    {

    }
    void enemyReturn()
    {
        isReturning = true;
        isAlert = false;
        
        
        Debug.Log("voltando ao estado inicial");
    }

    void OnDestroy()
    {
        if (_enemyHealth != null)
            _enemyHealth.onDeath -= HandleEnemyDeath;
    }
}
