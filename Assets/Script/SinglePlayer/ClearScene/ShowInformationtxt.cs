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
        string jsonString = JsonFile.text;
        Information[] stageInfos = JsonConvert.DeserializeObject<Information[]>(jsonString);

        int stageID = StageState.chooseStage;

        // stageID�� �ش��ϴ� String ��������
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