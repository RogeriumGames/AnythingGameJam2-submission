using UnityEngine;

public class CameraHolder : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0, 1.6f, 0);

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }

}
