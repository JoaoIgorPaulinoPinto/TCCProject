using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class HandControl : MonoBehaviour
{
    [SerializeField] GameObject btnDropHand, btnCallHand;
    [SerializeField] GameObject handToDrop;

   
    public float weight;
    public bool wtHand = true;
    bool coming = false;

    Rigidbody2D rb;

    public GameObject dropedHand = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (dropedHand != null)
        {
            if (Vector2.Distance(transform.position, dropedHand.transform.position) < 1f && coming)
            {
                Destroy(dropedHand);
                wtHand = true;
                coming = false;
                print("chegou");
                rb.mass = rb.mass + weight;
                dropedHand = null;
            }
        }
        if (wtHand)
        {
            btnCallHand.SetActive(false);
            btnDropHand.SetActive(true);
        }
        else
        {
            btnDropHand.SetActive(false);
            btnCallHand.SetActive(true);
        }
        if (dropedHand != null)
        {
            if (coming)
            {
                dropedHand.GetComponent<HandMov>().called = true;
            }
            else
            {
                dropedHand.GetComponent<HandMov>().called = false;
            }
        }

    }
    public void DropHand()
    {
        rb.mass = rb.mass - weight;
        dropedHand = Instantiate(handToDrop.gameObject, transform.position, handToDrop.transform.rotation);
        wtHand = false;
        coming = false;
    }
    public void CallHand()
    {
        coming = true;
        
    }
}
