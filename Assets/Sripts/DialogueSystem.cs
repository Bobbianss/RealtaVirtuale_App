using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text uiText;
    public Text uiName;
    public GameObject dialogueGUI;
    public Transform dialogueBoxGUI;
    //Velocità scrittura
    public float letterDelay = 0.1f;  
    public float textAccelerationFactor = 2f;

    public KeyCode dialogueInput = KeyCode.F;

    //Text Lines 
    public List<string> dialogueLines;
    public List<string> sentences;
    public List<string> names;

    public bool endOfSentence = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;

    public AudioClip audioClip;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        uiText.text = "";
    }//[m] Start()

    public void startTexting()
    {
        outOfRange = false;
        dialogueBoxGUI.gameObject.SetActive(true);
        if (Input.GetKeyDown(dialogueInput))
        {
            if (!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(startDialogue());
            }
        }
        startDialogue();
    }//[m] end startTexting()

    private IEnumerator displayToString(string stringToDisplay)
    {
        if (outOfRange == false) //se sei dentro
        {
            int lengthStringLine = stringToDisplay.Length;
            int currentCharacterIndex = 0;
            uiText.text = "";

            while (currentCharacterIndex < lengthStringLine)
            {
             
                uiText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++; // carattere per carattere
                if (currentCharacterIndex < lengthStringLine)
                {
                    if (Input.GetKeyDown(dialogueInput)) //cambiare a pressione costante anziché mashing
                    {
                        yield return new WaitForSeconds(letterDelay / textAccelerationFactor);
                        Debug.Log("Accelleratuuuu");
                        //audio
                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);
                        Debug.Log("NORMAL");
                        //audio
                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(dialogueInput))
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            endOfSentence = false;
            uiText.text = "";
        }

    }// [m] end displayToString(string stringToDisplay)
    private IEnumerator startDialogue()
    {
        if (outOfRange == false)
        {
            int dialogueLenght = sentences.Count;
            Debug.Log("quante frasi" + dialogueLenght);
            int currentDialogueIndex = 0;
            while(currentDialogueIndex< dialogueLenght || !endOfSentence)
            {
                if (!endOfSentence)
                {
                    endOfSentence = true;
                    Debug.Log("Indice" + currentDialogueIndex);
                    uiName.text = names[currentDialogueIndex];
                    StartCoroutine(displayToString(sentences[currentDialogueIndex++]));

                    if(currentDialogueIndex>= dialogueLenght)
                    {
                        dialogueEnded = true;
                    }
                }
                yield return 0;
            }
            while (true)
            {
                if(Input.GetKeyDown(dialogueInput) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
           closeDialogue();
        }
    }// [m] startDialogue()

    public void outOfRangeOfNPC()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            endOfSentence = false;
            dialogueActive = false;
            StopAllCoroutines(); //ma se dovessimo usare altre coroutine questo ci uccide anche quelle?
            dialogueGUI.SetActive(false);
            dialogueBoxGUI.gameObject.SetActive(false);
        }
    }// [m] end outOfRangeOfNPC
    public void enterOfRangeOfNPC()
    {
        outOfRange = false;
        dialogueGUI.SetActive(true);
        if (dialogueActive == true)
        {
            dialogueGUI.SetActive(false);
        }
    }// [m] end enterRangeOfNPC()
    public void closeDialogue()
    {
        dialogueBoxGUI.gameObject.SetActive(false);
        dialogueGUI.SetActive(false);
    }//[m] end closeDialogue()
}
