using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogue : MonoBehaviour
{
    public List<string> dialogue = new List<string>();
    [SerializeField]
    CanvasGroup dialoguebox;
    Text displayed;
    int currentDialogue;

    // Start is called before the first frame update
    void Start()
    {
        currentDialogue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        displayed.text = dialogue[currentDialogue];
    }
    public void NextDialogue()
    {
        if(currentDialogue < dialogue.Count)
        {
            currentDialogue++;
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialoguebox.enabled = false;
    }
}
