using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public Transform playerT;
    public Transform enemyT;
    public Transform enemySP;
    public LookAtPlayer lookScript;
    public SpriteRenderer enemySprite;
    private Vector3 enemyStartPosition;
    private Quaternion enemyStartRotation;

    Vector3 rayOrigin;
    public float rayDistance;
    public float coneAngle;
    public float AlertTimer;
    private float AlertDuration = 3f;

    Vector3 playerXZ;
    Vector3 enemyXZ;
    Vector3 direction;
    Vector3 pDirection;

    float distanceZ;
    float distanceX;
    float distanciaPlana;
    float angulo;
    float pAngulos;
    float anguloPraPlayer;
    public float returnSpeed = 45f;



    public bool playerIsSeen;
    bool isReturning;
    public bool isAlert = false;
    
    void Start()
    {
        
        enemyStartPosition = transform.position;
        enemyStartRotation = Quaternion.Euler(0, transform.rotation.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        pDirection = (playerT.position - transform.position).normalized;
        anguloPraPlayer = Vector3.Angle(transform.forward, pDirection);

        distanceX = playerT.position.x - transform.position.x;
        distanceZ = playerT.position.z - transform.position.z;


        distanciaPlana = Mathf.Sqrt(distanceX * distanceX + distanceZ * distanceZ);

        direction = playerT.position - transform.position;
        direction.y = 0;

        angulo = Mathf.Atan2(distanceX, distanceZ) * Mathf.Rad2Deg;

        if(angulo < 0)
        {
            angulo += 360;
        }
        pAngulos = Mathf.RoundToInt(angulo % 45);

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



    }
    
    void enemyIdle()
    {
        playerIsSeen = false;
        rayOrigin = transform.position;

        
        if (Physics.Raycast(rayOrigin, pDirection, out RaycastHit hitInfo, rayDistance))
        {
            
            Debug.DrawRay(rayOrigin, pDirection * rayDistance, Color.red);
            if (anguloPraPlayer <= coneAngle /2 &&
                distanciaPlana <= rayDistance &&
                hitInfo.transform == playerT)
            {
                playerIsSeen = true;
                enemyAlert();
                Debug.Log("player visto, em alerta!");
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
        transform.rotation = Quaternion.LookRotation(direction);
        isAlert = true;
        
        
    }
    void enemyAttack()
    {

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
}
