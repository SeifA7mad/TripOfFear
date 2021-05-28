using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    private AudioSource audioEffects;
    public AudioClip footsteps;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        audioEffects = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetFloat("walkSpeed") > 3.9 && !audioEffects.isPlaying) {
            audioEffects.clip = this.footsteps;
            audioEffects.Play();
        } else if (anim.GetFloat("walkSpeed") < 0.1 && audioEffects.isPlaying) {
            audioEffects.Stop();
            audioEffects.clip = null;
        } 
    }

}
