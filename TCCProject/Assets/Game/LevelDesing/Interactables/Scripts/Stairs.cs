using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public FixedJoystick Joystick;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Joystick.AxisOptions = AxisOptions.Horizontal;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.transform.tag == "Player")
        {
            if(collision.gameObject.GetComponent<HandControl>().wtHand == true)
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

                rb.velocity = new Vector2(rb.velocity.x, Joystick.Vertical * 3);
                Joystick.AxisOptions = AxisOptions.Both;
            }

        }
    }
}
