using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public GameObject flashlightImage;
    public GameObject keysImage;
    public GameObject screwDriverImage;
    public bool Flashlight;
    public bool keys;
    public bool screwDriver;
    public bool weapon;
    private DialogSequense dialog;
    private bool genDialog = true;
    // Start is called before the first frame update

    private void Awake() {
        dialog = FindObjectOfType<DialogSequense>();
    }

    private void Start() {
        if (SceneManager.GetActiveScene().buildIndex == 4) {
            this.weapon = true;
            this.GetComponent<Animator>().SetBool("Weapon", weapon);
        }
        this.Flashlight = GameController.Instance.flashlight;
        this.keys = GameController.Instance.keys;
        this.screwDriver = GameController.Instance.screwDriver;
        if (Flashlight) {
            flashlightImage.GetComponent<Image>().enabled = true;
        }
        if (keys) {
            keysImage.GetComponent<Image>().enabled = true;
        }
        if (screwDriver) {
            screwDriverImage.GetComponent<Image>().enabled = true;
        }
    }

    private void Update() {
       
    }

   public void takeFlashlight() {
       if (!Flashlight) {
            Flashlight = true;
            GameController.Instance.flashlight = true;
            flashlightImage.GetComponent<Image>().enabled = true;
            StartCoroutine(dialog.dialogSequence("Miles: There is a flashlight...I hope it would be helpful"));
       }
   }

   public void readNote(GameObject other) {
       other.SetActive(!other.activeSelf);
   }

   public void takeKeys() {
       if (!keys) {
            keys = true;
            GameController.Instance.keys= true;
            keysImage.GetComponent<Image>().enabled = true;
            StartCoroutine(dialog.dialogSequence("Miles: I think one of this keys can open the door"));
       }
   }

   public void takeScrewDriver() {
       if (!screwDriver) {
            screwDriver = true;
            GameController.Instance.screwDriver = true;
            screwDriverImage.GetComponent<Image>().enabled = true;
            StartCoroutine(dialog.dialogSequence("Miles: Hmmmm a screwdiver I think it will be useful some how"));
       }
   }

   private void OnTriggerEnter2D(Collider2D other) {
       if (other.tag == "Generator" && genDialog) {
           genDialog = false;
           StartCoroutine(dialog.dialogSequence("Miles: Hmm, what is this!.....", "Miles: There is a passcode I should find to it to get electricity for the elevator", "Miles: Where can I find the passcode?"));
       }
   }

}
