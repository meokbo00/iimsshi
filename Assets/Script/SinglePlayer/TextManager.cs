using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject Linebox;

    public void GiveMeTextId(int chatId)
    {
        if (Linebox != null)
        {
            Linebox.SetActive(true); // Linebox를 활성화합니다.

            ShowText showTextScript = Linebox.GetComponent<ShowText>();
            if (showTextScript != null)
            {
                showTextScript.DisplayChatById(chatId);
            }
            else
            {
                Debug.LogError("ShowText script not found on Linebox.");
            }
        }
        else
        {
            Debug.LogError("Linebox is not assigned.");
        }
    }
}