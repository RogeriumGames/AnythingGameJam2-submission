using UnityEngine;

public class FirstPersonMove : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 cameraInitPosition;
    public Movimento movimento;
    private float bobTimer = 0f;
    public float bobFrequency = 5f;
    public float bobAmplitude = 0.05f;

    void Start()
    {
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
        
    }

}
