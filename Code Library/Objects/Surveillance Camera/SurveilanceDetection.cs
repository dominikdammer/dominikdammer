/// <copyright> (c) Out of the box 2022 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveilanceDetection : MonoBehaviour
{
    //#region serialized fields
    //[Header("Audio")]
    //[SerializeField]
    //private EventReference EventReferenceRotate;
    //[SerializeField]
    //private EventReference EventReferenceAlarm;
    //[SerializeField]
    //private EventReference EventReferenceDetectionOn;
    //[SerializeField]
    //private EventReference EventReferenceDetectionOff;
    //#endregion serialized fields

    #region fields

    [Header("Material")]
    public Material searchingMat, spottedMat;

    string playerTag;
    public Light spotlight;
    public Transform alarm;

    Transform detector;


    [Header("Trigger ID")]
    public int id;

    //private FMOD.Studio.EventInstance Alarminstance;
    #endregion fields

    #region initialization and shutdown

    private void Start()
    {
        detector = transform.parent.GetComponent<Transform>();
        playerTag = GameObject.FindGameObjectWithTag("Player").tag;
    }

    #endregion initialization and shutdown

    #region handling

    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction = other.transform.position - detector.position;
        RaycastHit hit;

        if (Physics.Raycast(detector.transform.position, direction.normalized, out hit, 1000))
        {
            if (hit.collider.gameObject.tag == playerTag)
            {
                GameEvents.Instance.TriggerOn(id);

                //FMODUnity.RuntimeManager.PlayOneShot(EventReferenceDetectionOn);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == playerTag)
        {
            Vector3 direction = other.transform.position - detector.position;
            RaycastHit hit; 

            //what happens on detection of player
            if (Physics.Raycast(detector.transform.position, direction.normalized, out hit, 1000))
            {
                Debug.DrawRay(detector.transform.position, direction.normalized * 1000, Color.cyan);

                if (hit.collider.gameObject.tag == playerTag)
                {
                    alarm.GetComponentInParent<MeshRenderer>().material = spottedMat;
                    transform.parent.LookAt(other.transform);
                    spotlight.color = Color.red;
                }
                else
                {
                    alarm.GetComponentInParent<MeshRenderer>().material = searchingMat;
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == playerTag)
        {
            alarm.GetComponentInParent<MeshRenderer>().material = searchingMat;
            spotlight.color = Color.white;
            GameEvents.Instance.TriggerOff(id);

            //FMODUnity.RuntimeManager.PlayOneShot(EventReferenceDetectionOff);

        }
    }
    #endregion handling


}
