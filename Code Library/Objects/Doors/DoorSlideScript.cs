/// <copyright> (c) Out of the box 2022 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using LeanTween Package


//Observer Pattern Sender -> Handler -> !Listener!
public class DoorSlideScript : MonoBehaviour
{
    #region serialized fields
    [Header("Audio")]
    [SerializeField]
    private EventReference EventReferenceDoorOpen;
    [SerializeField]
    private EventReference EventReferenceDoorClose;

    #endregion serialized fields

    #region fields
    [Header("Door Behaviour")]
    public GameObject pivot;
    public bool defaultOpen = false;
    public float openingSize = 3f;
    public float openingTime = 1f;
    public enum DoorOpeningDirection { Up, Down, Left, Right, Forwards, Backwards };
    public DoorOpeningDirection direction;
    

    //public AnimationCurve animationCurve;

    [Header("Trigger ID")]
    public int id;

    private Vector3 openDirection;

    private FMOD.Studio.EventInstance instanceDoorOpen;
    private FMOD.Studio.EventInstance instanceDoorClose;

    #endregion fields

    #region initialization and shutdown


    private void Start()
    {       
        if (defaultOpen)
        {
            DoorOpenDirection();
            pivot.transform.position += (openDirection * openingSize);
        }
    }

    private void OnEnable()
    {
        GameEvents.Instance.onTriggerOn += OnDoorwayOpen;
        GameEvents.Instance.onTriggerOff += OnDoorwayClose;
    }

    private void OnDisable()
    {
        GameEvents.Instance.onTriggerOn -= OnDoorwayOpen;
        GameEvents.Instance.onTriggerOff -= OnDoorwayClose;
    }

    private void Update()
    {
        DoorOpenDirection();

    }

    #endregion initialization and shutdown

    #region handling

    //only works with set directions... so only with world facing in z
    private void DoorOpenDirection()
    {
        if (direction == DoorOpeningDirection.Up)
        {
            openDirection = Vector3.up;
        }
        else if (direction == DoorOpeningDirection.Down)
        {
            openDirection = Vector3.down;
        }
        else if(direction == DoorOpeningDirection.Left)
        {
            openDirection = Vector3.left;
        }
        else if(direction == DoorOpeningDirection.Right)
        {
             openDirection = Vector3.right;
        }
        else if (direction == DoorOpeningDirection.Forwards)
        {
            openDirection = Vector3.forward;
        }
        else if (direction == DoorOpeningDirection.Backwards)
        {
            openDirection = Vector3.back;
        }

    }


    private void OnDoorwayOpen(int id)
    {
        if (id == this.id)
        {
            if (defaultOpen)
            {
                CloseAnimation();
            }
            else
            {
                OpenAnimation();
            }
        }
    }

    private void OnDoorwayClose(int id)
    {
        if (id == this.id)
        {
            if (defaultOpen)
            {
                OpenAnimation();
            }
            else
            {
                CloseAnimation();
            }           
        }
    }




    private void OpenAnimation()
    {
        instanceDoorOpen = FMODUnity.RuntimeManager.CreateInstance(EventReferenceDoorOpen);
        instanceDoorOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        instanceDoorOpen.start();
        instanceDoorOpen.release();

        pivot.LeanMoveLocal(openDirection.normalized * openingSize, openingTime)
             .setEaseInQuad();
    }

    private void CloseAnimation()
    {       
        instanceDoorClose = FMODUnity.RuntimeManager.CreateInstance(EventReferenceDoorClose);
        instanceDoorClose.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        instanceDoorClose.start();
        instanceDoorClose.release();

        pivot.LeanMoveLocal(new Vector3(0, 0, 0), openingTime)
             .setEaseOutQuad();
    }

    public void PauseAnimation()
    {
        pivot.LeanPause();
    }

    public void ResumeAnimation()
    {
        pivot.LeanResume();
    }


    #endregion handling



}
