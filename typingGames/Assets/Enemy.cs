using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    public string word;
    public float speedAutoMove = 0.4f;

    public float positionX;
    public float positionY;
    public bool isWin = false;


    public void setWord(string wordStr)
    {
        word = wordStr;

        positionX = Random.Range(5f, Screen.width - 5f);
        positionY = Random.Range(5f, 20f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnGUI()
    {
        //随机出现位置，然后开始移动
        GUI.Label(new Rect(positionX, positionY, 20, 20), word);
    }

    // Update is called once per frame
    void Update()
    {
        if (positionY >= Screen.height - 3f)
        {
            isWin = true;
        }
        else
        {
            positionY += speedAutoMove;
        }
    }

    void FixedUpdate()
    {
        
    }
}
