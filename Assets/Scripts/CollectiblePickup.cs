using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    public int currentValue;
    public AudioClip collectSound;
    public GameObject collectibleEffect;
    public static int total;

    // counts the total amount of collectibles there are on the map
    void Awake() => total++;


    // rotates the collectibles within the y axis
    private void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
    }


    // on trigger, if "Player" (tagged player in hierarchy), touches collectible, then triggers this method
    private void OnTriggerEnter(Collider other)
    {
        // checks for the Tag of "Player" of which we set in our hierarchy
        if (other.tag == "Player")
        {
            // finds the collectible, and if collided then calls AddCollectibles in GameManager.
            FindObjectOfType<GameManager>().AddCollectibles(currentValue);

            // once pickup on the collectible, does collectible effect
            Instantiate(collectibleEffect, transform.position, transform.rotation);

            // once pickup on the collectible, does collectible sound effect
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // once pick up, destroys the collectible from the player's current session.
            Destroy(gameObject);
        }
    }
}
