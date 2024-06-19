using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

class TextWithDelay
{
    public string text;
    public float delay;
}

class Chat
{
    public int id;
    public List<TextWithDelay> textWithDelay;
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
    private int chatIdToDisplay = -1;

    void Awake()
    {
        ChatBox.gameObject.SetActive(true);
        if (JsonFile != null)
        {
            try
            {
                var json = JsonFile.text;
                chats = JsonConvert.DeserializeObject<List<Chat>>(json);
            }
            catch (JsonReaderException e)
            {
                Debug.LogError("Failed to parse JSON: " + e.Message);
            }
        }
        else
        {
            //Debug.LogError("JsonFile is not assigned.");
        }
    }

    void OnEnable()
    {
        if (chatIdToDisplay != -1)
        {
            DisplayChatById(chatIdToDisplay);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                chatting.text = chats[currentChatIndex].textWithDelay[currentTextIndex].text;
                isTyping = false;
            }
            else
            {
                currentTextIndex++;
                if (currentTextIndex >= chats[currentChatIndex].textWithDelay.Count)
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
            if (currentTextIndex < chats[currentChatIndex].textWithDelay.Count)
            {
                var textWithDelay = chats[currentChatIndex].textWithDelay[currentTextIndex];
                typingCoroutine = StartCoroutine(Typing(textWithDelay.text, textWithDelay.delay));
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

    public void DisplayChatById(int chatId)
    {
        chatIdToDisplay = chatId;

        var chat = chats.Find(c => c.id == chatId);
        if (chat != null)
        {
            currentChatIndex = chats.IndexOf(chat);
            currentTextIndex = 0;
            DisplayCurrentChat();
        }
        else
        {
            Debug.LogError("Chat with ID " + chatId + " not found.");
        }
    }
}