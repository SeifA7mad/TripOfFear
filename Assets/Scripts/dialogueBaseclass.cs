using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogBaseclass : MonoBehaviour
{
    public bool finished { get; set; }

    protected IEnumerator WriteText(string input, Text textHolder, float delay, float delaybetweenlines)
    {
        for (int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(delaybetweenlines);
        finished = true;
    }

    protected IEnumerator WriteText(string input, TMPro.TextMeshProUGUI textHolder, float delay, float delaybetweenlines)
    {
        finished = false;
        for (int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(delaybetweenlines);
        textHolder.text = "";
        finished = true;
    }
}

