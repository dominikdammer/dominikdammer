using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{
    public GameObject destroyedVersion;
    public float breakForce;
    
    //spawn a new cell fractured object on trigger and apply outward force

    private void OnMouseDown()
    {
        GameObject frac = Instantiate(destroyedVersion, transform.position, transform.rotation);
        
        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(force);
        }
        
        Destroy(gameObject);
    }
}
