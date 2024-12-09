using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPickUpController : MonoBehaviour
{
    int pointValue = 1;
    public GameObject pickUpFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().SetMultiplier(pointValue);
            Instantiate(pickUpFX, transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }

    public void SetPoints(int points) 
    {
        pointValue = points;
    }
}

