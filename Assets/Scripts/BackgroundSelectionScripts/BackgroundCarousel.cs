using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundCarousel : MonoBehaviour
{
    [SerializeField]
    BackgroundImageDataSO backgroundImageDatabase;
    [SerializeField]
    Image backgroundPreview;
    [SerializeField]
    SwipeDetection carouselSwipeDetection;

    private void OnEnable()
    {
        carouselSwipeDetection.OnSwipe += OnSwipe;
        UpdatePreview(backgroundImageDatabase.GetCurrentBackground());
    }
    private void OnDisable()
    {
        carouselSwipeDetection.OnSwipe -= OnSwipe;
    }

    public void PreviousImage()
    {
        UpdatePreview(backgroundImageDatabase.GetPreviousBackground());
    }

    public void NextImage()
    {
        UpdatePreview(backgroundImageDatabase.GetNextBackground());
    }

    void UpdatePreview(Sprite newPreview)
    {
        backgroundPreview.sprite = newPreview;
    }

    void OnSwipe(Vector2 position, SwipeDetection.DIRECTION direction)
    {
        if (direction == SwipeDetection.DIRECTION.LEFT)
        {
            PreviousImage();
        }
        else if (direction == SwipeDetection.DIRECTION.RIGHT) NextImage();
    }
}
