using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearEnding : MonoBehaviour
{
    public Text GameClear;
    public Text ClearText;

    private string m_text = "배를 타고 가던 중 어선과 만나게 되어 구출되었다.\n"
                            + "현재는 집에 무사히 도착하여 평소의 삶을 만끽하고 있다.\n"
                            + "최근 뉴스에 섬에 매각된 쓰레기들로 인해 유독가스가 나온다는 기사를 보았다.\n"
                            + "아무래도 내가 다녀왔던 섬의 얘기인 것 같다.\n"
                            + "섬에서의 삶은 좋지 않았으며 그 전의 삶도 마땅치 않았다.\n"
                            + "하지만 섬에서의 경험으로 그는 새로운 삶을 살게 된 동기를 얻게 되었다.\n";

    public Image Panel;
    public Button button;
    bool isSwap = false;
    bool isText = false;

    float time = 0f;
    float F_time = 3f;

    AudioManager audioManager;
    public string Typing;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        ClearText.gameObject.SetActive(false);
        Panel.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        GameClear.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSwap)
        {
            if(Inventory.instance.IsClear)
            {
                isSwap = true;
                Fade();
            }
        }
        if(isText)
        {
            isText = false;
            audioManager.Play(Typing);
            StartCoroutine(typing());
        }
    }

    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        ClearText.gameObject.SetActive(true);
        GameClear.gameObject.SetActive(true);
        Color alpha = Panel.color;
        Color TextAlpha = GameClear.color;
        while (alpha.a < 0.9f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 0.9f, time);
            TextAlpha.a = Mathf.Lerp(0, 1f, time);

            Panel.color = alpha;
            GameClear.color = TextAlpha;
            yield return null;
        }
        isText = true;
    }

    IEnumerator typing()
    {
        for(int i = 0; i <= m_text.Length; i++)
        {
            audioManager.SetLoop(Typing);
            ClearText.text = m_text.Substring(0, i);
            yield return new WaitForSeconds(0.08f);
        }
        audioManager.Stop(Typing);
        audioManager.SetLoopCancel(Typing);
        button.gameObject.SetActive(true);
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("Main");
    }
}
