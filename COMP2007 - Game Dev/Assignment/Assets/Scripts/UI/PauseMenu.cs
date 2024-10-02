using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public PlayerMovement playerMovement;
    public Blaster blaster;

    void Update()
    {
        //If key is pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Open menu
            pauseMenu.SetActive(true);

            //Make cursor visible and usable
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            //Pause the game
            Time.timeScale = 0;
            playerMovement.enabled = false;
            blaster.enabled = false;
        }
    }

    public void ContinueGame()
    {
        //Close menu
        pauseMenu.SetActive(false);

        //Make cursor invisible and locked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Unpause the game
        Time.timeScale = 1;
        playerMovement.enabled = true;
        blaster.enabled = true;
    }

    public void Settings()
    {
        //Open settings
    }

    public void ExitToMenu()
    {
        //Unpause the game so code can be ran
        Time.timeScale = 1;
        playerMovement.enabled = true;
        blaster.enabled = true;

        //Open main menu scene
        SceneManager.LoadScene("Menu");
    }
}
