using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//popup text/overlay controll and UI click
public class PickupPopup : MonoBehaviour, IPointerClickHandler
{
    #region serialized fields

    [SerializeField]
    private GameObject popupKey;

    [SerializeField]
    private GameObject pickup;

    public Canvas canvas;

    #endregion serialized fields

    #region fields
    private bool popUpActive;
    #endregion fields

    #region initialization and shutdown


    #endregion initialization and shutdown

    #region handling

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
             popupKey.SetActive(true);
             popupKey.GetComponent<Billboard>().enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                popupKey.GetComponent<Billboard>().enabled = false;
                popupKey.SetActive(false);
                pickup.SetActive(true);

                if (!popUpActive && this.gameObject != pickup)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(EventReferencePickUp);
                    popUpActive = true;
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        { 
            pickup.SetActive(false);
            popupKey.SetActive(false);

            LeavePickup();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        pickup.SetActive(false);
        popupKey.GetComponent<Billboard>().enabled = true;
        popupKey.SetActive(true);
        LeavePickup();
    }

    private void LeavePickup()
    {
        if (popUpActive && this.gameObject != pickup )
        {
            FMODUnity.RuntimeManager.PlayOneShot(EventReferenceLayDown);
            popUpActive = false;
        }
        else
        {
            return;
        }
    }

    #endregion handling


}