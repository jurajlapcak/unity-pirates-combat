using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public static bool isPanelShowing;
    public StarterAssetsInputs inputs;

    private void Start()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPanelShowing)
            {
                ResumeGame();
                return;
            }

            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPanelShowing = true;
        inputs.cursorLocked = false;
        Cursor.visible = true;
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        inputs.cursorLocked = true;
        Cursor.visible = false;
        panel.SetActive(false);
        Time.timeScale = 1f;
        isPanelShowing = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Debug.Log("restart");
        Application.Quit();
    }
    
    public void ToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }
}