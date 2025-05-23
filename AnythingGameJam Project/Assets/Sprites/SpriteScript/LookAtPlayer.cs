using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using Zenject;

public class LookAtPlayer : MonoBehaviour
{
    public EnemyActions _enemyActions;
    [Inject]
    public PlayerStats _playerTransform;
    public SpriteRenderer spriteRender;
    public Sprite[] sprites;
    public bool canLookVertical;

    public float ShootTimer;
    private int shootingSpriteIndex = 8;
    [Inject]
    void Construct( PlayerStats playerstats)
    {
        _playerTransform = playerstats;
    }
    private void Start()
    {
        _enemyActions = GetComponentInParent<EnemyActions>();
        ShootTimer = 0;
        shootingSpriteIndex = 8;
    }

    void Update()
        {
        if (_playerTransform == null)
        {
            Debug.LogError("LookAtPlayer: Depend�ncia _playerTransform � nula!"); 
            if (_enemyActions == null)
            {
                Debug.LogError("_enemyActions � nula");
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

        if (ShootTimer > 0f)
            ShootTimer -= Time.deltaTime;

        if (_enemyActions.isShooting)
        {
            ShootTimer = _enemyActions.enemyAttackCooldown / 2;
            
        }

        if (ShootTimer > 0f)
        {
            ShootTimer -= Time.deltaTime;
            spriteRender.sprite = sprites[shootingSpriteIndex];
        }
        else
        {
            spriteRender.sprite = sprites[spriteAngles];
        }


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
