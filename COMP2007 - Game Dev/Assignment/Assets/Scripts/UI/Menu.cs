using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    LevelManager levelManager;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void StartGame()
    {
        //Get level to load
        int size = levelManager.GetLevel();

        //Check what to load
        if(size > 0)
        {
            SceneManager.LoadScene("Labyrinth");
        }
        else if(size == -1)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {

        }
    }

    public void OpenSettings()
    {
        //Open settings
    }

    public void QuitGame()
    {
        //Quit application
        Application.Quit();
    }
}
