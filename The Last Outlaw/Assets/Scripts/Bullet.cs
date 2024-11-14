using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Target playerHealth = other.GetComponent<Target>();
            playerHealth.health -= 10;

            if (playerHealth.health <= 0)
            {
                //This needs to be updated to take us to the new scene using scenemanager
                SceneManager.LoadScene(2);
            }

            Destroy(gameObject);

        }
    }
}
