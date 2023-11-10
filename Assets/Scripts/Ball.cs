using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 initialPosition;

    public GameObject soldier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position += (rb.velocity * Time.deltaTime) +
                              (0.5f * Physics.gravity * Time.deltaTime * Time.deltaTime);
        rb.velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= -5)
        {
            Destroy(gameObject);
        }

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Soldier"))
        {
            soldier.GetComponent<Soldier>().removeHealth(rb.velocity.x);
        }
    }

}
