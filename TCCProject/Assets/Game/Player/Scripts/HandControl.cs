using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HandControl : MonoBehaviour
{
    [SerializeField] GameObject btnDropHand, btnCallHand;

    [SerializeField] NavMeshAgent handToDrop;
    public float weight;
    bool wtHand = true;
    bool coming = false;

    Rigidbody2D rb;

    GameObject dropedHand = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        handToDrop.updateRotation = false;
        handToDrop.updateUpAxis = false;
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
        if (coming)
        {
            dropedHand.GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
        
    }
    public void DropHand()
    {
        rb.mass = rb.mass - weight;
        dropedHand = Instantiate(handToDrop.gameObject, transform.position, transform.transform.rotation);
            wtHand = false;
        coming = false;
    }
    public void CallHand()
    {
        coming = true;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);

        if(collision.gameObject == dropedHand)
        {
            if (coming == true)
            {
                Destroy(collision.gameObject);
                wtHand = true;
                coming = false;
                print("chegou");
                rb.mass = rb.mass + weight; 
            }

        }
    }
}
