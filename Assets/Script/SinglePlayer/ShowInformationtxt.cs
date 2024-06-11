using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public class ShowInformationtxt : MonoBehaviour
{
    public TextAsset JsonFile; // JSON ������ ������ ����
    public TMP_Text informationText; // ������ ����� TextMesh

    private void Start()
    {
        // �ʱ�ȭ�� ���� �ƹ� ������ ������� �ʵ��� �մϴ�.
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

        // stageID�� �ش��ϴ� String ��������
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
                Debug.Log(info.id + "�� �����Դϴ�");
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