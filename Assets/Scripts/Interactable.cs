using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    private float interactionTime = 0f;
    private float interactionDuration = 0.5f;
    private bool interacting = false;
    public GameObject textPanel;
    // Start is called before the first frame update
    void Start()
    {
        textPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange) {
            if (Input.GetKeyDown(this.interactKey)) {
                this.interactAction.Invoke();
                if (!this.gameObject.CompareTag("Locker") && !this.gameObject.CompareTag("Note")) {
                    this.interacting = true;
                    FindObjectOfType<PlayerController>().anim.SetBool("Interaction", true);
                }
            }
        }

        if (this.interacting) {
            if (interactionTime < interactionDuration) {
                interactionTime += Time.deltaTime;
            } else {
                this.interacting = false;
                this.interactionTime = 0f;
                FindObjectOfType<PlayerController>().anim.SetBool("Interaction", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isInRange = true;
            textPanel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Press " + interactKey.ToString();
            if (this.gameObject.CompareTag("Elevator")) {
                if (FindObjectOfType<LevelManager>().loadNext == false) {
                    textPanel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "There is no Electricity";
                } else {
                    textPanel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Enter the Elevator";
                }
            } else if (this.gameObject.CompareTag("Keys")) {
                textPanel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text += " to steal keys";
            } else if (this.gameObject.CompareTag("Rope")) {
                 textPanel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text += " to untie her";
            }
            textPanel.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
         if (other.gameObject.CompareTag("Player")) {
            isInRange = false;
            textPanel.SetActive(false);
        }
    }
}
