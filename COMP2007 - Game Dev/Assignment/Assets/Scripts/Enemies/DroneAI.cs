using System;
using UnityEngine;
using UnityEngine.AI;

public class DroneAI : MonoBehaviour
{
    LabyrinthGenerator labyrinthGenerator;
    GameObject player;

    NavMeshAgent agent;

    [SerializeField]
    LayerMask groundLayer, playerLayer;

    //Walking
    Vector3 destinationPoint;
    bool walkPointSet;

    //Finding
    [SerializeField]
    float coneAngle = 100f;
    [SerializeField]
    Camera droneCam;
    bool playerInSight;

    //Looking
    [SerializeField]
    GameObject droneParts;
    Quaternion initialRelativeRotation;
    bool wasAttacking;
    Transform lastPosition;

    //Shooting
    public GameObject laserPrefab;
    public Transform leftGun;
    public ParticleSystem leftFlash;
    public Transform rightGun;
    public ParticleSystem rightFlash;
    int weapon = 1;
    public float fireRate;
    float fireTime;

    void Start()
    {
        //Set variables
        initialRelativeRotation = Quaternion.Inverse(transform.rotation) * droneParts.transform.rotation;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        labyrinthGenerator = GameObject.Find("LabyrinthGenerator").GetComponent<LabyrinthGenerator>();
    }

    void Update()
    {
        //Check if the player can be seen
        Vector3 directionToPlayer = player.transform.position - droneCam.transform.position;

        float angleToPlayer = Vector3.Angle(droneCam.transform.forward, directionToPlayer);

        if (angleToPlayer <= coneAngle * 0.5f)
        {
            //Checks that there is nothing between the drone and player
            RaycastHit hit;
            if (Physics.Raycast(droneCam.transform.position, directionToPlayer, out hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    playerInSight = true;
                }
                else
                {
                    playerInSight = false;
                }
            }
            else
            {
                playerInSight = true;
            }
        }
        else
        {
            playerInSight = false;
        }

        //Decide on drone state (Attacking or Patroling)
        if (!playerInSight)
        {
            //If the drone was just attacking set the target point to where the player was spotted
            if (wasAttacking)
            {
                wasAttacking = false;
                destinationPoint = GetLastPosition();
            }
            Patrol();
            ResetRotation();
        }
        if (playerInSight)
        {
            Attack();
        }
    }

    void Attack()
    {
        //Change bool
        wasAttacking = true;
        //Variable for if player moves out of vision
        lastPosition = player.transform;
        //Set destination to current position (Stand Still)
        agent.SetDestination(transform.position);
        //Aim at player
        RotateToPlayer();
        Shoot();
    }

    void Patrol()
    {
        //If no current destination find one
        if (!walkPointSet)
        {
            SearchForDestination();
        }
        //If the drone has a destination set it
        if (walkPointSet)
        {
            agent.SetDestination(destinationPoint);
        }
        //If close to destination reset bool so a new one is set
        if (Vector3.Distance(transform.position, destinationPoint) < 5)
        {
            walkPointSet = false;
        }
    }

    void SearchForDestination()
    {
        //Get a random x and z
        float x = UnityEngine.Random.Range(0, labyrinthGenerator._labyrinthWidth * labyrinthGenerator._labyrinthCellScale);
        float z = UnityEngine.Random.Range(0, labyrinthGenerator._labyrinthHeight * labyrinthGenerator._labyrinthCellScale);

        //Set destination to the random point
        destinationPoint = new Vector3(x, transform.position.y, z);

        //Create a path to the destination
        NavMeshPath path = new NavMeshPath();
        //Check if the path can be travelled
        bool isPathValid = agent.CalculatePath(destinationPoint, path);

        //Set destination to true if the path can be fully travelled
        if (isPathValid && path.status == NavMeshPathStatus.PathComplete)
        {
            walkPointSet = true;
        }
    }

    void RotateToPlayer()
    {
        droneParts.transform.LookAt(player.transform.position);
    }

    void ResetRotation()
    {
        droneParts.transform.rotation = transform.rotation * initialRelativeRotation;
    }

    Vector3 GetLastPosition()
    {
        float x = 0;
        float z = 0;

        for (int i = 0; i < labyrinthGenerator._labyrinthWidth; i++)
        {
            if (i * labyrinthGenerator._labyrinthCellScale <= lastPosition.position.x && lastPosition.position.x <= (i + 1) * labyrinthGenerator._labyrinthCellScale)
            {
                x = ((float)labyrinthGenerator._labyrinthCellScale / 2) + (i * (float)labyrinthGenerator._labyrinthCellScale);
            }
        }

        for (int i = 0; i < labyrinthGenerator._labyrinthHeight; i++)
        {
            if (i * labyrinthGenerator._labyrinthCellScale <= lastPosition.position.z && lastPosition.position.z <= (i + 1) * labyrinthGenerator._labyrinthCellScale)
            {
                z = ((float)labyrinthGenerator._labyrinthCellScale / 2) + (i * (float)labyrinthGenerator._labyrinthCellScale);
            }
        }

        return new Vector3(x, transform.position.y, z);
    }

    void Shoot()
    {
        //Ensures shots fired at correct fire rate
        if (Time.time > fireTime)
        {
            //Check which weapon to shoot from
            if (weapon == 1)
            {
                //Play animations and shoot laser from left weapon
                leftFlash.Play();
                GameObject laser = GameObject.Instantiate(laserPrefab, leftGun.position, leftGun.rotation);
                GameObject.Destroy(laser, 2f);

                //Set next time a weapon can be fired and which weapon to fire
                fireTime = Time.time + fireRate;
                weapon = 2;
            }
            else if (weapon == 2)
            {
                //Play animations and shoot laser from right weapon
                rightFlash.Play();
                GameObject laser = GameObject.Instantiate(laserPrefab, rightGun.position, rightGun.rotation);
                GameObject.Destroy(laser, 2f);

                //Set next time a weapon can be fired and which weapon to fire
                fireTime = Time.time + fireRate;
                weapon = 1;
            }
        }
    }
}
