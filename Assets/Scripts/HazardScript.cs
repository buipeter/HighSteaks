using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    // on trigger, if "Player" (tagged player in hierarchy), touches hazard, then triggers this method
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("colliding " + other);
        // checks for the Tag of "Player" of which we set in our hierarchy
        if (other.tag == "Player")
        {
            FindObjectOfType<PlayerController>().HandleDamage(damage);
            // hazard VFX
            //Instantiate(collectibleEffect, transform.position, transform.rotation);

            // hazard SFX
            //if (collectSound != null)
            //{
            //    AudioSource.PlayClipAtPoint(collectSound, transform.position);
            //}

            
        }
    }

}
