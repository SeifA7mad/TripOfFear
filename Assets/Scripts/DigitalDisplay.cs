using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitalDisplay : MonoBehaviour
{
    [SerializeField]
    private Sprite[] digits;
    [SerializeField]
    private Image[] characters;
    private string codeSequence;

    // Start is called before the first frame update
    void Start()
    {
        this.codeSequence = "";

        for (int i = 0; i < characters.Length; i++) {
            characters[i].sprite = digits[10];
        }

        PushTheButton.ButtonPressed += AddDigitToCodeSequence;
    }

    private void AddDigitToCodeSequence(string digitEntered) {
        switch (digitEntered) {
            case "Exit":
                    Exit();
                    break;
            case "Enter":
                if (codeSequence.Length > 0) {
                    checkResults();
                }
                break;
        }
        if (this.codeSequence.Length < 4) {
            switch (digitEntered) {
                case "Zero":
                    this.codeSequence += "0";
                    DisplayCodeSequence(0);
                    break;
                case "One":
                    this.codeSequence += "1";
                    DisplayCodeSequence(1);
                    break;
                case "Two":
                    this.codeSequence += "2";
                    DisplayCodeSequence(2);
                    break;
                case "Three":
                    this.codeSequence += "3";
                    DisplayCodeSequence(3);
                    break;
                case "Four":
                    this.codeSequence += "4";
                    DisplayCodeSequence(4);
                    break;
                case "Five":
                    this.codeSequence += "5";
                    DisplayCodeSequence(5);
                    break;
                case "Six":
                    this.codeSequence += "6";
                    DisplayCodeSequence(6);
                    break;
                case "Seven":
                    this.codeSequence += "7";
                    DisplayCodeSequence(7);
                    break;
                case "Eight":
                    this.codeSequence += "8";
                    DisplayCodeSequence(8);
                    break;
                case "Nine":
                    this.codeSequence += "9";
                    DisplayCodeSequence(9);
                    break;
            }
        }
    }

    private void DisplayCodeSequence(int digitjustEntered) {
        switch (codeSequence.Length) {
            case 1:
                characters[3].sprite = digits[digitjustEntered];
                break;
            case 2:
                characters[2].sprite =  characters[3].sprite;
                characters[3].sprite = digits[digitjustEntered];
                break;
            case 3:
                characters[1].sprite =  characters[2].sprite;
                characters[2].sprite =  characters[3].sprite;
                characters[3].sprite = digits[digitjustEntered];
                break;
            case 4:
                characters[0].sprite = characters[1].sprite;
                characters[1].sprite =  characters[2].sprite;
                characters[2].sprite =  characters[3].sprite;
                characters[3].sprite = digits[digitjustEntered];
                break;
        }
    }

    private void checkResults() {
        if (codeSequence == "7195") {
            GameObject.Find("Generator").GetComponent<ObjectController>().turnOnGenerator();
            StartCoroutine(FindObjectOfType<DialogSequense>().dialogSequence("Miles: Yessssss, it works"));
            FindObjectOfType<LevelManager>().loadNext = true;
        }
        else {
            StartCoroutine(FindObjectOfType<DialogSequense>().dialogSequence("Miles: mmmmm, the pass is wrong I need to try again"));
            ResetDisplay();
        }
    }

    private void ResetDisplay() {
        this.codeSequence = "";

        for (int i = 0; i < characters.Length; i++) {
            characters[i].sprite = digits[10];
        }
    }

    private void Exit() {
        transform.parent.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        PushTheButton.ButtonPressed -= AddDigitToCodeSequence;
    }

}
