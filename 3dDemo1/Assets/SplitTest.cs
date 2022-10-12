using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SplitTest : MonoBehaviour
{

    //���ĵ�
    public Transform cenerObj;
    //�����Ӷ���
    public List<GameObject> childs = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in cenerObj)
        {
            //Vector3 vector3 = SplitCalDistance(cenerObj, t);
            //t.position = vector3;
            
            childs.Add(t.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SplitObj();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MergeObj();
        }
    }

    void SplitObj()
    {
        for(int i = 0; i < childs.Count; i++)
        {
            GameObject t = childs[i];
            Vector3 vector3 = SplitCalDistance(cenerObj, t.transform);
            //Dotween�������Ч��
            //childs[i].transform.DOMove(vector3, 1f, false);
            //childs[i].transform.position = vector3;
            childs[i].transform.Translate(vector3);
        }
    }

    void MergeObj()
    {
        for (int i = 0; i < childs.Count; i++)
        {
            GameObject t = childs[i];
            Vector3 vector3 = t.transform.position;
            vector3.x = vector3.x / 2 * Time.deltaTime;
            vector3.y = vector3.y / 2 * Time.deltaTime;
            vector3.z = vector3.z / 2 * Time.deltaTime;
            //Dotween�������Ч��
            //t.transform.DOMove(vector3, 1f, false);
            // t.transform.position = vector3;
            t.transform.Translate(vector3);
        }
    }

    //�����ӽڵ㵽���ĵ�ľ��룬Ȼ�����2
    Vector3 SplitCalDistance(Transform parentObj,Transform targetObj)
    {
        Vector3 temp;

        temp.x = (targetObj.position.x - parentObj.position.x) * 2 * Time.deltaTime;
        temp.y = (targetObj.position.y - parentObj.position.y) * 2 * Time.deltaTime;
        temp.z = (targetObj.position.z - parentObj.position.z) * 2 * Time.deltaTime;

        return temp;
    }
}
