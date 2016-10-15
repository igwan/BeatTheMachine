using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    Animator animator;

    public class DeathEvent : UnityEvent {};
    public DeathEvent Die = new DeathEvent();

    int health = 1;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        health--;

        if(health < 0)
        {
            Die.Invoke();
        }

    }
}
