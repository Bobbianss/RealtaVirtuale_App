using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{   //import fileTXT -----------//string path = "Assets/Resources/"name".txt"; in caso volessimo imporate i testi a mano [it doesn't sense]
    public TextAsset dialogueFile;

    //Struct Dialogue 
    List<string> dialogLines;
    List<string> names;
    List<string> sentences;

    // interface
    DialogueSystem dialogueSystem;
    DialogueAudio dialogueAudio;

    // Start is called before the first frame update
    void Start()
    {
        dialogueAudio = GetComponent<DialogueAudio>();

        names = new List<string>();
        sentences = new List<string>();
        if (dialogueFile)
        {
            dialogLines = new List<string>(dialogueFile.text.Split("\n"[0]));
        }

        foreach(string str in dialogLines)
        {
            string[] vectorTemp = str.Split('#');
            Debug.Log("nome" + vectorTemp[0]);
            Debug.Log("frase" + vectorTemp[1]);
            names.Add(vectorTemp[0]);
            sentences.Add(vectorTemp[1]);
        }

        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }//[m] end Start()

    void setUpDialogueText()
    {
        dialogueSystem.dialogueAudio = dialogueAudio;
        dialogueSystem.names = names;
        dialogueSystem.senteces = sentences;
    }//[m] end setUpDialogueText()


    public void OnTriggerStay(Collider other)
    {
        //active script 
        this.gameObject.GetComponent<NPCTalk>().enabled = true;
        //enable All dialogue and other GUIs
        FindObjectOfType<DialogueSystem>().enterOfRangeOfNPC();
        if((other.gameObject.tag == "Player") && Input.GetKeyDown(dialogueSystem.dialogueInput))
        {
            this.gameObject.GetComponent<NPCTalk>().enabled = true;
            setUpDialogueText();
            FindObjectOfType<DialogueSystem>().startTexting();
        }

    }//[m] end OnTriggerStay (Collider other)
    public void OnTriggerExit()
    {
        //Disable script
        this.gameObject.GetComponent<NPCTalk>().enabled = false;
        //disable All dialogue and other GUIs
        FindObjectOfType<DialogueSystem>().outOfRangeOfNPC();
    }//[m] end OnTriggerExit();



}
