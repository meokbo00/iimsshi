using UnityEngine;
using TMPro;

public class CameraReduction : MonoBehaviour
{
    private Camera mainCamera;
    private int currentIndex = 0;
    private float[] sizes = { 15f, 30f, 45f };
    private string[] sizeTexts = { "100%", "75%", "50%" };

    public TextMeshProUGUI buttonText; // 버튼 텍스트 참조

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
        }
        UpdateButtonText(); // 초기 텍스트 설정
    }

    public void ChangeCameraSize()
    {
        if (mainCamera != null)
        {
            currentIndex = (currentIndex + 1) % sizes.Length;
            mainCamera.orthographicSize = sizes[currentIndex]; // 카메라 크기 즉시 변경
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