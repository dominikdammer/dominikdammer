using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    private float nextTimeToFire = 0f;
    public float fireRate = 15f;
    public Gun gun;

    // shoots gun and applies a fire rate
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            if (gun)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                gun.Shoot();
            }

        }


    }
}
