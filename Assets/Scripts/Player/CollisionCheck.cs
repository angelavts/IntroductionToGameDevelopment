using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            Debug.Log("Collision with Heart detected!");
        }
        
        Debug.Log(collision.gameObject.name);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            Debug.Log("Collision EXIT with Heart detected!");
        }
        Debug.Log("EXIT" + collision.gameObject.name);
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            Debug.Log("Collision stay with Heart detected!");
        }
        Debug.Log("STAY" + collision.gameObject.name);
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Heart"))
        {
            Debug.Log("Trigger with Heart detected!");
        }
    }*/
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Heart"))
        {
            Debug.Log("Trigger with Heart detected!");
            Destroy(other.gameObject); // Destroy the heart object on trigger
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Heart"))
        {
            Debug.Log("Trigger EXIT with Heart detected!");
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Heart"))
        {
            Debug.Log("Trigger stay with Heart detected!");
        }
    }
}
