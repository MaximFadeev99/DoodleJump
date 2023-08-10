using System;
using UnityEngine;

public class Death : MonoBehaviour
{
    public static Action playerDied;

    private void OnTriggerEnter2D (Collider2D collision)
    {              
        if (collision.GetComponent<Player>()) 
        {
            playerDied.Invoke();
        }
    }
}
