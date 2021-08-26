using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string startScene = "Level1";

    public void StartGame() 
    {
        SceneManager.LoadScene(startScene);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
