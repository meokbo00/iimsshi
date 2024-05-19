// ���� ���� �ִ� ��� ������ �����ϴ� ��ũ��Ʈ

//using System.Collections.Generic;
//using UnityEngine;
//using Newtonsoft.Json;
//class Data
//{
//    public int id;
//    public float x;
//    public float y;
//}
//public class Insenemy : MonoBehaviour
//{
//    public GameObject enemyball;
//    public int stage;

//    void Start()
//    {
//        var asset = Resources.Load<TextAsset>("enemy");
//        var json = asset.text;
//        var datas = JsonConvert.DeserializeObject<List<Data>>(json);
//        foreach (var data in datas)
//        {
//            Debug.LogFormat("{0}, {1}, {2}", data.id, data.x, data.y);
//            Instantiate(enemyball, new Vector3(data.x, data.y, 0), Quaternion.identity);
//        }
//    }
//}

// ���� �ٸ� �ڵ忡�� stage�� ���� ������Ű�� �ʹٸ�

//using UnityEngine;

//public class OtherScript : MonoBehaviour
//{
//    // Insenemy ��ũ��Ʈ ����
//    public Insenemy insenemyScript;

//    void Start()
//    {
//        // Insenemy ��ũ��Ʈ�� stage ���� ����
//        insenemyScript.stage = 2; // ���÷� 2�� �����ϸ� stage�� 2�� ���� �����˴ϴ�.
//    }
//}
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

class Data
{
    public int id;
    public float x;
    public float y;
}

public class Insenemy : MonoBehaviour
{
    public GameObject enemyball;
    public int stage = 1;

    void Start()
    {
        // ���������� �ش��ϴ� ID ���

        var asset = Resources.Load<TextAsset>("enemy");
        var json = asset.text;
        var datas = JsonConvert.DeserializeObject<List<Data>>(json);
        foreach (var data in datas)
        {
            // ���� ���������� �ش��ϴ� ID�� ����
            if (data.id == stage)
            {
                Debug.LogFormat("{0}, {1}, {2}", data.id, data.x, data.y);
                Instantiate(enemyball, new Vector3(data.x, data.y, 0), Quaternion.identity);
            }
        }
    }
}