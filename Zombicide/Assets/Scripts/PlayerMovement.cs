using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed;

    public Rigidbody2D RB;
    public Camera Cam;

    Vector2 movement;
    Vector2 MousePos;

    private void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        //Movement
        RB.MovePosition(RB.position + movement * MoveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = MousePos - RB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        RB.rotation = angle;
    }

}
