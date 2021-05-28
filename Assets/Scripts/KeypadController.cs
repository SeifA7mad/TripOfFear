using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void use(GameObject textPanel) {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        textPanel.SetActive(!textPanel.activeSelf);
    }
}
