using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAudio : MonoBehaviour
{
  
    public List<AudioClip> clipsPlayer;
    List<int> sequenceClips;
    private AudioSource audioSource;
    public float rythm;
    public bool isRunning=false;
    private IEnumerator courtineVoice;
    public string characterName;


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
      
    }
    private void sequenceFromSentece(string sentence)
    {
        sequenceClips = new List<int>();

        for (int index = 0; index < sentence.Length / 3; index++)
        {
            int x = sentence[index * 3] + (sentence[index * 3 + 1]) * (sentence[index * 3 + 2]);
            int y = sentence[index * 3] * (sentence[index * 3 + 1]) + (sentence[index * 3 + 2]);

            sequenceClips.Add((int)(Mathf.PerlinNoise((x * 13.5674721f), (y * 23.8430874f)) * sentence.Length));
        }
    }
    public void saySentence(string characterName, List<AudioClip> clips, string sentence)
    {
        if (isRunning)
        {
            StopCoroutine("Voice");
            audioSource.Stop();
        }
        sequenceFromSentece(sentence);
        if (this.characterName.Equals(characterName))
        {
            Debug.Log("STA PARLANDO" + this.characterName);
            StartCoroutine(Voice(this.sequenceClips, clips));
        } 
        else
        {
            Debug.Log("STA PARLANDO : IL PLAYER"  );
            StartCoroutine(Voice(this.sequenceClips,this.clipsPlayer));
        }
    }
    private IEnumerator Voice(List<int> sequenceClips, List<AudioClip> clips)
    {
        isRunning= true;
        audioSource.PlayOneShot(clips[sequenceClips[0] % clips.Count]);

        for(int index=1; index<sequenceClips.Count;)
        {
           
            yield return new WaitForSeconds(rythm);
            if (!audioSource.isPlaying)
            {              
                audioSource.PlayOneShot(clips[sequenceClips[index] % clips.Count]);
                index++;
            }

        }

        isRunning = false;
    }

    public void stopExecution()
    {
        StopCoroutine("Voice");
        audioSource.Stop();
    }
  
}
