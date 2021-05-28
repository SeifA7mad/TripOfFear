using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject Enemy;
    private float spawnDuration = 20f;
    private float spawnTime = 0f;
    private float deathDuration = 4f;
    private float deathTime = 0f;
    public bool loadNext = false;
    public bool gameOver = false;
    public bool canEnter = true;
    public bool gone = false;
    private DialogSequense dialog;

    private void Awake() {
        dialog = FindObjectOfType<DialogSequense>();
    }


    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.currentScene = SceneManager.GetActiveScene().buildIndex;

        if (SceneManager.GetActiveScene().buildIndex == 1) {
            StartCoroutine(dialog.dialogSequence("Miles: Oh shit, I manged to escape from him, what is this place? And where is marry?", "Miles: It's too dark I cannot see clearly", "Miles: I have to find a way out from here"));
        } else if (SceneManager.GetActiveScene().buildIndex == 2) {
            StartCoroutine(dialog.dialogSequence("Miles: What is this rooms? It looks like a hospital...I have to find marry", "Miles: I think I'm hearing marry's voice I must get closer to the voice"));
        } else if (SceneManager.GetActiveScene().buildIndex == 3) {
            StartCoroutine(dialog.dialogSequence("Miles: Ohhhhhh... I think he saw me", "Miles: I need to close the door with anything noooow before he enter"));
        } else if (SceneManager.GetActiveScene().buildIndex == 4) {
            StartCoroutine(dialog.dialogSequence("Chris: Ahhhhh finaly I will kill u now", "Miles: No u won't \" I need to block his bullets with my shield \""));
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if (gone) {
            StartCoroutine(dialog.dialogSequence("Miles: I think he is gone now......"));
        }

        if (canEnter && SceneManager.GetActiveScene().buildIndex != 4) {
            if (!Enemy.activeSelf) {
                gone = false;
                if (spawnTime < spawnDuration) {
                    spawnTime += Time.deltaTime;
                }
                else {
                    spawnTime = 0f;
                    Enemy.SetActive(true);
                    StartCoroutine(dialog.dialogSequence("Miles: \"Hearing footsteps \" I must hide before he find me..."));
                }
            } 
        }

        if (gameOver) {
            if (deathTime < deathDuration) {
                deathTime += Time.deltaTime;
            }
            else {
                deathTime = 0f;
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void nextLevel() {
        if (loadNext) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
