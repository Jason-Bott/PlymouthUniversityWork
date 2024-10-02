using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ObjectiveManager : MonoBehaviour
{
    [Header("Objectives")]
    public GameObject objectiveBackground;
    public GameObject powerCore;
    public GameObject extract;

    [Header("Extraction")]
    public GameObject extractionBackground;
    public GameObject coward;

    [Header("Time")]
    public GameObject timer;
    public GameObject outOfTimeBackground;

    [Header("Death")]
    public GameObject deathBackground;

    [Header("Player Scripts")]
    public PlayerMovement playerMovementScript;
    public PlayerInteraction playerInteractionScript;

    [Header("Fade In")]
    public float fadeDuration = 2f;
    private Color targetColor;
    private Color initialColor;
    private float elapsedTime = 0f;


    private void Start()
    {
        //Turn objective UI on and display first objective
        objectiveBackground.SetActive(true);
        powerCore.SetActive(true);
    }

    public void NextObjective()
    {
        //Disable first objective
        powerCore.SetActive(false);
        //Enable final objective
        extract.SetActive(true);
    }

    public void Extract()
    {
        //If main objective is still active
        if(powerCore.activeSelf == true)
        {
            //Display coward message and remove after 2 seconds
            coward.SetActive(true);
            StartCoroutine(HideCowardAfterDelay(2f));
        }
        else
        {
            //Remove in game UI and enable extraction background
            objectiveBackground.SetActive(false);
            timer.SetActive(false);
            extractionBackground.SetActive(true);

            //Start fade in transition
            SetColor(extractionBackground.GetComponent<UnityEngine.UI.Image>());
            StartCoroutine(FadeInImage(extractionBackground.GetComponent<UnityEngine.UI.Image>()));

            //Disable some scripts
            playerMovementScript.enabled = false;
            playerInteractionScript.enabled = false;
        }
    }

    public void OutOfTime()
    {
        //Disable UI and enable correct background
        objectiveBackground.SetActive(false);
        timer.SetActive(false);
        outOfTimeBackground.SetActive(true);

        //Start fade in transition
        SetColor(outOfTimeBackground.GetComponent<UnityEngine.UI.Image>());
        StartCoroutine(FadeInImage(outOfTimeBackground.GetComponent<UnityEngine.UI.Image>()));

        //Disable some scripts
        playerMovementScript.enabled = false;
        playerInteractionScript.enabled = false;
    }

    public void Death()
    {
        //Disable UI and enable correct background
        objectiveBackground.SetActive(false);
        timer.SetActive(false);
        deathBackground.SetActive(true);

        //Start fade in transition
        SetColor(deathBackground.GetComponent<UnityEngine.UI.Image>());
        StartCoroutine(FadeInImage(deathBackground.GetComponent<UnityEngine.UI.Image>()));

        //Disable some scripts
        playerMovementScript.enabled = false;
        playerInteractionScript.enabled = false;
    }

    private IEnumerator HideCowardAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        coward.SetActive(false);
    }

    void SetColor(UnityEngine.UI.Image background)
    {
        initialColor = background.color;
        targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
    }

    IEnumerator FadeInImage(UnityEngine.UI.Image background)
    {
        //While time is less than duration add alpha (transparency) to the color
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            background.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            yield return null;
        }

        background.color = targetColor;
        StartCoroutine(LoadMenuAfterDelay(1f));
        yield return null;
    }

    IEnumerator LoadMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Menu");
    }
}
