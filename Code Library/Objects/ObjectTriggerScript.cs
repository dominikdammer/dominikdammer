/// <copyright> (c) Out of the box 2022 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using UnityEngine;

//subject
public class ObjectTriggerScript : MonoBehaviour
{
    #region serialized fields

    [SerializeField]
    private Collider activateCollider;

    [SerializeField]
    private Collider deactivateCollider;

    #endregion serialized fields

    #region fields
    [Header("Materials")]
    public Material triggerOnMaterial;
    public Material triggerOffMaterial;
    

    [Header("Trigger ID")]
    public int id;
    #endregion fields

    #region initialization and shutdown
    private void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = triggerOffMaterial;
    }

    #endregion initialization and shutdown

    #region handling

    //check if collider is within point of activation collider
    public static bool IsPointWithinCollider(Collider collider, Vector3 point)
    {
        return (collider.ClosestPoint(point) - point).sqrMagnitude < Mathf.Epsilon * Mathf.Epsilon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = triggerOnMaterial;

            //Send Message to all triggers that trigger is on
            if (other.transform.tag == this.transform.tag)
            {
                GameEvents.Instance.TriggerOn(id);
            }          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = triggerOffMaterial;

            //Unsend message
            if (other.transform.tag == this.transform.tag)
            {
                GameEvents.Instance.TriggerOff(id);
            }          
        }
    }

    #endregion handling


}
