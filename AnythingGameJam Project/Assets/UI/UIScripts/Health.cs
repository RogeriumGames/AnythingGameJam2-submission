using TMPro;
using UnityEngine;
using Zenject;

public class Health : MonoBehaviour
{
    [Inject]public PlayerStats _playerStats;
    public TextMeshProUGUI _healthText;

    void Start()
    {
        _healthText = GetComponent<TextMeshProUGUI>();

        _playerStats.onHealthChanged.AddListener(UpdateHealthText);
        Debug.Log("_playerStats é nulo? " + (_playerStats == null));
        // Debug para confirmar que Start rodou e listener foi adicionado
        Debug.Log("Listener de vida adicionado");

        UpdateHealthText(Mathf.RoundToInt(_playerStats.Health));
    }
    void UpdateHealthText(int currentHealth)
    {
        _healthText.text = currentHealth.ToString();
    }
}