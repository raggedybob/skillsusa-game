using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDialogueScript : MonoBehaviour, IClickable
{
    [TextArea(2, 6)]
    [SerializeField] private List<string> dialogueList = new List<string>();
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;

    public bool isDialogueActive = true;
    private int dialogueIndex = 0;

    private void Awake()
    {
        isDialogueActive = true;
        dialogueIndex = -1;
    }

    public void OnClicked()
    {
        if (!isDialogueActive) return;

        dialogueIndex++;
        
        if (dialogueIndex > dialogueList.Count)
        {
            isDialogueActive = false;
            gameObject.SetActive(false);
            dialogueBox.SetActive(false);
            dialogueText.text = "";
        }
        dialogueText.text = dialogueList[dialogueIndex];
    }

}
