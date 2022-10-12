using System.Collections;
using UnityEngine;

namespace Assets
{
    public class Dialog : MonoBehaviour
    {

        public string content;

        // Use this for initialization
        void Start()
        {

        }

        private void OnGUI()
        {
            GUI.Box(new Rect(Screen.width / 2f, Screen.height / 2f, Screen.width / 4f, Screen.height /4f), content);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}