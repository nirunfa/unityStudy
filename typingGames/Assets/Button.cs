using System.Collections;
using UnityEngine;

namespace Assembly_CSharp
{
    public class Button : MonoBehaviour
    {

        public string btnText;
        public bool isClick = false;
        public bool isExitApp = false;
        public Rect rect;

        // Use this for initialization
        void Start()
        {

        }

        private void OnGUI()
        {
            isClick = GUI.Button(rect, btnText);
            if (isClick)
            {
                if (isExitApp)
                {
                    Application.Quit();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}