using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public Text InfoText;
    public Text deathText;
    public Image deathPanel;
    public Button button;

    bool isSwap = false;

    float time = 0f;
    float F_time = 3f;

    [Range(0.001f, 0.2f)]
    public float transitionTime;

    private AudioManager audioManager;
    public string DeathBGM;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        InfoText.gameObject.SetActive(false);
        deathPanel.gameObject.SetActive(false);
        deathText.text = "Game Over";
        button.gameObject.SetActive(false);
        deathText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSwap)
        {
            if (!PlayerStat.instance.isLive)
            {
                Fade();
                isSwap = true;
                ReadText(1);
            }
            else if (DN.instance.dayCount >= 8)
            {
                Fade();
                isSwap = true;
                ReadText(2);
            }
        }
    }

    public void Fade()
    {
        audioManager.Play(DeathBGM);
        audioManager.SetLoop(DeathBGM);
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        deathPanel.gameObject.SetActive(true);
        deathText.gameObject.SetActive(true);
        Color alpha = deathPanel.color;
        Color textAlpha = deathText.color;
        while (alpha.a < 0.7f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 0.7f, time);
            textAlpha.a = Mathf.Lerp(0, 1f, time);

            deathPanel.color = alpha;
            deathText.color = textAlpha;
            yield return null;
        }
        InfoText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        button.gameObject.SetActive(true);
    }
    public void SceneChange()
    {
        audioManager.SetLoopCancel(DeathBGM);
        audioManager.Stop(DeathBGM);
        SceneManager.LoadScene("Main");
    }

    private void ReadText(int num)
    {
        switch (num)
        {
            case 1:
                InfoText.text = "주인공의 눈 앞이 캄캄해지면서 그만 쓰러지고 말았다. 더 이상 이 주인공에게는 희망이 보이지 않는다.";
                break;
            case 2:
                InfoText.text = "섬에는 유독가스가 넘쳐나게 되었고 이에 버티지 못한 주인공은 죽고 말았다.\n"
                                + "몇일 뒤 퍼진 유독가스의 원인지로 이 섬은 발견되었으며 당시 죽었던 주인공의 시체 또한 발견되었다.\n"
                                + "주인공의 죽음으로 가스에 대한 정보는 빨리 알려졌지만\n"
                                + "그의 죽음을 슬퍼하는 이는 아무도 없었다.\n";
                break;
        }
    }
}
