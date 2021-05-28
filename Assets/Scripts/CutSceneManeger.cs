using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneManeger : MonoBehaviour
{
    public GameObject imageHolder;
    public GameObject vanCar;
    private AudioSource audio;
    private Image image;
    [SerializeField]
    private Sprite[] imgs;
    [SerializeField]
    private AudioClip[] clips;
    int i;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        image = imageHolder.GetComponent<Image>();
        i = 0;
    }

    void Update()
    {
        if (i < imgs.Length) {
            if (image.sprite == imgs[i] && !audio.isPlaying) {
                audio.clip = clips[i];
                audio.Play();
                if ((SceneManager.GetActiveScene().name == "CutScene1" && image.sprite != imgs[2]) || (SceneManager.GetActiveScene().name == "CutScene2" && image.sprite != imgs[1]))
                    vanCar.SetActive(true);
            } else if (image.sprite != imgs[i] && audio.isPlaying) {
                audio.clip = null;
                audio.Stop();
                i++;
                vanCar.SetActive(false);
            }
        }
    }
}
