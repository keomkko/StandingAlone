using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTextManager : MonoBehaviour
{
    public static DayTextManager instance;
    public Text DayEndtext;
    public GameObject talkPanel;
    public bool isPrint;

    private void Start()
    {
        instance = this;
    }

    public void TextPrint(int day)
    {
        if (isPrint)
        {
            isPrint = false;
            PlayerAction.instance.speed = 125f;
        }
        else
        {
            isPrint = true;
            talkPanel.SetActive(true);
            switch (day)
            {
                case 1:
                    DayEndtext.text = "해변가를 걷던 도중 큰 파도를 맞게 되었고 눈을 떠보니 이름 모르는 섬에 와 있었다.\n"
                                        + "들고 있는 것도 없고 외부와의 연락도 되지 않는다.\n"
                                        + "보이는 것은 눈 앞의 돌 하나와 야자수 한 그루이다.\n"
                                        + "아무래도 이 섬에서 오래 버티진 못할 것 같다.\n"
                                        + "한시라도 빨리 탈출하자.";
                    break;
                case 2:
                    DayEndtext.text = "이 섬에 온지 이틀 째가 되었다.\n"
                                        + "아직 모르는 것 투성이다.\n" + "이제는 전과 같지 않다.\n" + "혼자서 생존해야 한다";
                    break;
                case 3:
                    DayEndtext.text = "이 섬에 온지 벌써 3일이 되었다.\n"
                                        + "동물들이 존재하는 것으로 보아 나름 생명이 살만한 곳이라는 것을 느꼈다.\n"
                                        + "하지만 지내다 보면서 점점 숨을 쉬기가 힘들어 지는 것 같다.\n" + "기분 탓인가..";
                    break;
                case 4:
                    DayEndtext.text = "여기서 모은 재료들을 통해 여러가지 만들 수 있게 된 것 같다.\n"
                                        + "처음에 생각 했던 것 보다는 오래 생존할 수 있을 것만 같은 느낌을 받았다.\n"
                                        + "조금만 있으면 탈출할 수 있을 것 같다.";
                    break;
                case 5:
                    DayEndtext.text = "평상시였다면 지금쯤 집에서 일반 가정식을 먹어야 한다는 생각이 들며 집이 그리워진다.\n"
                                        + "생명체는 매우 일부만 존재하는 것 같고 수도 많지 않은 듯하다.\n"
                                        + "내가 얼마나 더 버틸 수 있을까 생각된다.";
                    break;
                case 6:
                    DayEndtext.text = "이상하게 날이 갈수록 숨 쉬는 게 힘들어지는 것 같다.\n"
                                        + "이제 정말 얼마 버티지 못할 것이라는 생각이 든다.\n"
                                        + "하루빨리 이 곳을 탈출해야 한다.";
                    break;
                case 7:
                    DayEndtext.text = "아마 오늘 안에 이 곳을 탈출하지 못하면 죽을 것이라는 생각이 든다.\n"
                                        + "숨은 더욱 쉬기 힘들며 섬 내의 동물 등 자원들도 얼마 남지 않았다.\n"
                                        + "생존하기 위해선 어떻게든 탈출해야 한다.";
                    break;
            }
            PlayerAction.instance.speed = 0;
        }
        talkPanel.SetActive(isPrint);
    }
}
