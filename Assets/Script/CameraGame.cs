using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGame : MonoBehaviour
{
    [SerializeField] Material GLDraw;
    GameObject player;
    float camHeight, camWidth;
    Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        cam.aspect = 4f / 3f;   
        camHeight = 2 * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y;
        transform.position = pos;
    }
}
