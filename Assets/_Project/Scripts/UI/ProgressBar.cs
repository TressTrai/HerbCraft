using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private int duration = 1;

    private SpriteRenderer bar;
    private SpriteRenderer overflowingBar;

    // progress can be [0, 1]
    private float targetProgress = 0f;
    private float initialProgress = 1f;

    private float fillSpeed = 0.5f;
    private bool isAnimationContinues = false;
    private float value;
    private bool progressBarShowing = false;

    private OnCraftEnd onCraftEnd;


    private void SetValue(float value)
    {
        this.value = value;

        if (!(value <= 1f && value >= 0f)) return;
        overflowingBar.size = new Vector2(value, 1);
    }


    public int Duration
    {
        get { return duration; }
        set
        {
            duration = value;
            fillSpeed = 1f / value;
        }
    }

    /*public ProgressBar()
    {
        targetProgress = 0f;
        initialProgress = 1f;
    }
    public ProgressBar(float newTargetProgress, float newInitialProgress)
    {
        targetProgress = newTargetProgress;
        initialProgress = newInitialProgress;
    }
    public ProgressBar(float newTargetProgress, float newInitialProgress, float newFillSpeed)
    {
        targetProgress = newTargetProgress;
        initialProgress = newInitialProgress;
        fillSpeed = newFillSpeed;
    }
    public ProgressBar(int duration)
    {
        targetProgress = 0f;
        initialProgress = 1f;
        fillSpeed = 1 / duration;
    }*/


    private void Awake()
    {
        gameObject.SetActive(progressBarShowing);
        fillSpeed = 1f / duration;
        bar = gameObject.GetComponent<SpriteRenderer>();
        overflowingBar = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        overflowingBar.drawMode = SpriteDrawMode.Sliced;

        SetValue(initialProgress);
    }

    private void Update()
    {
        if (isAnimationContinues) UpdateSliderValue();
    }

    public void Begin(OnCraftEnd onCraftEnd)
    {
        print("beginning");
        if (onCraftEnd == null) return;

        SetValue(initialProgress);
        fillSpeed = 1f / duration;
        isAnimationContinues = true;
        this.onCraftEnd = onCraftEnd;
    }
    private void UpdateSliderValue()
    {
        bool shouldIncreaseProgress = targetProgress > initialProgress ? true : false;

        if (shouldIncreaseProgress)
        {
            if (value < targetProgress)
            {
                SetValue(value + fillSpeed * Time.deltaTime);
            } else
            {
                OnTimerEnds();
            }
        }
        else
        {
            if (value > targetProgress)
            {
                SetValue(value - fillSpeed * Time.deltaTime);
            }
            else
            {
                OnTimerEnds();
            }
        }
    }

    private void OnTimerEnds()
    {
        isAnimationContinues = false;
        onCraftEnd();
    }

    public void ShowProgressBar()
    {
        progressBarShowing = true;
        gameObject.SetActive(progressBarShowing);
    }
    public void HideProgressBar()
    {
        progressBarShowing = false;
        gameObject.SetActive(progressBarShowing);
    }
}
