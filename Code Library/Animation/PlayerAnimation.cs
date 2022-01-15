using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;


    // get the reference for the animator
    //remember to set up animation tree
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //animation forward & backwards
        if (Input.GetKey("w") ^ Input.GetKey("s"))
        {
            animator.SetBool("walk", true);
        }

        if (!Input.GetKey("w") ^ Input.GetKey("s"))
        {
            animator.SetBool("walk", false);
        }

        //animation forward right
        if (Input.GetKey("d"))
        {
            animator.SetBool("walkRight", true);
        }

        if (!Input.GetKey("d"))
        {
            animator.SetBool("walkRight", false);
        }

        //animation forward left
        if (Input.GetKey("a"))
        {
            animator.SetBool("walkLeft", true);
        }

        if (!Input.GetKey("a"))
        {
            animator.SetBool("walkLeft", false);
        }

        //animation jump
        if (Input.GetKey("space"))
        {
            animator.SetBool("jump", true);
        }

        if (!Input.GetKey("space"))
        {
            animator.SetBool("jump", false);
        }
    }
}
