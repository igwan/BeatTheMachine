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


	//speed
	public float speed = 1f ;

    public float InvulnerabilityDuration = 1f;

	//distance between Tile 
	private float distance ;

	private Vector3 targetPosition ;

	private bool vulnerability = true;

	void Start(){
		this.distance = MapManager.Instance.tileLength;
		targetPosition = this.transform.position;
	}
		
	private bool mustMove(){
		return this.transform.position != this.targetPosition;
	}

	public void MoveToTarget(){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);

	}

	public void Walk(){
		//set Objectif 
		targetPosition = transform.position + new Vector3(distance,0,0);
		//Launch Animation
		animator.SetTrigger("Walk");
		Debug.Log("Walk");
	}

    public void Jump()
    {
        Debug.Log("Jump");
		//Launch Animation
		animator.SetTrigger("Jump");
    }

    public void Sprint()
    {
        Debug.Log("Sprint");
		//Launch Animation
		animator.SetTrigger("Sprint");
    }

    public void Stop()
    {
        Debug.Log("Stop");
		//Launch Animation
		animator.SetTrigger("Stop");

    }

    public void Hit() {
		//Launch Animation
		if (vulnerability) {
			animator.SetTrigger ("Hit");
			health--;

			if (health < 0) {
				Die.Invoke ();
			}
            else
            {
                vulnerability = false;
                StartCoroutine(StopInvulnerability());
            }
		}
    }

    IEnumerator StopInvulnerability()
    {
        yield return new WaitForSeconds(1f);
        vulnerability = true;
    }

//	void FixedUpdate(){
//		if (!vulnerability) {
//			this.TimerInvulnerability -= Time.deltaTime;
//            Debug.Log(TimerInvulnerability);
//			if (this.TimerInvulnerability < 0) {
//				vulnerability = true;
//                TimerInvulnerability = InvulnerabilityDuration;
//			}
//		}
//		if (this.mustMove ()) {
//			this.MoveToTarget ();
//		}
//	}
}
