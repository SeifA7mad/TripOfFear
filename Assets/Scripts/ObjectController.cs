using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public bool isOpen = false;
    private Animator animator;
    private AudioSource soundEffect;
    public AudioClip openingMetalEffect;
    public AudioClip OpenClosedDoorEffect;
    public AudioClip generatorSound;

    private void Start() {
        animator = GetComponent<Animator>();
        soundEffect = transform.GetChild(0).GetComponent<AudioSource>();
    }
    private void FixedUpdate() {
        if (this.gameObject.CompareTag("Locker")) {
            if (isOpen) {
                Open();
            }
        }
    }

    public void Open() {
        isOpen = !isOpen;
        animator.SetBool("IsOpen", isOpen);
        soundEffect.clip = openingMetalEffect;
        soundEffect.Play();
    }

    public void OpenWithKeys() {
        if (FindObjectOfType<PlayerInventory>().keys && this.gameObject.tag == "correctDoor") {
            FindObjectOfType<LevelManager>().loadNext = true;
        } else if (FindObjectOfType<PlayerInventory>().keys) {
            StartCoroutine(FindObjectOfType<DialogSequense>().dialogSequence("Miles: I think the sound doesn't come from this door"));
        } else {
            soundEffect.clip = OpenClosedDoorEffect;
            soundEffect.Play();
            StartCoroutine(FindObjectOfType<DialogSequense>().dialogSequence("Miles: I think I need keys to open this door"));
        }
    }

    public void OpenWithScrewDriver() {
        if (FindObjectOfType<PlayerInventory>().screwDriver) {
            FindObjectOfType<LevelManager>().loadNext = true;
        } else {
            StartCoroutine(FindObjectOfType<DialogSequense>().dialogSequence("Miles: I need to find something to unscrew the hatch"));
        }
    }

    public void turnOnGenerator() {
        soundEffect.clip = generatorSound;
        soundEffect.Play();
    }

}
