using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IInteractable
{
    public void Interact(GameObject player);
    public bool IsPressed();
}

interface ICollectable
{
    public void Collect(GameObject player);
}

public class PlayerInteraction : MonoBehaviour
{
    [Header("RayCast")]
    public Transform playerCam;
    public float interactRange;

    [Header("User Interface")]
    public Image interactableTip;
    public Image collectableTip;

    private void Update()
    {
        //Fire ray in forward direction of where player is looking
        Ray ray = new Ray(playerCam.position, playerCam.forward);
        //If it hits something in the interaction range of the player
        if (Physics.Raycast(ray, out RaycastHit hitinfo, interactRange))
        {
            //Check if it is interactable
            if (hitinfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                //If not pressed display interactable tip and check if interact button is pressed
                if(interactObj.IsPressed() == false)
                {
                    interactableTip.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //Interact with the object and disable tool tip
                        interactObj.Interact(gameObject);
                        interactableTip.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                //Disable tool tip
                interactableTip.gameObject.SetActive(false);
            }

            //Check if it is collectable
            if(hitinfo.collider.gameObject.TryGetComponent(out ICollectable collectObj))
            {
                //Display collectable tip and check if collect button is pressed
                collectableTip.gameObject.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    //Collect the object
                    collectObj.Collect(gameObject);
                }
            }
            else
            {
                //Disable tool tip
                collectableTip.gameObject.SetActive(false);
            }
        }
        else
        {
            //Disable tool tips
            interactableTip.gameObject.SetActive(false);
            collectableTip.gameObject.SetActive(false);
        }
    }
}