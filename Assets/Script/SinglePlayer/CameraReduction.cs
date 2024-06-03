using UnityEngine;
using TMPro;

public class CameraReduction : MonoBehaviour
{
    private Camera mainCamera;
    private int currentIndex = 0;
    private float[] sizes = { 15f, 30f, 45f };
    private string[] sizeTexts = { "100%", "75%", "50%" };

    public TextMeshProUGUI buttonText; // ��ư �ؽ�Ʈ ����

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
        }
        UpdateButtonText(); // �ʱ� �ؽ�Ʈ ����
    }

    public void ChangeCameraSize()
    {
        if (mainCamera != null)
        {
            currentIndex = (currentIndex + 1) % sizes.Length;
            mainCamera.orthographicSize = sizes[currentIndex]; // ī�޶� ũ�� ��� ����
            UpdateButtonText();
        }
    }

    private void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = sizeTexts[currentIndex];
        }
    }
}