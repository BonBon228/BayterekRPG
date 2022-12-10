using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AulSceneTrigger : MonoBehaviour
{
    public GameObject Bijai;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && Bijai == null)
        {
            SceneManager.LoadScene(3);
        }
    }
}
