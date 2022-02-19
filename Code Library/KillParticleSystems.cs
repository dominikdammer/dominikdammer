using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParticleSystems : MonoBehaviour
{
    [SerializeField] 
    private float timeToKill;
    private float timePassed = 0f;


    // Update is called once per frame
    void Update()
    {
        timePassed += 1 * Time.deltaTime;
        if(timePassed >= timeToKill) {
            Destroy(gameObject);
        }
    }
}
