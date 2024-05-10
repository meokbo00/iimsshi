using UnityEngine;

public class CameraControl : MonoBehaviour 
{
    GameObject player;

    void Start()
    {
        this.player = GameObject.Find("cat");
    }
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(transform.position.x, playerPos.y, transform.position.z);
    }
}