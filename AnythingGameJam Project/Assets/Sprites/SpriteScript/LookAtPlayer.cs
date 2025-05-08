using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class LookAtPlayer : MonoBehaviour
{
    public EnemyActions enemyActions;
    public Transform Player;
    public SpriteRenderer spriteRender;
    public Sprite[] sprites;
    public bool canLookVertical;
    void Update()
    {
        float distanceX = Player.transform.position.x - transform.position.x;
        float distanceZ = Player.transform.position.z - transform.position.z;

        Vector3 forward = transform.parent.forward;
        Vector3 toPlayer = (Player.position - transform.parent.position).normalized;
        forward.y = 0;
        toPlayer.y = 0;

        float angulo = Vector3.SignedAngle(forward, toPlayer, Vector3.up);

        if (angulo < 0) angulo += 360;

        int spriteAngles = Mathf.FloorToInt((angulo + 22.5f) / 45f) % 8;
        spriteRender.sprite = sprites[spriteAngles];

        if (Input.GetKeyDown(KeyCode.G))
        Debug.Log(angulo);
        if (!enemyActions.playerIsSeen)
        {
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
}
