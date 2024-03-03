using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    public int currentValue;

    public GameObject collectibleEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddCollectibles(currentValue);

            Instantiate(collectibleEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
