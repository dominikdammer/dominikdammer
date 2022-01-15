using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float range = 100f;
    public float pitchMin, pitchMax, volumeMin, volumeMax;

    public GameObject destroyedVersion;
    public float breakForce = 1.0f;
    public AudioSource deathSound;


    
        public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            RandomAudioPitch();

            Die();
            
        }


        //destroys inital object and spawns a predestroyed object (Blender), then applies outwards force
        void Die()
        {
                
            GameObject frac = Instantiate(destroyedVersion, transform.position, transform.rotation);

                foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
                {
                    Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
                    rb.AddForce(force);
                }

                Destroy(gameObject);
            }
    }

    //applies a random pitch to the sound of the destroyed object
    private void RandomAudioPitch()
    {

        FindObjectOfType<AudioSource>().pitch = Random.Range(pitchMin, pitchMax);
        deathSound.Play();

    }


}
