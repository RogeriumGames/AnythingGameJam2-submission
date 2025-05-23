using UnityEngine;
using System.Collections;

public class HealingCube : MonoBehaviour
{
    public float healAmount = 20f;
    public float respawnTime = 30f;  // tempo para reaparecer em segundos
    public bool useRespawn = true;

    private Collider _collider;
    private Renderer _renderer;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.Health = Mathf.Min(playerStats.Health + healAmount, playerStats.MaxHealth);

            if (useRespawn)
            {
                // Desativa temporariamente
                StartCoroutine(RespawnRoutine());
            }
            else
            {
                // Destroi o objeto (sem respawn)
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator RespawnRoutine()
    {
        // Desativa o cubo (colisor e visual)
        _collider.enabled = false;
        _renderer.enabled = false;

        // Espera o tempo de respawn
        yield return new WaitForSeconds(respawnTime);

        // Reativa o cubo
        _collider.enabled = true;
        _renderer.enabled = true;
    }
}