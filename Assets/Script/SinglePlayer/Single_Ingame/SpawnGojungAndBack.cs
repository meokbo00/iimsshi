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
        public float R;
        public float G;
        public float B;
        public float A;
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
                float r = colorData.R / 255f;
                float g = colorData.G / 255f;
                float b = colorData.B / 255f;
                float a = colorData.A / 255f;

                GameObject backgroundObject = GameObject.Find("BackGround");

                if (backgroundObject != null)
                {
                    SpriteRenderer spriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();

                    if (spriteRenderer != null)
                    {
                        spriteRenderer.color = new Color(r, g, b, a);
                    }
                    else
                    {
                        Debug.LogError("SpriteRenderer 컴포넌트를 찾을 수 없습니다.");
                    }
                }
                else
                {
                    Debug.LogError("\"BackGround\"라는 이름을 가진 오브젝트를 찾을 수 없습니다.");
                }

                // Gojungnum에 해당하는 오브젝트를 생성합니다.
                if (colorData.Gojungnum > 0 && colorData.Gojungnum <= Gojungtype.Length)
                {
                    Instantiate(Gojungtype[colorData.Gojungnum - 1], new Vector3(0, 0, 0), Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Gojungnum 값이 배열 범위를 벗어났습니다.");
                }
            }
            else
            {
                Debug.LogError($"ID가 {stage}인 데이터를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("JSON 파일이 지정되지 않았습니다.");
        }
    }
}