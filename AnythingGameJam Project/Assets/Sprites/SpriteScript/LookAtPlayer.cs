using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using Zenject;

public class LookAtPlayer : MonoBehaviour
{
    [Inject]
    public EnemyActions _enemyActions;
    [Inject]
    public PlayerStats _playerTransform;

    public SpriteRenderer spriteRender;
    public Sprite[] sprites;
    public bool canLookVertical;

    [Inject]
    void construct(EnemyActions enemyactions, PlayerStats playerstats)
    {
        _enemyActions = enemyactions;
        _playerTransform = playerstats;
    }

        void Update()
        {
        if (_playerTransform == null)
        {
            Debug.LogError("LookAtPlayer: Dependência _playerTransform é nula!"); 
            if (_enemyActions == null)
            {
                Debug.LogError("_enemyActions é nula");
            }
            return;
        }
        float distanceX = _playerTransform.transform.position.x - transform.position.x;
        float distanceZ = _playerTransform.transform.position.z - transform.position.z;

        Vector3 forward = transform.parent.forward;
        Vector3 toPlayer = (_playerTransform.transform.position - transform.parent.position).normalized;
        forward.y = 0;
        toPlayer.y = 0;

        float angulo = Vector3.SignedAngle(forward, toPlayer, Vector3.up);

        if (angulo < 0) angulo += 360;

        int spriteAngles = Mathf.FloorToInt((angulo + 22.5f) / 45f) % 8;
        spriteRender.sprite = sprites[spriteAngles];

        if (Input.GetKeyDown(KeyCode.G))
        Debug.Log(angulo);
        if (!_enemyActions.playerIsSeen)
        {
            if (canLookVertical)
            {
                transform.LookAt(_playerTransform.transform);
            }
            else
            {
                Vector3 modifiedTarget = _playerTransform.transform.position;
                modifiedTarget.y = transform.position.y;

                transform.LookAt(modifiedTarget);
            }
        }

    }
}
