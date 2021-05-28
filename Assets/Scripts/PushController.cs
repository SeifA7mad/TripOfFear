using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour
{
    public bool isBeingPushed = false;
    private float xPos;
    public GameObject textPanel;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        xPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {  
            if (isBeingPushed == false) {
                transform.position = new Vector3(xPos, transform.position.y);
                audio.Stop();
            }
            else {
                xPos = transform.position.x;
                if (!audio.isPlaying)
                    audio.Play();
            }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Door") {
            FindObjectOfType<LevelManager>().canEnter = false;
            StartCoroutine(FindObjectOfType<DialogSequense>().dialogSequence("Marry: I'm trapped come free me....."));
        }
        if (other.tag == "Player") {
            textPanel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Hold Space to Push";
            textPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        textPanel.SetActive(false);
    }
}
