using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch1_2Story : MonoBehaviour
{
    Ch1Story ch1Story;
    void Start()
    {
        ch1Story = FindObjectOfType<Ch1Story>();
        if(ch1Story.WatchPrologue2)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
