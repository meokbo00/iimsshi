using UnityEngine;

public class CameraControl : MonoBehaviour 
{
    GameObject player;
<<<<<<< HEAD:Assets/Script/SinglePlay/Stage/CameraControl.cs
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
=======
    public RectTransform stage1RectTransform;
    public Camera mainCamera;

    void Start()
    {
        this.player = GameObject.Find("Player");
>>>>>>> b685f8518e130fb019b884e08be9052cb5c494b8:Assets/Script/SinglePlayer/Stage/CameraControl.cs
    }
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y+5, transform.position.z);
    }
}