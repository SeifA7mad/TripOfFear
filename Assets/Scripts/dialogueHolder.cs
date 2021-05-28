using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class dialogueHolder : dialogBaseclass
{
    private Image imgHolder;
    public string sceneToLoad;

    private void Awake()
    {
        imgHolder = GetComponent<Image>();
        StartCoroutine(dialogueSequence());
    }

    private IEnumerator dialogueSequence()
    {
        int i;
        for(i = 0; i< transform.childCount; i++)
        {
            Deactivate();
            transform.GetChild(i).gameObject.SetActive(true);
            if (imgHolder.sprite != transform.GetChild(i).gameObject.GetComponent<dialogueLine>().imagesprite)
                imgHolder.sprite = transform.GetChild(i).gameObject.GetComponent<dialogueLine>().imagesprite;
            yield return new WaitUntil(() => transform.GetChild(i).GetComponent<dialogueLine>().finished);
        }
        if (i == transform.childCount)
            SceneManager.LoadScene(sceneToLoad);
        gameObject.SetActive(false);
    }

    private void Deactivate()
    {
        for ( int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
