using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMeAudio : MonoBehaviour
{
    AudioSource audio;
    private float audioDuration = 5f;
    private float audioTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
           if (audioTime < audioDuration) {
               audioTime += Time.deltaTime;
           } else {
               audioTime = 0f;
               audio.Play();
           }
    }
}
