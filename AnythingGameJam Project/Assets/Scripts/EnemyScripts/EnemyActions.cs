using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public Transform playerT;
    public Transform enemyT;
    private Vector3 enemyStartPosition;
    private Quaternion enemyStartRotation;
    private bool playerFound;

    Vector3 rayOrigin;
    public float rayDistance;

    

    void Start()
    {
        
        enemyStartPosition = transform.position;
        enemyStartRotation = Quaternion.Euler(0, transform.rotation.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        enemyIdle();
    }
    
    void enemyIdle()
    {
        rayOrigin = transform.position;
        Debug.DrawRay(rayOrigin, transform.forward * rayDistance, Color.red);
        playerFound = Physics.Raycast(rayOrigin, transform.forward, out RaycastHit hitInfo, rayDistance);
        Debug.DrawRay(rayOrigin, transform.forward * rayDistance, Color.red);
    }
    void enemyAlert()
    {

    }
    void enemyAttack()
    {

    }
    void enemySearch()
    {

    }
    void enemyReturn()
    {

    }
}
