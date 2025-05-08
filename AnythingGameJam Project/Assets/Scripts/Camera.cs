using Unity.VisualScripting;
using UnityEngine;


public class Camera : MonoBehaviour
{
    // tenho q fazer com que o corpo rotacione no eixo horizontal, enquanto a camera só vai fazer o
    // trabalho de olhar pra cima e para baixo
    float rotationX = 0.0f;
    public float Sensi;
    public Transform body;
    public Rigidbody rb;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
       
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * Sensi;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * Sensi;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        body.Rotate(Vector3.up * mouseX);
        
    }


}
