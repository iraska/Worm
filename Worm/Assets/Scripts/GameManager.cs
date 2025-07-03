using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int totalFoodCount = 10;
    int currentFoodCollected = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UIController.Instance.SetFoodGoal(totalFoodCount);
    }

    public void CollectFood()
    {
        currentFoodCollected++;
        UIController.Instance.UpdateProgress(currentFoodCollected, totalFoodCount);
        EffectsController.Instance.PlayCollectSound();

        if (currentFoodCollected >= totalFoodCount)
        {
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        Debug.Log("Level Complete!");
        UIController.Instance.ShowCompletionUI();
    }
}
