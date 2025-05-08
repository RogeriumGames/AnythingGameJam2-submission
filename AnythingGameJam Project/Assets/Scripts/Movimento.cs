using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;


public class Movimento : MonoBehaviour
{

    // --------------------
    // objetos:
    // --------------------

    public Rigidbody rb;
    public Transform cameraT;
    public CapsuleCollider capCol;

    // --------------------
    // aqui é relacionado a velocidade de correr, andar, etc
    // --------------------
    public float speed;
    public float maxspeed;
    public float atrito;
    public float sprintMulti;
    float praticalSpeed;
    float praticalMaxSpeed;
    public float JumpPower;

    // --------------------
    // para agachar
    // --------------------

    public float standingHeight = 2f;
    public float crouchingHeight = 1f;

    public float cameraStand = 2f;
    public float cameraCrouch = 1f;


    //Bools
    public bool grounded;
    public bool crouchingBool = false;
    public bool isRunning = false;
    public bool isWalking;
    // --------------------
    // Keys
    // --------------------
    public KeyCode runButton;
    public KeyCode crouchButton;
    public KeyCode jumpButton;

    void Start()
    {
        rb.freezeRotation = true;
    }
    void FixedUpdate()
    {
        // --------------------
        // movimento
        // --------------------
        float inX = Input.GetAxis("Horizontal");
        float inZ = Input.GetAxis("Vertical");


        Vector3 forward = cameraT.forward;
        Vector3 right = cameraT.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 Direction = (forward * inZ + right * inX).normalized;

        
        // --------------------
        // atrito
        // --------------------

        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        Vector3 rayDirection = Vector3.down;
        float rayDistance;
       
        if (crouchingBool)
        {
            rayDistance = 0.7f;
        }
        else
        {
            rayDistance = 1.1f;
        }

            grounded = Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, rayDistance);

        if (grounded) 
        {
            rb.linearDamping = atrito;
        }
        else
        {
            rb.linearDamping = 1f;
        }


        // --------------------
        // movimentação de correr + agachar
        // --------------------
        Vector3 HorVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        float praticalSpeed = speed;
        float praticalMaxSpeed = maxspeed;

        if (Input.GetKey(runButton) && !crouchingBool)
        {

            praticalSpeed *= sprintMulti;
            praticalMaxSpeed *= sprintMulti;
            isRunning = true;
        }else if (crouchingBool)
        {
            praticalSpeed = speed * 0.5f;
            rb.linearDamping *= 1.5f;
            isRunning = false;
        }
        else
        {
            isRunning = false;
        }

        // --------------------
        // limite de velocidade
        // --------------------

        if (HorVel.magnitude > praticalMaxSpeed)
        {
            Vector3 limitSpeed = HorVel.normalized * praticalMaxSpeed;
            rb.linearVelocity = new Vector3(limitSpeed.x, rb.linearVelocity.y, limitSpeed.z);
        }

        //adicionando velocidade
        if (grounded)
        {
            rb.AddForce(Direction * praticalSpeed, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce((Direction * praticalSpeed) /10, ForceMode.Acceleration);
        }

        //parar quando não tiver teclas tocando//

        if (grounded && inX == 0 && inZ == 0)
        {
            Vector3 vel = rb.linearVelocity;
            Vector3 horizontal = new Vector3(vel.x, 0, vel.z);

            Vector3 slowed = Vector3.Lerp(horizontal, Vector3.zero, Time.fixedDeltaTime * 10f);

            rb.linearVelocity = new Vector3(slowed.x, vel.y, slowed.z);
        }

        if (rb.linearVelocity.magnitude > 0.1f)
            isWalking = true;
        else
            isWalking = false;
    }


    
    void Update()
    {
        crouch();
        jumping();
        
    }

   void crouch()
    {
        if (Input.GetKey(crouchButton))
        {
            
            capCol.height = crouchingHeight;
            capCol.center = Vector3.zero;
            cameraT.GetChild(0).localPosition = new Vector3(0, -0.5f, 0);
            crouchingBool = true;
        }
        else
        {
            capCol.height = standingHeight;
            capCol.center = Vector3.zero;
            cameraT.GetChild(0).localPosition = new Vector3(0, 0f, 0);
            crouchingBool = false;
        }
    }

    void jumping()
    {
        if (Input.GetKeyDown(jumpButton) && grounded){
           
            rb.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
        }


    }
}
