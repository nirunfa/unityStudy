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
        startBtn.btnText = "开始游戏";
        startBtn.rect = new Rect(Screen.width / 2f, Screen.height / 2f, 100, 30);
        startBtn.enabled = true;

        exitBtn = this.AddComponent<GameButton>();
        exitBtn.btnText = "退出";
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
        GUI.Label(new Rect(0, 0, 100, 20), "当前HP:" + HP);

        if (HP <= 0)
        {
            //关闭生成敌人
            StopCoroutine("GenerateEnemies");
            //停止敌人移动
            foreach(Enemy enemy in enemies)
            {
                enemy.speedAutoMove = 0;
            }
            //关闭weapon
            weapon.enabled = false;
            
            resultDialog.content = "Game Over";
            resultDialog.isExit = false;
            resultDialog.isRestart = false;
            resultDialog.enabled = true;
        }
        else if(success >= 500)
        {
            //成功
            //关闭生成敌人
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

                //目前先用销毁的方法处理开始按钮
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
            //需要重复执行的代码就放于在此处
            //随机生成一波敌人
            float randomNum = Random.Range(5, 12);
            for (int i = 0; i < randomNum; i++)
            {
                //创建敌人，并追加到集合里
                var enemy = this.AddComponent<Enemy>();
                enemy.setWord(enemyWord());
                enemies.Add(enemy);
            }

            //设置间隔时间为3秒
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
            //重置状态
            HP = 100;
            //清除敌人
            foreach(Enemy enemy in enemies)
            {
                enemy.enabled = false;
                Destroy(enemy);
            }
            enemies.Clear();
            //清空weapon输入值
            weapon.enabled = true;
            weapon.inputStr = "";
            //开始生成敌人
            StartCoroutine("GenerateEnemies");

            //销毁窗体
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
                        //只有字母才允许
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
     * 随机获取敌人单词
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
