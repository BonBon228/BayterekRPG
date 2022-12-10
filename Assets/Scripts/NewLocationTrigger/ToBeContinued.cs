using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBeContinued : MonoBehaviour
{
    private void Start() {
        StartCoroutine(ToMainMenu());
    }

    private IEnumerator ToMainMenu() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
