using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public FixedJoystick Joystick;
    float pushingMovSpeed;
    float normalSpeed;

    private void Awake()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            
            float angle = Vector2.Angle(collision.contacts[0].normal, Vector2.up);
            if (angle > 90)
            {
                collision.gameObject.GetComponent<PlayerMoviment>().readyToMov = true;
            }
            else
            {
                if (collision.gameObject.GetComponent<PackageControler>().wtPackage != false)
                {

                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

                }
                else
                {
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    collision.gameObject.GetComponent<PlayerMoviment>().readyToMov = false;
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Joystick.Horizontal * 1, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
            }
            
        }   
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //velocidade esta continuando em 1 quando o jogador para de colidir 
            collision.gameObject.GetComponent<PlayerMoviment>().readyToMov = true;
        }
    }
}
