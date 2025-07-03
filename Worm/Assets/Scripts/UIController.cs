// using UnityEngine;
// using UnityEngine.UI;

// public class UIController : MonoBehaviour
// {
//     public static UIController Instance;

//     [SerializeField] Image magnetIcon;

//      void Awake()
//     {
//         if (Instance == null)
//             Instance = this;
//         else
//             Destroy(gameObject);

//         HideMagnetIcon(); // Oyun başında gizli başlasın
//     }

//     public void ShowMagnetIcon()
//     {
//         if (magnetIcon != null)
//             magnetIcon.transform.localScale = Vector3.one;
//     }

//     public void HideMagnetIcon()
//     {
//         if (magnetIcon != null)
//             magnetIcon.transform.localScale = Vector3.zero;
//     }
// }

using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] Image magnetIcon;
    [SerializeField] float scaleSpeed = 5f;

    Vector3 targetScale = Vector3.zero;
    bool isScaling = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        magnetIcon.transform.localScale = Vector3.zero;
        magnetIcon.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isScaling)
        {
            magnetIcon.transform.localScale = Vector3.Lerp(magnetIcon.transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

            if (Vector3.Distance(magnetIcon.transform.localScale, targetScale) < 0.01f)
            {
                magnetIcon.transform.localScale = targetScale;
                if (targetScale == Vector3.zero)
                    magnetIcon.gameObject.SetActive(false);

                isScaling = false;
            }
        }
    }

    public void ShowMagnetIcon()
    {
        magnetIcon.gameObject.SetActive(true);
        targetScale = Vector3.one;
        isScaling = true;
    }

    public void HideMagnetIcon()
    {
        targetScale = Vector3.zero;
        isScaling = true;
    }
}
