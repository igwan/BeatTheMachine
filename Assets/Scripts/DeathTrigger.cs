using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            //player.Death();
        }
    }
}
