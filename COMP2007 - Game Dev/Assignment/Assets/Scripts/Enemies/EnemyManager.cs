using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] droneSpawners;
    private DroneSpawner[] droneSpawnerScripts;

    public void Enable()
    {
        //Find out how many drone spawners have been generated
        int size = 0;
        for(int i = 0; i < droneSpawners.Length; i++)
        {
            if (droneSpawners[i] != null)
            {
                size++;
            }
        }

        //Create an array of that size
        droneSpawnerScripts = new DroneSpawner[size];

        int droneSpawner = 0;
        for(int i = 0; i < droneSpawnerScripts.Length; i++)
        {
            //Check if a spawner is contained
            if (droneSpawners[droneSpawner] != null)
            {
                //Get the spawners script and enable it
                droneSpawnerScripts[i] = droneSpawners[droneSpawner].GetComponent<DroneSpawner>();
                droneSpawnerScripts[i].enabled = true;

                droneSpawner++;
            }

            //If no spawner increment the array containing original spawners but not the for loop altering scripts
            else
            {
                droneSpawner++;
                i--;
            }
            
        }
    }
}
