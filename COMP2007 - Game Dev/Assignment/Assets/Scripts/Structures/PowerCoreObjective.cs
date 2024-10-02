using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerCoreObjective : MonoBehaviour
{
    public GameObject powerCore;
    int remainingHolders = 2;

    public void HolderDestroyed()
    {
        //Decrease holders by 1
        remainingHolders--;


        //If no holders left 
        if(remainingHolders == 0)
        {
            //Add a rigidbody to the power core
            Rigidbody rb = powerCore.AddComponent<Rigidbody>();

            //Add the collectable script to the power core
            Collectable collectable = powerCore.AddComponent<Collectable>();
            collectable.type = CollectableType.Objective;
        }
    }
}
