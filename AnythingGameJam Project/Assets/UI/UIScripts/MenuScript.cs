using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pauseMenu;

    public void PauseGame()
    {
        Time.timeScale = 0f;                  
        pauseMenu.SetActive(true);              
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;                      
        pauseMenu.SetActive(false);              
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
                PauseGame();
            else
                ResumeGame();
        }
    }
    public void RestartManually()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
