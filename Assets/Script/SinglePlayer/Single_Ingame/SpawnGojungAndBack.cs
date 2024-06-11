using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class SpawnGojungAndBack : MonoBehaviour
{
    public TextAsset jsonFile;
    public GameObject[] Gojungtype;

    [System.Serializable]
    public class GojungBackData
    {
        public int id;
        public int Gojungnum;
        public string Color;
    }

    void Start()
    {
        int stage = GlobalData.SelectedStage;

        if (jsonFile != null)
        {
            List<GojungBackData> colorDataList = JsonConvert.DeserializeObject<List<GojungBackData>>(jsonFile.text);

            GojungBackData colorData = colorDataList.Find(data => data.id == stage);

            if (colorData != null)
            {
                if (ColorUtility.TryParseHtmlString("#" + colorData.Color, out Color color))
                {
                    GameObject backgroundObject = GameObject.Find("BackGround");

                    if (backgroundObject != null)
                    {
                        SpriteRenderer spriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();

                        if (spriteRenderer != null)
                        {
                            spriteRenderer.color = color;
                        }
                        else
                        {
                            Debug.LogError("SpriteRenderer 컴포넌트를 찾을 수 없습니다.");
                        }
                    }
                    else
                    {
                        Debug.LogError("\"BackGround\"라는 이름의 배경 오브젝트를 찾을 수 없습니다.");
                    }

                    // Gojungnum에 해당하는 오브젝트를 스폰합니다.
                    if (colorData.Gojungnum > 0 && colorData.Gojungnum <= Gojungtype.Length)
                    {
                        Instantiate(Gojungtype[colorData.Gojungnum - 1], new Vector3(0, 0, 0), Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogError("Gojungnum 값이 배열 범위를 벗어납니다.");
                    }
                }
                else
                {
                    Debug.LogError("유효하지 않은 색상 코드입니다: " + colorData.Color);
                }
            }
            else
            {
                Debug.LogError($"ID가 {stage}인 데이터를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("JSON 파일이 할당되지 않았습니다.");
        }
    }
}