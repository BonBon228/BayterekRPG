using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{

    public void GameStarter() {
        SceneManager.LoadScene(1);
    }

    public void Quitting() {
        Application.Quit();
    }
}
