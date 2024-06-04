using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

class Chat
{
    public int id;
    public string[] texts;
    public float[] delaytimes;
}

public class ShowText : MonoBehaviour
{
    public TMP_Text chatting;
    public GameObject ChatBox;
    public TextAsset JsonFile;
    private List<Chat> chats;
    private int currentChatIndex = 0;
    private int currentTextIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        ChatBox.gameObject.SetActive(true);
        if (JsonFile != null)
        {
            try
            {
                var json = JsonFile.text;
                chats = JsonConvert.DeserializeObject<List<Chat>>(json);
                DisplayCurrentChat();
            }
            catch (JsonReaderException e)
            {
                Debug.LogError("Failed to parse JSON: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("JsonFile is not assigned.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                chatting.text = chats[currentChatIndex].texts[currentTextIndex];
                isTyping = false;
            }
            else
            {
                currentTextIndex++;
                if (currentTextIndex >= chats[currentChatIndex].texts.Length)
                {
                    currentChatIndex++;
                    currentTextIndex = 0;
                }
                DisplayCurrentChat();
            }
        }
    }

    private void DisplayCurrentChat()
    {
        if (currentChatIndex < chats.Count)
        {
            if (currentTextIndex < chats[currentChatIndex].texts.Length)
            {
                typingCoroutine = StartCoroutine(Typing(chats[currentChatIndex].texts[currentTextIndex], chats[currentChatIndex].delaytimes[currentTextIndex]));
            }
            else
            {
                ChatBox.gameObject.SetActive(false);
            }
        }
        else
        {
            ChatBox.gameObject.SetActive(false);
        }
    }

    IEnumerator Typing(string talk, float delaytime)
    {
        isTyping = true;
        chatting.text = string.Empty;

        for (int i = 0; i < talk.Length; i++)
        {
            chatting.text += talk[i];
            yield return new WaitForSeconds(delaytime);
        }

        isTyping = false;
    }
}