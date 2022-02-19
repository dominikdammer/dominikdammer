/// <copyright> (c) Out of the box 2021 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using System.Collections;
using UnityEngine;

public class SurveilanceCamController : MonoBehaviour
{
    #region fields
    [Header("Look for")]
    public Transform target;

    [Header("Camera movement")]
    public float yaw;
    public float pitch;
    public float secondsToRot;
    public float rotSwitchTime;
    public bool rotRight;

    Transform camEye;

    bool startNextRotation = true;

    #endregion fields

    #region initialization and shutdown

    private void Start()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0f, yaw / 2, 0f);
        camEye = transform.GetChild(0);
        camEye.localRotation = Quaternion.AngleAxis(pitch, Vector3.right);
    }

    private void Update()
    {

        if (startNextRotation && rotRight)
        {
            StartCoroutine(Rotate(yaw, secondsToRot));
        }
        else if(startNextRotation && !rotRight)
        {
            StartCoroutine(Rotate(-yaw, secondsToRot));          
        }
    }

    #endregion initialization and shutdown

    #region handling

    IEnumerator Rotate (float yaw, float duration)
    {
        startNextRotation = false;
        
        Quaternion initialRotation = transform.rotation;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.rotation = initialRotation * Quaternion.AngleAxis(timer / duration * yaw, Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(rotSwitchTime);

        startNextRotation = true;
        rotRight = !rotRight;

    }

    void SetUpStartRotation()
    {
        if (rotRight)
        {
            transform.localRotation = Quaternion.AngleAxis(-yaw / 2, Vector3.up) ;            
        }
        else
        {
            transform.localRotation = Quaternion.AngleAxis(yaw / 2, Vector3.up);
        }
    }


    #endregion handling


}
