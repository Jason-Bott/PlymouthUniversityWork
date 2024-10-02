using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneSpawner : MonoBehaviour
{
    public GameObject dronePrefab;
    public GameObject[] spawnTubes;
    private GameObject[] drones;

    void Start()
    {
        //Establise a GameObject array for drones to occupy, 1 for each spawn tube
        drones = new GameObject[spawnTubes.Length];

        //For each spawn tube, spawn a drone in that tube
        for(int i = 0; i < spawnTubes.Length; i++)
        {
            drones[i] = Instantiate(dronePrefab, spawnTubes[i].transform.position, Quaternion.identity);
            StartCoroutine(Decend(drones[i]));
        }
    }

    void Update()
    {
        //Check drone array to see if any have been destroyed
        for(int i = 0; i < drones.Length; i++)
        {
            if (drones[i] == null)
            {
                //Spawn drone in its tube
                drones[i] = Instantiate(dronePrefab, spawnTubes[i].transform.position, Quaternion.identity);
                StartCoroutine(Decend(drones[i]));
            }
        }
    }

    IEnumerator Decend(GameObject drone)
    {
        //Checks if drone has been killed and if it has reached its still decending
        while(drone != null && drone.transform.position.y > 0.075)
        {
            //Lower drone and wait
            float y = drone.transform.position.y - 0.005f;
            Vector3 newPos = new Vector3(drone.transform.position.x, y, drone.transform.position.z);
            drone.transform.position = newPos;
            yield return new WaitForSeconds(0.001f);
        }

        //If drone has been killed
        if(drone != null)
        {
            //Enable scripts so it can be reset
            drone.GetComponent<NavMeshAgent>().enabled = true;
            drone.GetComponent<DroneAI>().enabled = true;
        }
        yield return null;
    }
}
