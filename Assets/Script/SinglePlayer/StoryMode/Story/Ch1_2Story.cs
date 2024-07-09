using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ch1_2Story : MonoBehaviour
{
    Ch1Story ch1Story;
    RemainTime remainTime;
    public GameObject Fadeinout;
    public GameObject RemainTime;

    void Start()
    {
        ch1Story = FindObjectOfType<Ch1Story>();
        remainTime = GetComponent<RemainTime>();
    }

    void Update()
    {
        
    }
}
