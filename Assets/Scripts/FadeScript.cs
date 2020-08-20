using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public static FadeScript instance;
    public Image Panel;
    float time = 0f;
    float F_time = 5f;
    public bool canText = false;

    private void Start()
    {
        instance = this;
    }
    public void Fade()
    {
        canText = false;
        DN.instance.isText = true;
        StartCoroutine(FadeFlow());
        PlayerAction.instance.speed = 0;
    }

    public void StartFadeOut()
    {
        canText = false;
        StartCoroutine(FadeOut());
        PlayerAction.instance.speed = 0;
    }

    public void EndingFade()
    {
        canText = false;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;
        Panel.gameObject.SetActive(false);
        canText = true;
    }

    IEnumerator FadeOut()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        alpha.a = 1;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0;
        Panel.gameObject.SetActive(false);
        canText = true;
    }

    IEnumerator FadeIn()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;
        canText = true;
    }
}
