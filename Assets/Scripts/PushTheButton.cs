using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PushTheButton : MonoBehaviour {
    public static event Action<string> ButtonPressed;
    private int deviderPosition;
    private string buttonName, buttonValue;
    
    // Start is called before the first frame update
    void Start() {
        this.buttonName = this.gameObject.name;
        this.deviderPosition = buttonName.IndexOf("_");
        this.buttonValue = buttonName.Substring(0, deviderPosition);
        this.gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

   private void ButtonClicked() {
       ButtonPressed(buttonValue);
   }
}
