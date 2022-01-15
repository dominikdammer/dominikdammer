/// <copyright> (c) Out of the box 2021 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveilanceDetection : MonoBehaviour
{
    #region serialized fields

    #endregion serialized fields

    #region fields
    // [Header("NAME1")]
    public Material searchingMat, spottedMat;

    string playerTag;
    bool follow;
    public Light spotlight;

    Transform detector;

    [Header("Trigger ID")]
    public int id;

    #endregion fields

    #region initialization and shutdown

    private void Start()
    {
        detector = transform.parent.GetComponent<Transform>();
        playerTag = GameObject.FindGameObjectWithTag("Player").tag;
        Debug.Log(playerTag);
        
    }



    #endregion initialization and shutdown

    #region handling

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (hit.collider.gameObject.tag == playerTag)
    //    {
    //        GameEvents.Instance.TriggerOn(id);
    //    }
            
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            Vector3 direction = other.transform.position - detector.position;
            RaycastHit hit; 

            //what happens on detection of player
            if (Physics.Raycast(detector.transform.position, direction.normalized, out hit, 1000))
            {
                //Debug.Log(hit.collider.name);

                if (hit.collider.gameObject.tag == playerTag)
                {
                    detector.GetComponentInParent<MeshRenderer>().material = spottedMat;
                    transform.parent.LookAt(other.transform);
                    spotlight.color = Color.red;
                    

                }
                else
                {
                    detector.GetComponentInParent<MeshRenderer>().material = searchingMat;
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == playerTag)
        {
            detector.GetComponentInParent<MeshRenderer>().material = searchingMat;
            spotlight.color = Color.white;

        }
    }
    #endregion handling


}
