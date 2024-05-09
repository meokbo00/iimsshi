using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

class Chat
{
    public int id;
    public string text;
    public float delaytime; // delaytime 추가
}

public class ShowText : MonoBehaviour
{
    public TMP_Text chatting;
    private List<Chat> chats;
    private int currentChatIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        var asset = Resources.Load<TextAsset>("text");
        var json = asset.text;
        chats = JsonConvert.DeserializeObject<List<Chat>>(json);
        DisplayCurrentChat();
    }

    void Update()
    {
        if (!isTyping && Input.GetMouseButtonDown(0))
        {
            currentChatIndex++;
            DisplayCurrentChat();
        }
    }

    private void DisplayCurrentChat()
    {
        if (currentChatIndex < chats.Count)
        {
            StartCoroutine(Typing(chats[currentChatIndex].text, chats[currentChatIndex].delaytime)); // delaytime 전달
        }
        else
        {
            chatting.gameObject.SetActive(false);
        }
    }

    IEnumerator Typing(string talk, float delaytime) // delaytime 매개변수 추가
    {
        isTyping = true;
        chatting.text = null;

        for (int i = 0; i < talk.Length; i++)
        {
            chatting.text += talk[i];
            yield return new WaitForSeconds(delaytime);
        }

        isTyping = false;
    }
}