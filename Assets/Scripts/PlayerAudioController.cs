using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private AudioSource audioEffects;
    public AudioClip footsteps;
    public AudioClip sneaksteps;
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
        if (anim.GetFloat("walkSpeed") == 4 && audioEffects.clip != footsteps) {
            audioEffects.clip = this.footsteps;
            audioEffects.Play();
        } else if (anim.GetFloat("sneakSpeed") == 2 && audioEffects.clip != sneaksteps) {
            audioEffects.clip = this.sneaksteps;
            audioEffects.Play();
        } else if (anim.GetFloat("walkSpeed") < 0.1 && anim.GetFloat("sneakSpeed") < 0.1 && audioEffects.isPlaying) {
            audioEffects.Stop();
            audioEffects.clip = null;
        }
    }
}
