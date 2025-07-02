using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Food collected!");
            
            FindObjectOfType<PlayerTailController>().AddSegment();
            Destroy(gameObject);
        }
    }
}
