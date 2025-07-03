using UnityEngine;

public class FoodController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectFood();
            FindObjectOfType<PlayerTailController>().AddSegment();
            Destroy(gameObject);
        }
    }
}
