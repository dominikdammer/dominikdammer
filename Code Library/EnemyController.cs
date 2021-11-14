using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float speed = 1;

    [SerializeField]
    float rotationSpeed = 90f;

    public AudioSource nameClip;

    Animation walkAnimation;
    Rigidbody rigidbody;


    private void Start()
    {
        walkAnimation = GetComponent<Animation>();
        rigidbody = GetComponent<Rigidbody>();
    }


    // activates enemy animation and plays sound, when player collides
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            walkAnimation.Play();
            nameClip.Play();
            Debug.Log("Playing sound and animation.");
        }
    }

    // applies rotation to player position and move to player position for as long as player is colliding
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            walkAnimation.Play();
            var targetRotation = Quaternion.LookRotation(other.transform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
    }


    // stops animation and freeze rotation to stop unwanted rotation when player leaves collider
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            walkAnimation.Stop();
            Debug.Log("Stopping the animation.");
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

}
