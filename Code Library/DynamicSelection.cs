/// <copyright> (c) Out Of The Box 2022 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url>  </url>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//using simple outline package
public class DynamicSelection : MonoBehaviour
{
    #region serialized fields

    #endregion serialized fields

    #region fields
    [Header("Raycast")]

    public Transform startRay;
    public int raycastLenght;
    public float sphereCastRadius = 3f;

    private Outline isOutlined;
    private bool isHighlighted = false;

    #endregion fields

    #region initialization and shutdown

    private void Update()
    {
        SpherecastSelection();
    }
    #endregion initialization and shutdown

    #region handling
    void SpherecastSelection()
    {
        float minDistance = 10;
        //start position, radius, start direction (vector), layer, if collider is trigger
        RaycastHit[] hits = Physics.SphereCastAll(startRay.position, sphereCastRadius, startRay.up, 15f, LayerMask.GetMask("entangleable"), QueryTriggerInteraction.Ignore);

        //isHighlighted does nothing atm, but is maybe needed for keypress changes
        if (!isHighlighted)
        {
            foreach (RaycastHit target in hits)
            {
                float currentDistance = Vector3.Distance(transform.position, target.transform.position);
                //Debug.Log(target.transform.name);

                if (currentDistance < minDistance && !isHighlighted)
                {
                    targetObject = target.transform.gameObject;
                    minDistance = currentDistance;
                    //Array.Clear(hits, 0, hits.Length);
                }
            }

            if (targetObject != null)
            {
                if (isOutlined == null || targetObject.name != isOutlined.name)
                {
                    if (isOutlined != null)
                    {
                        Destroy(isOutlined);
                    }

                    var outline = targetObject.AddComponent<Outline>();
                    outline.OutlineMode = Outline.Mode.OutlineAll;
                    outline.OutlineColor = Color.blue;
                    outline.OutlineWidth = 5f;
                    isOutlined = outline;
                }
            }

            //DOTO
            //There is a bug, where the outline is not deleted, when no longer hitting. Solution must be something in if statement below

            if (hits.Length < 1)
            {
                //if (isOutlined != null) Destroy(isOutlined);
                targetObject = null;
                Array.Clear(hits, 0, hits.Length);
            }
        }
    }

    //simple vizualisation
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startRay.position, sphereCastRadius);
    }
    #endregion handling

}


