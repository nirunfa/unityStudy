using System.Collections;
using UnityEngine;

namespace Assets
{
    public class Weapon : MonoBehaviour
    {
        public string inputStr = "";

        float centerX = Screen.width / 2f;
        float centerY = Screen.height - 120f;


        // Use this for initialization
        void Start()
        {

        }

        void OnGUI()
        {
            inputStr = GUI.TextField(new Rect(centerX, centerY, 100, 30), inputStr);
            inputStr = inputStr.ToUpper();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}