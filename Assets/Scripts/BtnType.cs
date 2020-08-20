using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnType : MonoBehaviour
{
    public BTNType currentType;
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.New:
                LoadScene.LoadSceneHandle("Play", 0);
                break;
            case BTNType.Continue:
                LoadScene.LoadSceneHandle("Play", 1);
                break;
            case BTNType.Quit:
                Application.Quit();
                break;
        }
    }
}
