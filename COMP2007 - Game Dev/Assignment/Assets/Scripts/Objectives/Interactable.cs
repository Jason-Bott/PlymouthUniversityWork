using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    Button,
    Tutorial,
}

public class Interactable : MonoBehaviour, IInteractable
{
    public InteractableType type;

    [Header("Animations")]
    public Animator hangarDoorFront;
    public Animator hangarDoorBack;
    public Animator shipDoorOne;
    public Animator shipDoorTwo;
    public Animator shipDoorThree;

    [Header("Tutorial")]
    public Animator tutorialDoor;
    public TutorialManager tutorialManager;

    public void Interact(GameObject player)
    {
        if(type == InteractableType.Button)
        {
            if(gameObject.name == "HangarShip")
            {
                StartCoroutine(ShipDoors());
            }
            else if (gameObject.name == "HangarDoor")
            {
                StartCoroutine(HangarDoors());
            }
            else if (gameObject.name == "TrooperShip")
            {
                StartCoroutine(Extract(player));
            }
        }
        else if(type == InteractableType.Tutorial)
        {
            if(gameObject.name == "Door")
            {
                StartCoroutine(TutorialDoor());
            }
            if(gameObject.name == "Extract")
            {
                StartCoroutine(TutorialExtract(player));
            }
        }
    }

    public bool IsPressed()
    {
        return gameObject.GetComponent<Animator>().GetBool("isPressed");
    }

    IEnumerator HangarDoors()
    {
        gameObject.GetComponent<Animator>().SetBool("isPressed", true);
        gameObject.GetComponent<AudioSource>().Play();
        hangarDoorFront.SetBool("isOpen", !hangarDoorBack.GetBool("isOpen"));
        hangarDoorBack.SetBool("isOpen", !hangarDoorBack.GetBool("isOpen"));

        yield return new WaitForSeconds(10f);
        gameObject.GetComponent<Animator>().SetBool("isPressed", false);
    }

    IEnumerator ShipDoors()
    {
        gameObject.GetComponent<Animator>().SetBool("isPressed", true);
        gameObject.GetComponent<AudioSource>().Play();
        shipDoorOne.SetBool("isOpen", !shipDoorOne.GetBool("isOpen"));
        shipDoorTwo.SetBool("isOpen", !shipDoorTwo.GetBool("isOpen"));
        shipDoorThree.SetBool("isOpen", !shipDoorThree.GetBool("isOpen"));

        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().SetBool("isPressed", false);
    }

    IEnumerator Extract(GameObject player)
    {
        gameObject.GetComponent<Animator>().SetBool("isPressed", true);
        gameObject.GetComponent<AudioSource>().Play();
        player.GetComponent<ObjectiveManager>().Extract();

        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().SetBool("isPressed", false);
    }

    IEnumerator TutorialDoor()
    {
        gameObject.GetComponent<Animator>().SetBool("isPressed", true);
        gameObject.GetComponent<AudioSource>().Play();
        tutorialDoor.SetBool("isOpen", !tutorialDoor.GetBool("isOpen"));
        tutorialManager.ButtonPressed();

        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().SetBool("isPressed", false);
    }

    IEnumerator TutorialExtract(GameObject player)
    {
        gameObject.GetComponent<Animator>().SetBool("isPressed", true);
        gameObject.GetComponent<AudioSource>().Play();
        if (tutorialManager.progress == 1)
        {
            player.GetComponent<ObjectiveManager>().Extract();
        }

        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().SetBool("isPressed", false);
    }
}
