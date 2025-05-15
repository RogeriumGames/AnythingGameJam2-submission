using Unity.VisualScripting;
using UnityEngine;


public class pCamera: MonoBehaviour
{

    public Weapon weapons;
    float rotationX = 0.0f;
    public float Sensi;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        movingCamera();
        shooting();
        
    }

    void movingCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * Sensi;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * Sensi;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.parent.transform.parent.Rotate(Vector3.up * mouseX);
    }
    void shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapons.StdRifle.shoot();
        }
    }


}
