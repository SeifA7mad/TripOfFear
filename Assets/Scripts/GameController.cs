using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public int currentScene;
    public bool flashlight = false;
    public bool keys = false;
    public bool screwDriver = false;
    
    public static GameController Instance {
        get;
        set;
    }

    void Awake () {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }

}
