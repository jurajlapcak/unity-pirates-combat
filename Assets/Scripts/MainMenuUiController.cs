using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUiController : MonoBehaviour
{
    public GameObject panel;

    private void Awake()
    {
        panel.SetActive(false);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowPanel()
    {Debug.Log("showpanel");
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}