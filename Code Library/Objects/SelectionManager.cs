/// <copyright> (c) Out of the box 2021 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    #region serialized fields

    [SerializeField]
    private Material highlightMaterial;

    //[SerializeField]
    //private Material normalMaterial;

    private Outline isOutlined;

    #endregion serialized fields

    #region fields
    // [Header("NAME1")]
    public Transform player;
    public float rayLenght = 5.0f;

    private bool isHighlighted = false;
    private Material changedMaterial;


    // [Header("NAME2")]

    #endregion fields

    #region initialization and shutdown


    private void Update()
    {
        Selection();
    }
    #endregion initialization and shutdown

    #region handling


    //man speichert sich das ursprüngliche material (normalMaterial) des objekts am anfang. 
    //man outlined das objekt bei kontakt.
    //man drückt contrl um das objekt zu markieren und setzt das material auf hightlightMaterial.
    //das outline kann nicht auf highlight existieren, weil sonst normalMat überschrieben wird.
    //man drückt v um alle markierungen aufzuheben.

    //controls aus dem script rausnehmen, da es ja ein mangager ist?

    public void Selection()
    {
        Debug.DrawRay(player.position, player.forward * rayLenght, Color.red);
        var ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        //select
        if (Physics.Raycast(ray, out hit, rayLenght, LayerMask.GetMask("entangleable")) && !isHighlighted)
        {
            var selection = hit;
            var selectionRenderer = selection.collider.gameObject.GetComponent<Renderer>();

            //normalMaterial = hit.collider.GetComponent<Renderer>().material;

            if (isOutlined == null || selection.collider.gameObject != isOutlined.gameObject)
            {
                if (isOutlined != null) Destroy(isOutlined);

                if (selection.collider.gameObject != null)
                {
                    var outline = selection.collider.gameObject.AddComponent<Outline>();
                    outline.OutlineMode = Outline.Mode.OutlineAll;
                    outline.OutlineColor = Color.blue;
                    outline.OutlineWidth = 5f;
                    isOutlined = outline;
                }
            }

            //mark controll
            if (Input.GetKeyDown(KeyCode.LeftControl) && isOutlined)
            {
                Debug.Log("Controll pressed");
                isHighlighted = true;
                selectionRenderer.material = highlightMaterial;
                changedMaterial = selectionRenderer.material;

            }

        }
        else
        {
            
            if (isOutlined != null) Destroy(isOutlined);
        }


        ////unmark controll
        //if (Input.GetKeyDown(KeyCode.V) && isHighlighted)
        //{
        //    Debug.Log("V was pressed");
        //    isHighlighted = false;
        //    changedMaterial = normalMaterial;
        //}
    }
    #endregion handling


}