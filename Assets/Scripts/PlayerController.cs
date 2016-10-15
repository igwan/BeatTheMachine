using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public class DeathEvent : UnityEvent {};
    public DeathEvent Die = new DeathEvent();

    bool hasArmor = true;

    public void Jump()
    {
        Debug.Log("Jump");
    }

    public void Sprint()
    {
        Debug.Log("Sprint");
    }

    public void Stop()
    {
        Debug.Log("Stop");
    }

    public void Walk()
    {
        Debug.Log("Walk");
    }

    public void Hit()
    {
        if(hasArmor)
        {
            hasArmor = false;
        }
        else
        {
            Die.Invoke();
        }

    }
}
