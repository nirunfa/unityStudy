using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class helloWord : MonoBehaviour
{
    public string name = "hello world demo";
    // Start is called before the first frame update
    void Start()
    {
        print(name);

    }

    private void OnGUI()
    {
        GUI.ModalWindow(1, new Rect(20, 20, 120, 50), modalWindowFunc, "modal-window");
    }

    // Update is called once per frame
    void Update()
    {
        //print("hello world update");
    }

    void modalWindowFunc(int windowId)
    {
        if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
        {
            print("Got a click");
        }
    }
}
