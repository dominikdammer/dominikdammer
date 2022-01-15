/// <copyright> (c) Out of the box 2022 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using UnityEngine;

//object
public class DoorController : MonoBehaviour
{
    #region serialized fields

    #endregion serialized fields

    #region fields
    [Header("Door Behaviour")]
    public GameObject pivot;
    public float openingTime = 1f;
    public enum DoorOpeningDirection { Forwards, Backwards };
    public DoorOpeningDirection direction;

    private float rotation = 120f;
    private Vector3 openDirection;
    private Vector3 closeDirection;

    [Header("Trigger ID")]
    public int id;



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
        if (direction == DoorOpeningDirection.Forwards)
        {
            openDirection = Vector3.up;
            closeDirection = Vector3.down;          
        }
        else if (direction == DoorOpeningDirection.Backwards)
        {
            openDirection = Vector3.down;
            closeDirection = Vector3.up;
           
        }

    }


    private void OnDoorwayOpen(int id)
    {
        if (id == this.id)
        {
            if (rotation > rotation + 0.1f)
            {
                pivot.LeanCancel();
            }
            else
            {
                pivot.LeanRotateAroundLocal(openDirection, rotation, openingTime)
                     .setEaseInQuad();           
            }
        }
    }

    private void OnDoorwayClose(int id)
    {
        if (id == this.id)
        {
            if (rotation < -0.1f)
            {
                pivot.LeanCancel();
            }
            else
            {
                pivot.LeanRotateAroundLocal(closeDirection, rotation, openingTime)
                     .setEaseOutQuad();
            }
        }
    }




    #endregion handling


}


