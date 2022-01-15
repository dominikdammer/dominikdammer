using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Suicide : MonoBehaviour
{

    public float range = 100f;

    public GameObject FirstPersonController;
    public GameObject myGun;
    public GameObject myEquipPosition;

    public PickUpScript pickUpScript;
    public Camera fpsCam;


    //checks if "checkForGun" in the PickUpScript is true and then applies the gameObject to myGun
    void Update()
    {
        if (pickUpScript.checkForGun)
        {
            myGun = GameObject.Find("Gun");
        }
        myEquipPosition = GameObject.Find("EquipPosition");
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 10, Color.yellow);
        CheckForSuicide();
    }


    //shoots a raycast, then check if mouse button is pressed and myGun is filled to change gun position and rotation
    public void CheckForSuicide()
    {
        RaycastHit hitEnemy;
        
        if (Input.GetButtonDown("Fire1") && myGun != null)
        { 
             //moved detection collider to ignore raycast layer so this raycast only hits object with the true if condition
             if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitEnemy, range) && hitEnemy.transform.tag == "Enemy")
             {
                    Debug.Log("I HIT " + hitEnemy.transform.name + " + " + hitEnemy.collider);
                    Debug.Log("ALSO " + hitEnemy.collider);
                    myEquipPosition.transform.localPosition = new Vector3(0.53f, -0.046f, 0.87f);
                    myGun.transform.localEulerAngles = new Vector3(280f, 85f, 220f);

                    StartCoroutine(wait());

                    


            }
        }
    }

    //wait for 2 seconds then proceed with code
    IEnumerator wait()
    {
        Debug.Log("wait timer started");
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
    }
    
}
