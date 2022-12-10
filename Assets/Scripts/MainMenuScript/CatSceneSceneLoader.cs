using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatSceneSceneLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CutsceneEnd());
    }

    private IEnumerator CutsceneEnd()
    {
        yield return new WaitForSecondsRealtime(17f);
        SceneManager.LoadScene(2);
    }
}
