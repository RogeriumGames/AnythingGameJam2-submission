using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject deathScreen; // Painel da tela de morte
    public float delayToRestart = 5f;

    private bool hasDied = false;

    void Update()
    {
        if (!hasDied && playerStats.IsDead)
        {
            hasDied = true;
            deathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
    public void RestartManually()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}