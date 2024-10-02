using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [Header("Scripts To Enable")]
    public PlayerMovement playerMovement;
    public PlayerInteraction playerInteraction;
    public PlayerHealth playerHealth;
    public ObjectiveManager objectiveManager;
    public Timer timer;

    [Header("Labyrinth Generator")]
    public LabyrinthGenerator labyrinthGenerator;
    public EnemyManager enemyManager;

    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;

    int loadingSteps;

    void Start()
    {
        labyrinthGenerator._labyrinthWidth = LevelManager.GetWidth();
        labyrinthGenerator._labyrinthHeight = LevelManager.GetHeight();
        loadingSteps = labyrinthGenerator._labyrinthWidth * labyrinthGenerator._labyrinthHeight;
        labyrinthGenerator.StartGeneration(gameObject.GetComponent<LevelLoader>());
    }

    //50%
    public void BuildingShip()
    {
        loadingSlider.value += (0.5f / loadingSteps);
        loadingText.text = "Building Ship";
    }

    public void MakingLabyrinth()
    {
        loadingSlider.value = 0.6f;
        loadingText.text = "Creating Labyrinth";
    }

    public void MakingRooms()
    {
        loadingSlider.value = 0.7f;
        loadingText.text = "Adding Rooms";
    }

    public void Baking()
    {
        loadingSlider.value = 0.8f;
        loadingText.text = "Mapping Labyrinth";
    }

    public void Done()
    {
        loadingSlider.value = 1f;
        loadingText.text = "Done";

        loadingScreen.SetActive(false);

        enemyManager.Enable();

        playerMovement.enabled = true;
        playerInteraction.enabled = true;
        playerHealth.enabled = true;
        objectiveManager.enabled = true;
        timer.enabled = true;
    }
}
