using UnityEngine;


public class PickUp : MonoBehaviour
{
    public GameObject TextPopUp;


    //destroys object on collision with player. activate UI text for 3 secs
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("chalice");

            Debug.Log("Player entered");
            foreach (GameObject target in gameObjects)
            {
                GameObject.Destroy(target);
            }

            TextPopUp.SetActive(true);
            Destroy(TextPopUp,3f);

        }
    }

}
