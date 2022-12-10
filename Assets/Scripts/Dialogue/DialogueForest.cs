using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueForest : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    private GameObject player;
    public GameObject dialogueTrigger;
    public GameObject BossScriptManager;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            player.GetComponent<MainCharacter>().enabled = true;
            dialogueTrigger.SetActive(false);
            BossScriptManager.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
