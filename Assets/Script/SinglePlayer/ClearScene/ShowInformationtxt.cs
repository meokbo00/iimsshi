<<<<<<< HEAD
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public class ShowInformationtxt : MonoBehaviour
{
    public TextAsset JsonFile; // JSON 파일을 저장할 변수
    public TMP_Text informationText; // 정보를 출력할 TextMesh

    private void Start()
    {
        // 초기화할 때는 아무 정보도 출력하지 않도록 합니다.
        informationText.text = "";
    }

    private void Update()
    {
        UpdateStageInfo();
    }

    public void UpdateStageInfo()
    {
        string jsonString = JsonFile.text;
        Information[] stageInfos = JsonConvert.DeserializeObject<Information[]>(jsonString);

        int stageID = StageState.chooseStage;

        // stageID에 해당하는 String 가져오기
        string stageString = "";
        foreach (Information info in stageInfos)
        {
            if (info.id == stageID)
            {
                stageString = info.String;
                break;
            }
        }

        informationText.text = stageString;
    }
}

[System.Serializable]
public class Information
{
    public int id;
    public string String;
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInformationtxt : MonoBehaviour
{
    class Chat
    {
        public int id;
        public string[] texts;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
>>>>>>> ac305b52a0663efb6ca43cf435bb2d564e294b77
