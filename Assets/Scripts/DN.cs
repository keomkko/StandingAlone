using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DN : MonoBehaviour
{
    public static DN instance;
    public SpriteRenderer sr;

    public Color day;
    public Color night;

    public Image timeSlider;

    public Text dayText;
    public Text TimeText;
    public int dayCount = 1;
    public float oneDay = 480f;
    public float currentTime;

    [Range(0.001f, 0.2f)]
    public float transitionTime;

    public bool isText = false;
    bool isSwap = false;

    AudioManager audioManager;
    public string DayStartBGM;
    private void Awake()
    {
        float spriteX = sr.sprite.bounds.size.x;
        float spriteY = sr.sprite.bounds.size.y;

        float screenY = Camera.main.orthographicSize * 2;
        float screenX = screenY / Screen.height * Screen.width;

        transform.localScale = new Vector2(Mathf.Ceil(screenX / spriteX), Mathf.Ceil(screenY / spriteY));

        sr.color = day;

        instance = this;
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DayTextManager.instance.isPrint)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DayTextManager.instance.TextPrint(dayCount);
                audioManager.Stop(DayStartBGM);
            }
        }

        if (dayCount == 1)
        {
            if (currentTime == 0)
            {
                FadeScript.instance.StartFadeOut();
                audioManager.Play(DayStartBGM);
            }
            
            if (FadeScript.instance.canText && !isText)
            {
                isText = true;
                DayTextManager.instance.TextPrint(dayCount);
            }
        }
        else if(dayCount != 8)
        {
            if(FadeScript.instance.canText && isText)
            {
                isText = false;
                DayTextManager.instance.TextPrint(dayCount);
            }
        }

        currentTime += Time.deltaTime;
        if (currentTime >= oneDay)
        {
            currentTime = 0;
            dayCount++;
        }
        if (!isSwap)
        {
            if (Mathf.FloorToInt(oneDay * 0.625f) == Mathf.FloorToInt(currentTime))
            {
                //day -> night
                isSwap = true;
                StartCoroutine(SwapColor(sr.color, night));
            }
            else if (Mathf.FloorToInt(oneDay * 0.99f) == Mathf.FloorToInt(currentTime))
            {
                //night -> day
                if (dayCount != 7)
                {
                    audioManager.Play(DayStartBGM);
                    isSwap = true;
                    StartCoroutine(SwapColor(sr.color, day));
                    FadeScript.instance.Fade();
                }
                else
                {
                    isSwap = false;
                    StartCoroutine(SwapColor(sr.color, day));
                }
                
            }
        }

        dayText.text = "DAY " + dayCount.ToString();

        if (currentTime < 120f)
        {
            TimeText.text = (Mathf.FloorToInt(currentTime / 30) + 8).ToString() + " A.M";
        }
        else if (currentTime < 150f)
        {
            TimeText.text = (Mathf.FloorToInt(currentTime / 30) + 8).ToString() + " P.M";
        }
        else
        {
            TimeText.text = (Mathf.FloorToInt(currentTime / 30) - 4).ToString() + " P.M";
        }

        timeSlider.fillAmount = 1.0f - (Mathf.SmoothStep(0, 100, currentTime / oneDay) / 100);
    }

    IEnumerator SwapColor(Color start, Color end)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * (1 / (transitionTime * oneDay));
            sr.color = Color.Lerp(start, end, t);
            yield return null;
        }
        isSwap = false;
    }
}
