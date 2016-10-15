using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
	private enum PlayerAction{
		STOP,WALK,JUMP,IDLE
	}

	private PlayerAction currentAction ;

	private Bezier bezierUtil = new Bezier();

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

	//
	public void Walk(){
		//set Objectif 
		targetPosition = transform.position + new Vector3(distance,0,0);
		//Launch Animation
		animator.SetTrigger("Walk");
		//set PlayerAction
		currentAction = PlayerAction.WALK ;
		Debug.Log("Walk");
	}

    public void Jump()
    {
		//setObjectif
		targetPosition = transform.position + new Vector3(distance,distance,0);
		float startPointX = transform.position.x;
		float startPointY = transform.position.y;

		float endPointX = targetPosition.x;
		float endPointY = targetPosition.y;

		float controlPointX = transform.position.x;
		float controlPointY = targetPosition.y;
		bezierUtil.setCurve (startPointX, startPointY, controlPointX, controlPointY, targetPosition,transform.position.z,speed);
		//Launch Animation
		animator.SetTrigger("Jump");
		currentAction = PlayerAction.JUMP ;
    }

    public void Dash()
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
			Debug.Log ("Hit");
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


	private bool mustMove(){
		return this.transform.position != this.targetPosition;
	}

	public void MoveToTarget(){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);

	}

   IEnumerator StopInvulnerability(){
      yield return new WaitForSeconds(1f);
      vulnerability = true;
   }

	void FixedUpdate(){
		if (this.mustMove ()) {
			if (currentAction == PlayerAction.WALK)
				this.MoveToTarget ();
			else if (currentAction == PlayerAction.JUMP) {
				this.transform.position = this.bezierUtil.UpdateCurve ();
				if (!this.mustMove())
					this.currentAction = PlayerAction.IDLE;
			}
		}
	}
}
