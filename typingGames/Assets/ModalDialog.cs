using System.Collections;
using UnityEngine;

namespace Assets
{
    public class ModalDialog : MonoBehaviour
    {

        public string content;
        public int windowId;
        public bool isRestart = false;
        public bool isExit = false;
        public Rect rect;

        // Use this for initialization
        void Start()
        {

        }

        private void OnGUI()
        {
            GUI.ModalWindow(windowId, rect, WindowDoFunc, content);
        }

        void WindowDoFunc(int winId)
        {
            isRestart = GUI.Button(new Rect(0, Screen.height/5f, 100, 20), "重新开始");
            isExit = GUI.Button(new Rect(100, Screen.height / 5f, 80, 20), "退出");
            if (isExit)
            {
                Application.Quit();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}