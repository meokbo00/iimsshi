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
        StageGameManager gameManager = FindObjectOfType<StageGameManager>();

        string jsonString = JsonFile.text;
        Information[] stageInfos = JsonConvert.DeserializeObject<Information[]>(jsonString);

        int stageID = StageState.chooseStage;
        int stageClearID = gameManager.StageClearID;

        // stageID에 해당하는 String 가져오기
        string stageString = "";
        foreach (Information info in stageInfos)
        {
            if (info.id == stageID)
            {
                if (stageClearID < stageID)
                {
                    stageString = "<size=20>???</size>";
                }
                else
                {
                    stageString = info.String;
                }
                Debug.Log(info.id + "번 설명입니다");
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