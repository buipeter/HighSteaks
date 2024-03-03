using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    public int currentValue;
    public AudioClip collectSound;
    public GameObject collectibleEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddCollectibles(currentValue);

            // Instantiate the collectible effect
            Instantiate(collectibleEffect, transform.position, transform.rotation);

            // Play the collect sound if available
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            Destroy(gameObject);
        }
    }
}
