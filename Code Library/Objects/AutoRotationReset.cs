/// <copyright> (c) Out of the box 2022 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using UnityEngine;

public class AutoRotationReset : MonoBehaviour
{
    #region serialized fields

    [SerializeField, Range (0,100)]
    public float rotationBeforeFlip = 30.0f;

    [SerializeField, Range(0, 2)]
    public float rotationSpeed = 2f; //"waittime"

    #endregion serialized fields

    #region fields
    private Vector3 boxRotation;
    private float targetRotationX;
    private float targetRotationZ;
    #endregion fields

    #region initialization and shutdown


    /// <summary>
    /// / Bug in rotation, where the target position is not 0 but actually only the targetRotation 
    ///  Solution:
    ///   enum mit einer while loop um abbruch bedingnung zu setzen
    ///   bool ob die coroutine an ist, damit sie nur einmal aktiv ist.
    ///   coroutine statt update
    ///   am ende hartcoden das ziel (0,0,0)
    /// </summary>



    private void Start()
    {
        boxRotation = this.gameObject.transform.eulerAngles;
        targetRotationX = boxRotation.x + rotationBeforeFlip;
        targetRotationZ = boxRotation.z + rotationBeforeFlip;
    }
    private void Update()
    {
        var boxRotation_ = this.gameObject.transform.eulerAngles;


        //changes negative rotation values to positives
        if (boxRotation_.x >= -0.1f || boxRotation_.z >= -0.1f)
        {
            Mathf.Abs(boxRotation_.x);
            Mathf.Abs(boxRotation_.z);
        }

        //rotates above certain thresholds
        if (boxRotation_.x < 180  && boxRotation_.x > targetRotationX ||     /*if between (30) and 179,9*/
            boxRotation_.x >= 180 && boxRotation_.x < 360 - targetRotationX  /*if between (330) and 180*/
            ||
            boxRotation_.x > -180 && boxRotation_.x < -targetRotationX       /*if between (-30) and -179,9*/
            ||
            boxRotation_.x <= -180 && boxRotation_.x > -360 + targetRotationX    /*if between (-330) and -180*/
            )
        {
            Debug.Log("Rotating X to default");
            RotateX();
        }

        if (boxRotation_.z < 180 && boxRotation_.z > targetRotationZ ||
            boxRotation_.z >= 180 && boxRotation_.z < 360 - targetRotationZ
            ||
            boxRotation_.z > -180 && boxRotation_.z < -targetRotationZ
            ||
            boxRotation_.z <= -180 && boxRotation_.z > -360 + targetRotationZ
            )
        {
            Debug.Log("Rotating Z to default");
            RotateZ();
        }
        
    }

    #endregion initialization and shutdown

    #region handling


    private void RotateX()
    {
        Quaternion defaultRotation = Quaternion.Euler(0,0,0);
        Debug.Log(Quaternion.Lerp(this.transform.rotation, defaultRotation, Time.deltaTime * rotationSpeed));
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, defaultRotation, Time.deltaTime * rotationSpeed);
    }

    private void RotateZ()
    {
        Quaternion defaultRotation = Quaternion.Euler(0, 0, 0);
        this.transform.rotation = Quaternion.Lerp( this.transform.rotation, defaultRotation, Time.deltaTime * rotationSpeed);
    }

    #endregion handling


}
