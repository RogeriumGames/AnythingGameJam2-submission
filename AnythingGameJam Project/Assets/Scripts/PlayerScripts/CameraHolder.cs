using UnityEngine;

public class CameraHolder : MonoBehaviour
{

    public Transform target;
    private Vector3 initialPosition;
    private float bobTimer = 0f;
    public float bobFrequency = 5f;
    public float bobAmplitude = 0.015f;

    public Movimento movimento;

    private void Start()
    {
        initialPosition = transform.position;
    }


    private void Update()
    {

        float offsetY;
        if (movimento.isWalking)
        {


            if (movimento.isRunning && !movimento.crouchingBool)
            {

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
                bobAmplitude = 0.015f;
                bobTimer += Time.deltaTime * (bobFrequency);
                offsetY = (Mathf.Sin(bobTimer) / 10) * bobAmplitude;
               
            }
            transform.position = transform.position + new Vector3(0, offsetY, 0);


        }
        Vector3.MoveTowards(transform.position, initialPosition, 0f);
    }

}
