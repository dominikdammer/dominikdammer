/// <copyright> (c) Out of the box 2022 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSlideScript : MonoBehaviour
{
    #region serialized fields

    #endregion serialized fields

    #region fields
    [Header("Door Behaviour")]
    public GameObject pivot;
    public float openingSize = 3f;
    public float openingTime = 1f;
    public enum DoorOpeningDirection { Up, Down, Left, Right, Forwards, Backwards };
    public DoorOpeningDirection direction;

    [Header("Trigger ID")]
    public int id;


    private Vector3 openDirection;


    #endregion fields

    #region initialization and shutdown

    private void Start()
    {
       GameEvents.Instance.onTriggerOn += OnDoorwayOpen;
       GameEvents.Instance.onTriggerOff += OnDoorwayClose;
    }

    private void Update()
    {
        DoorOpenDirection();
    }

    #endregion initialization and shutdown

    #region handling

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
            openDirection = Vector3.forward;
        }
        else if(direction == DoorOpeningDirection.Right)
        {
             openDirection = Vector3.back;
        }
        else if (direction == DoorOpeningDirection.Forwards)
        {
            openDirection = Vector3.left;
        }
        else if (direction == DoorOpeningDirection.Backwards)
        {
            openDirection = Vector3.right;
        }
    }


    private void OnDoorwayOpen(int id)
    {
        if (id == this.id)
        {
            if (openingSize > openingSize + 0.1f)
            {
                pivot.LeanCancel();
            }
            else
            {
                pivot.LeanMoveLocal(openDirection.normalized * openingSize, openingTime)
                     .setEaseInQuad();
            }
        }
    }

    private void OnDoorwayClose(int id)
    {
        if (id == this.id)
        {
            pivot.LeanMoveLocal(new Vector3(0, 0, 0), openingTime)
                 .setEaseOutQuad();               
        }
    }



    #endregion handling


    
}
