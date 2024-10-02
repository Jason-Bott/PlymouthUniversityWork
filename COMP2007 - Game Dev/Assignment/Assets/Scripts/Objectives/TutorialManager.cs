using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Script")]
    public GameObject welcome;
    public GameObject movement;
    public GameObject jumping;
    public GameObject button;
    public GameObject gun;
    public GameObject drones;
    public GameObject task;
    public GameObject goodJob;
    public GameObject topLeft;
    public GameObject powerCore;
    public GameObject collect;
    public GameObject shortTime;
    public GameObject timer;
    public GameObject extract;
    public GameObject makeYourWay;

    [Header("Other")]
    public GameObject pistol;
    public GameObject unlockDrone;
    public Animator droneDoor;
    public ObjectiveManager objectiveManager;
    public GameObject holderTop;
    public GameObject holderBot;
    public GameObject powerCoreObject;
    public GameObject timerObject;

    [Header("Progress")]
    public int progress = 0;

    bool droneDestroyed;
    bool holdersDestroyed;
    bool powerCoreCollected;

    private void Start()
    {
        droneDestroyed = false;
        holdersDestroyed = false;
        powerCoreCollected = false;
        welcome.SetActive(true);
        StartCoroutine(HideMessagedAfterDelay(3f, welcome));
        StartCoroutine(NextMessageAfterDelay(4f, movement, 3f));
        StartCoroutine(NextMessageAfterDelay(8f, jumping, 3f));
    }

    private void Update()
    {
        if(unlockDrone == null && droneDestroyed == false)
        {
            droneDestroyed = true;
            droneDoor.SetBool("isOpen", true);
            goodJob.SetActive(true);
            StartCoroutine(HideMessagedAfterDelay(3f, goodJob));
            StartCoroutine(NextMessageAfterDelay(4f, topLeft, 3f));
            StartCoroutine(NextMessageAfterDelay(8f, powerCore, 5f));
        }

        if(holderTop == null && holderBot == null && holdersDestroyed == false)
        {
            holdersDestroyed = true;
            collect.SetActive(true);
            StartCoroutine(HideMessagedAfterDelay(3f, collect));
        }

        if(powerCoreObject == null && powerCoreCollected == false)
        {
            powerCoreCollected = true;
            shortTime.SetActive(true);
            StartCoroutine(HideMessagedAfterDelay(4f, shortTime));
            StartCoroutine(NextMessageAfterDelay(5f, timer, 3f));
            StartCoroutine(NextMessageAfterDelay(9f, extract, 4f));
            StartCoroutine(NextMessageAfterDelay(14f, makeYourWay, 4f));
        }
    }

    public void ButtonPressed()
    {
        button.SetActive(true);
        StartCoroutine(HideMessagedAfterDelay(4f, button));
        StartCoroutine(NextMessageAfterDelay(5f, gun, 3f));
        StartCoroutine(GiveGunAfterDelay(5f));
        StartCoroutine(NextMessageAfterDelay(9f, drones, 5f));
        StartCoroutine(NextMessageAfterDelay(15f, task, 4f));
    }

    private IEnumerator HideMessagedAfterDelay(float delay, GameObject message)
    {
        yield return new WaitForSeconds(delay);
        message.SetActive(false);
    }

    private IEnumerator NextMessageAfterDelay(float delay, GameObject message, float messageDelay)
    {
        yield return new WaitForSeconds(delay);

        if(message.name == "Top Left")
        {
            objectiveManager.enabled = true;
        }

        if(message.name == "Timer")
        {
            timerObject.SetActive(true);
        }

        if(message.name == "Make Your Way")
        {
            progress = 1;
        }

        message.SetActive(true);
        StartCoroutine(HideMessagedAfterDelay(messageDelay, message));
    }

    private IEnumerator GiveGunAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        pistol.SetActive(true);
    }
}
