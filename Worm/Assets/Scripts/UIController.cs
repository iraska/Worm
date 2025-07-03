using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] Image magnetIcon;
    [SerializeField] float scaleSpeed = 5f;
    [SerializeField] Image progressBarFill;
    [SerializeField] GameObject completionPanel;
    //[SerializeField] ParticleSystem confettiEffect;

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

    public void UpdateProgress(int current, int total)
    {
        float fillAmount = (float)current / total;
        progressBarFill.fillAmount = fillAmount;
    }

    public void ShowCompletionUI()
    {
        completionPanel.SetActive(true);
        EffectsController.Instance.StopBGM();
        EffectsController.Instance.PlayWinSound();

        // if (confettiEffect != null)
        // {
        //     confettiEffect.gameObject.SetActive(true);
        //     confettiEffect.Play();
        // }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerController controller = player.GetComponent<PlayerController>();

            if (controller != null)
                controller.StopMovement();
        }

        StartCoroutine(SlowDownTimeAndStop());
    }

    public void SetFoodGoal(int total)
    {
        progressBarFill.fillAmount = 0;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenStoreLink()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.wormgame.id");
    }

    IEnumerator SlowDownTimeAndStop()
    {
        float t = 0f;
        float duration = 2f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(1f, 0f, t / duration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            yield return null;
        }

        Time.timeScale = 0f;
    }
} 