using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static string loadScene;
    public static int loadType;

    private void Start()
    {
        StartCoroutine(SceneLoad());
    }

    public static void LoadSceneHandle(string _name, int _loadType)
    {
        loadScene = _name;
        loadType = _loadType;
        SceneManager.LoadScene("InGame");
    }
    IEnumerator SceneLoad()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            yield return null;

            if(loadType == 0)
            {

            }
            else if (loadType == 1)
            {

            }

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
        
    }
}
