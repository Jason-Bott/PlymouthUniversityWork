using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI timeText;
    public float timeLimit;

    float timeRemaining;

    void Start()
    {
        //Set initial time
        timeRemaining = timeLimit * 60;
    }

    void Update()
    {
        //If there is time left
        if(timeRemaining > 0)
        {
            //Reduce and display time
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            //Start out of time sequence
            player.GetComponent<ObjectiveManager>().OutOfTime();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        //If less than zero, set to zero to prevent negative time left
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        //Calculate minutes and seconds from seconds left
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //Display time
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
