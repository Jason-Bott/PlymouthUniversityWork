using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    public float damage = 10f;
    public float shotRate;
    private float shotRateTime;

    public Transform laserOrigin;
    public GameObject laserPrefab;

    public ParticleSystem barrelFlash;
    public AudioSource shotSound;

    void Update()
    {
        //Check if the fire button is currently held and if it is time to shoot another laser
        if(Input.GetButton("Fire1") && Time.time > shotRateTime)
        {
            ShootLaser();
            //Set next time a laser can be shot
            shotRateTime = Time.time + shotRate;
        }
    }

    void ShootLaser()
    {
        //Play sound effect
        shotSound.Play();
        //Play visual effect
        barrelFlash.Play();
        //Spawn Laser
        GameObject laser = GameObject.Instantiate(laserPrefab, laserOrigin.position, laserOrigin.rotation);
        //Set its damage
        laser.GetComponent<LaserController>().damage = damage;
        //Destroy after 2 seconds
        GameObject.Destroy(laser, 2f);
    }
}
