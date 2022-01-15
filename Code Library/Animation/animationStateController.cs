using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float decceleration = 0.1f;
    int VelocityHash;
    

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // aaplies player walk and run speed
    void Update()
    {
        bool forwarPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if (forwarPressed && velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
        }
       
        if (!forwarPressed && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * decceleration;
        }

        if (!forwarPressed && velocity < 0.0f)
        {
            velocity -= 0.0f;
        }

        animator.SetFloat(VelocityHash, velocity);
    }
}
