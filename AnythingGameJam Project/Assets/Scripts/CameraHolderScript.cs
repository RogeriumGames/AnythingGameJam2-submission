using UnityEngine;

public class CameraHolderScript : MonoBehaviour
{
    public Transform target;           // Jogador
    public Vector3 cameraOffset = new Vector3(0, 1.7f, 0); // Altura da câmera

    void LateUpdate()
    {
        transform.position = target.position + cameraOffset;
    }
}
