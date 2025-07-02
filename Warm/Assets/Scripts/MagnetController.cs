using UnityEngine;

public class MagnetController : MonoBehaviour
{
    [SerializeField] float magnetDuration = 5f;

    private void Start()
    {
        UIController.Instance.HideMagnetIcon(); // Oyun başında gizli başlasın
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTailController tailController = other.GetComponent<PlayerTailController>();

            if (tailController != null)
            {
                tailController.ActivateMagnet(magnetDuration);
            }

            UIController.Instance.ShowMagnetIcon();

            Destroy(gameObject);
        }
    }
}