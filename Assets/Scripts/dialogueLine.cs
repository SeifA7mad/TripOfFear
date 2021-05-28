using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dialogueLine : dialogBaseclass
{
    [SerializeField] private string input;
    [SerializeField] private float delay;
    [SerializeField] private float delaybetweenlines;
    private Text textHolder;
    [SerializeField] public Sprite imagesprite;


   private void Awake()
    {
        textHolder = GetComponent<Text>();
        textHolder.text = "";
    }

    private void Start()
    {
        StartCoroutine(WriteText(input, textHolder, delay, delaybetweenlines));
    }
}

