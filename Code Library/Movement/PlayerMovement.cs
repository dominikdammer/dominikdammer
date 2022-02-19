/// <copyright> (c) Out Of The Box 2021 </copyright>
/// <author> (c) Marco Eberhardt </author>
/// <url>  </url>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region fields
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody rb;
    bool isJump;
    float jumpForce = 1;
    float movementSpeed = 1;

    #endregion fields

    #region initialization and shutdown

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //normalized or clamped movement values have to be called from fixedUpdate so the diagonally movement is synched
    private void FixedUpdate()
    {
        movement();
    }
    #endregion initialization and shutdown

    #region handling
    void movement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

       ////Movement Variant 1
       ////using rb velocity is not good for walking movement, but might fit other characters
       //rb.velocity += (new Vector3(horizontal, 0, vertical) * movementSpeed / 2).normalized;

       ////Movement Variant 2 (is better but maybe needs slide)
       //using rb moveposition at clamped values is good for walking movement
       Vector3 dirVector = new Vector3(horizontal, 0, vertical).normalized * movementSpeed;

       rb.MovePosition(transform.position + Vector3.ClampMagnitude(dirVector, 100f) * Time.deltaTime);
        
    }


    public void Jump(float jumpForce)
    {

        //formular for velocity from falling height
        var g = Mathf.Abs(Physics.gravity.y);
        var h = (float)jumpForce;
        var v = Mathf.Sqrt(2 * g * h);

        //better jump to accomodate for input time
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else
        {
            // default jump
           rb.AddForce(transform.up * v, ForceMode.VelocityChange);
        }

    }

    //easy groundcheck with raycast
    public void GroundCheck()
    {
        Debug.DrawRay(groundCheck.position, Vector3.down * raylength, Color.red);
        var ray = new Ray(groundCheck.position, Vector3.down);
        RaycastHit hit;

        //select
        if (Physics.Raycast(ray, out hit, raylength, LayerMask.GetMask("Ground", "entangleable", "Object")))
        {
            isJump = false;
        }
        else
        {
            isJump = true;
        }
    }
    #endregion handling

}

