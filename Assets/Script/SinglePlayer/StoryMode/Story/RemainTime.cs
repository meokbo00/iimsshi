using System.Collections;
using TMPro;
using UnityEngine;

public class RemainTime : MonoBehaviour
{
    public TMP_Text timeDisplay;
    public int years;
    public int days;
    public int hours;
    public int minutes;
    public int seconds;

    void Start()
    {
        StartCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        while (true)
        {
            DisplayTime();
            yield return new WaitForSeconds(1f);
            DecreaseTime();
        }
    }

    void DisplayTime()
    {
        timeDisplay.text = $"�������� �ڵ� ��������\n{years}�� {days}�� {hours}�ð� {minutes}�� {seconds}��";
    }

    void DecreaseTime()
    {
        if (seconds > 0)
        {
            seconds--;
        }
        else
        {
            if (minutes > 0)
            {
                minutes--;
                seconds = 59;
            }
        }
    }
}