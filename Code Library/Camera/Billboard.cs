using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    private Camera theCam;

    public bool useDynamicBillboard;


    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if(useDynamicBillboard)

        {
            transform.LookAt(theCam.transform.position);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y - 180, 0f);

        } 
        else
        {
            transform.rotation = theCam.transform.rotation;
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }                
    }
}
