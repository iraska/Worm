using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTailController : MonoBehaviour
{
    [SerializeField] Transform segmentPrefab;
    [SerializeField] List<Transform> segments = new List<Transform>();
    [SerializeField] float distance = 0.3f;
    [SerializeField] float magnetRadius = 5f;
    [SerializeField] LayerMask foodLayer;

    bool isMagnetActive = false;
    float magnetTimer = 0f;

    void Start()
    {
        segments.Add(this.transform); // head of worm
    }

    void Update()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = Vector3.Lerp(segments[i].position, segments[i - 1].position, Time.deltaTime / distance);
        }

        if (isMagnetActive)
        {
            magnetTimer -= Time.deltaTime;

            if (magnetTimer <= 0f)
            {
                isMagnetActive = false;
                UIController.Instance.HideMagnetIcon(); // UI'ı gizle
            }
            else
            {
                AttractNearbyFood(); // Mıknatıs aktifken yemekleri çek
            }
        }
    }

    public void AddSegment()
    {
        Transform newSegment = Instantiate(segmentPrefab);
        newSegment.position = segments[segments.Count - 1].position;
        segments.Add(newSegment);
    }

    public void ActivateMagnet(float duration)
    {
        isMagnetActive = true;
        magnetTimer = duration;
    }

    private void AttractNearbyFood()
    {
        Collider[] foods = Physics.OverlapSphere(transform.position, magnetRadius, foodLayer);

        foreach (Collider food in foods)
        {
            Transform foodTransform = food.transform;
            foodTransform.position = Vector3.MoveTowards(foodTransform.position, transform.position, 10f * Time.deltaTime);
            foodTransform.localScale = Vector3.Lerp(foodTransform.localScale, Vector3.zero, Time.deltaTime * 1.1f);
        }
    }
}
