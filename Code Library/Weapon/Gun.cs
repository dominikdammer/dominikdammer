using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float pitchMin, pitchMax, volumeMin, volumeMax;


    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;
    public GameObject impactEffect;

    
    // plays sound and particle effect.
    // shoots ray where the bullet is going to land -> depending on target hit, do something
    //instantiate impact particle effect on target hit for 2 secs
    public void Shoot()
    {

        muzzleFlash.Play();
        RandomAudioPitch();

        

        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log("I HIT " + hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            BreakableWindow breakableWindow = hit.transform.GetComponent<BreakableWindow>();

            if (breakableWindow)
            {
                breakableWindow.breakWindow();
            }

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal* impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        
        }
    }

    private void RandomAudioPitch()
    {
        GetComponentInChildren<AudioSource>().pitch = Random.Range(pitchMin, pitchMax);
          shootSound.Play();


    }
}
