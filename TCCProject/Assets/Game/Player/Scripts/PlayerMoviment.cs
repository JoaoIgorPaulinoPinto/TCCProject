using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMoviment : MonoBehaviour
{
    [Header("Pulo")]
    public bool readyToJump;
    public bool readyToMov;
    [SerializeField] float jumpPower;
    [SerializeField] LayerMask layerwalkable;
    [SerializeField] Transform groundCheck;

    [Header("Controles")]
    public FixedJoystick Joystick;
    public float movSpeed;



    Rigidbody2D rb;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        readyToJump = Physics2D.OverlapCircle(groundCheck.position, 0.3f, layerwalkable);

        Mov();


    }
    public void Pulo()
    {
        if(readyToJump) rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

    }
    private void Mov()
    {
        if (readyToMov)
        {
            rb.velocity = new Vector2(Joystick.Horizontal * movSpeed, rb.velocity.y);
        }
    }


}
