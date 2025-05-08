using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class LookAtPlayer : MonoBehaviour
{
    public Transform Player;
    public SpriteRenderer spriteRender;
    public Sprite[] sprites;
    public bool canLookVertical;
    void Update()
    {
        
        float distanceX = Player.transform.position.x - transform.position.x;
        float distanceZ = Player.transform.position.z - transform.position.z;

        float angulo = Mathf.Atan2(distanceZ, distanceX) * Mathf.Rad2Deg;

        if (angulo < 0) angulo += 360;

        int spriteAngles = Mathf.RoundToInt((angulo / 45f)) -1;
        spriteRender.sprite = sprites[spriteAngles];

        if (Input.GetKeyDown(KeyCode.G))
            Debug.Log(angulo);

        if (canLookVertical)
        {
            transform.LookAt(Player.transform);
        }
        else
        {
            Vector3 modifiedTarget = Player.position;
            modifiedTarget.y = transform.position.y; 

            transform.LookAt(modifiedTarget);
        }
    }
}
