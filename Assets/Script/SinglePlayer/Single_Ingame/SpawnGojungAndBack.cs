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
                        Debug.LogError("SpriteRenderer ������Ʈ�� ã�� �� �����ϴ�.");
                    }
                }
                else
                {
                    Debug.LogError("\"BackGround\"��� �̸��� ���� ������Ʈ�� ã�� �� �����ϴ�.");
                }

                // Gojungnum�� �ش��ϴ� ������Ʈ�� �����մϴ�.
                if (colorData.Gojungnum > 0 && colorData.Gojungnum <= Gojungtype.Length)
                {
                    Instantiate(Gojungtype[colorData.Gojungnum - 1], new Vector3(0, 0, 0), Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Gojungnum ���� �迭 ������ ������ϴ�.");
                }
            }
            else
            {
                Debug.LogError($"ID�� {stage}�� �����͸� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("JSON ������ �������� �ʾҽ��ϴ�.");
        }
    }
}