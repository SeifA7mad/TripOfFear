using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInGame : dialogBaseclass
{
    private TMPro.TextMeshProUGUI textHolder;

    private void Awake() {
        textHolder = GetComponent<TMPro.TextMeshProUGUI>();
        textHolder.text = "";
        this.finished = true;
    }

    public void write(string input) {
        textHolder.text = "";
        StartCoroutine(WriteText(input, textHolder, 0.05f, 0.35f));
    }
}
