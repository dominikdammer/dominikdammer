using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [SerializeField]
    GunHolder gunHolder;

    public float distance = 10f;
    public Transform equipPosition;
    GameObject currentWeapon;
    public GameObject pickUpText;
    private Suicide suicide;


    bool canGrab;
    public bool checkForGun;


    // set bool to false, check for grab and pickup when e is pressed
    void Update()
    {
        checkForGun = false;
        CheckGrab();

        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
                PickUp();
        }
    }

    //shoots a ray, check for specific tag and set bool to true
    public void CheckGrab()
    {
        RaycastHit hit;
        

        if(Physics.Raycast(transform.position,transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "CanGrab")
            {
                //Debug.Log("I can grab it!"); 
                currentWeapon = hit.transform.gameObject;
                canGrab = true;
            } 
        }
        else
            canGrab = false;
    }


    //set object hit by raycast to predefined equipPosition, change the transform and roation, set kinematic to true, destroy the text, set bool true
    public void PickUp()
    {
        currentWeapon.transform.position = equipPosition.position;
        currentWeapon.transform.parent = equipPosition;
        currentWeapon.transform.localEulerAngles = new Vector3(270f, 180f, 270f);
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
        gunHolder.gun = currentWeapon.GetComponent<Gun>();

        Destroy(pickUpText);
        checkForGun = true;

        Debug.Log(currentWeapon.GetComponent<Gun>());
        //Debug.Log("picked it up");
    }


}
