using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GUI;
using Random = UnityEngine.Random;
using GameButton = Assembly_CSharp.Button;

public class Game : MonoBehaviour
{

    public List<Enemy> enemies = new List<Enemy>();

    public int HP = 100;

    public int success = 0;
    public int error = 0;
    public int total = 0;

    public bool ac = false;
   
    public Weapon weapon;
    public GameButton startBtn;
    public GameButton exitBtn;
    public ModalDialog resultDialog;

    public CanvasGroup canvasGroup;

    public string inputStr = "";

    // Start is called before the first frame update
    void Start()
    {
        weapon = this.AddComponent<Weapon>();
        weapon.enabled = false;

        startBtn = this.AddComponent<GameButton>();
        startBtn.btnText = "��ʼ��Ϸ";
        startBtn.rect = new Rect(Screen.width / 2f, Screen.height / 2f, 100, 30);
        startBtn.enabled = true;

        exitBtn = this.AddComponent<GameButton>();
        exitBtn.btnText = "�˳�";
        exitBtn.isExitApp = true;
        exitBtn.rect = new Rect(Screen.width-150, 20, 100, 20);
        exitBtn.enabled = true;

        resultDialog = this.AddComponent<ModalDialog>();
        resultDialog.windowId = 1;
        resultDialog.rect = new Rect(Screen.width / 2f, Screen.height / 2f, Screen.width/4f, Screen.height/4f);
        resultDialog.enabled = false;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 20), "��ǰHP:" + HP);

        if (HP <= 0)
        {
            //�ر����ɵ���
            StopCoroutine("GenerateEnemies");
            //ֹͣ�����ƶ�
            foreach(Enemy enemy in enemies)
            {
                enemy.speedAutoMove = 0;
            }
            //�ر�weapon
            weapon.enabled = false;
            
            resultDialog.content = "Game Over";
            resultDialog.isExit = false;
            resultDialog.isRestart = false;
            resultDialog.enabled = true;
        }
        else if(success >= 500)
        {
            //�ɹ�
            //�ر����ɵ���
            StopCoroutine("GenerateEnemies");

            weapon.enabled = false;

            resultDialog.content = "You Are Win!!";
            resultDialog.isExit = false;
            resultDialog.isRestart = false;
            resultDialog.enabled = true;
        }
        else
        {
            if (startBtn.isClick)
            {
                StartCoroutine("GenerateEnemies");

                ac = true;
                weapon.enabled = true;

                //Ŀǰ�������ٵķ�������ʼ��ť
                Destroy(startBtn);
            }
            if (exitBtn.isClick)
            {
                Application.Quit();
            }
        }
    }


    IEnumerator GenerateEnemies()
    {

        while (true)
        {
            //��Ҫ�ظ�ִ�еĴ���ͷ����ڴ˴�
            //�������һ������
            float randomNum = Random.Range(5, 12);
            for (int i = 0; i < randomNum; i++)
            {
                //�������ˣ���׷�ӵ�������
                var enemy = this.AddComponent<Enemy>();
                enemy.setWord(enemyWord());
                enemies.Add(enemy);
            }

            //���ü��ʱ��Ϊ3��
            yield return new WaitForSeconds(2);
        }

    }

    // Update is called once per frame
    void Update()
    {
        HP = HP - enemies.Where(e => e.isWin == true).Count();
        enemies.RemoveAll(e => e.isWin == true);

        if (resultDialog.isRestart)
        {
            //����״̬
            HP = 100;
            //�������
            foreach(Enemy enemy in enemies)
            {
                enemy.enabled = false;
                Destroy(enemy);
            }
            enemies.Clear();
            //���weapon����ֵ
            weapon.enabled = true;
            weapon.inputStr = "";
            //��ʼ���ɵ���
            StartCoroutine("GenerateEnemies");

            //���ٴ���
            Destroy(resultDialog);
        }

        if (ac)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        int asciiCode = (int)keyCode;
                        //ֻ����ĸ������
                        if ((asciiCode >= 65 && asciiCode <= 90) || (asciiCode >= 97 && asciiCode <= 122))
                        {
                            weapon.inputStr = inputStr = keyCode.ToString();
                            total++;

                            Enemy enemy1 = enemies.Where(e => e.word.Equals(inputStr)).FirstOrDefault();
                            if (enemy1 != null)
                            {
                                success++;

                                enemy1.enabled = false;
                                Destroy(enemy1);
                                enemies.Remove(enemy1);

                            }
                            else
                            {
                                error++;
                            }
                        }
                        Debug.LogError("Current Key is : " + keyCode.ToString());
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    /**
     * 
     * �����ȡ���˵���
     */
    public string enemyWord()
    {
        List<char> wordChars = new List<char>();

        char z = 'Z';
        for (int j = 65; ; j++)
        {
            wordChars.Add((char)j);
            //System.out.print((char)j);  

            if (z == (char)j)
            {
                break;
            }
        }

        int randomNum = (int)Random.Range(0f, 26);

        return wordChars[randomNum].ToString();
    }
}
