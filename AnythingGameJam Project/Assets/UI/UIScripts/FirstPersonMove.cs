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
        
        if (movimento.isWalking)
        {
            if (movimento.isRunning && !movimento.crouchingBool)
            {
                bobAmplitude = 1f;
                bobTimer += Time.deltaTime * (bobFrequency * 2f);
                offsetY = (Mathf.Sin(bobTimer) / 10) * bobAmplitude;
            }
            else if (movimento.crouchingBool)
            {
                bobAmplitude /= 10;
                bobTimer += Time.deltaTime * (bobFrequency / 2f);
                offsetY = (Mathf.Sin(bobTimer) / 10) * bobAmplitude;
            }
            else
            {
                bobAmplitude = 1f;
                bobTimer += Time.deltaTime * (bobFrequency);
                offsetY = (Mathf.Sin(bobTimer) / 10) * bobAmplitude;
                
            }
            transform.position = initialPosition += new Vector3(0, offsetY, 0);
        }

        Vector3.MoveTowards(transform.position, initialPosition, 0f);
    }

}
