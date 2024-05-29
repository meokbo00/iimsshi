using UnityEngine;

public class CameraControl : MonoBehaviour 
{
    GameObject player;
<<<<<<< HEAD
    public RectTransform stage1RectTransform;
    public Camera mainCamera;

    void Start()
    {
        this.player = GameObject.Find("Player");
=======

    void Start()
    {
        this.player = GameObject.Find("Eball");
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
    }
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }
}