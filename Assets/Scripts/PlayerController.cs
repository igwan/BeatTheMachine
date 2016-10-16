using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ProximityScanner))]
public class PlayerController : MonoBehaviour
{
	private enum PlayerAction{
		WALK,JUMP,DASH,IDLE,FALL,JUMP2
	}

	private PlayerAction currentAction = PlayerAction.IDLE ;

	private Bezier bezierUtil = new Bezier();

	private ProximityScanner scanner ;

    Animator animator;

    public SpriteRenderer missedHind;

    public class DeathEvent : UnityEvent {};
    public DeathEvent Die = new DeathEvent();

    int defaultHealth = 1;
    int health = 1;

    [SerializeField] bool godMode;


    void Awake()
    {
        animator = GetComponent<Animator>();
		scanner = GetComponent<ProximityScanner> ();
    }



	public void AddDeathEvent(UnityAction action){
		this.Die.AddListener (action);
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

    public void Reset()
    {
        health = defaultHealth;
    }

	//
	public void Walk(){
		if (!scanner.SomethingFront() && currentAction == PlayerAction.IDLE) {
			//set Objectif 
			targetPosition = transform.position + new Vector3 (distance, 0, 0);
			//Launch Animation
			animator.SetTrigger ("Walk");
			//set PlayerAction
			currentAction = PlayerAction.WALK;
			Debug.Log ("Walk");
		} else {
			this.Hit ();
		}
	}

    public void Jump()
    {
		if (!scanner.SomethingUp () && !scanner.SomethingUpFront() && currentAction == PlayerAction.IDLE) {
			//setObjectif
			targetPosition = transform.position + new Vector3 (distance, distance, 0);
			float startPointX = transform.position.x;
			float startPointY = transform.position.y;

			float controlPointX = transform.position.x;
			float controlPointY = targetPosition.y;
			bezierUtil.setCurve (startPointX, startPointY, controlPointX, controlPointY, targetPosition, transform.position.z, speed);
			//Launch Animation
			animator.SetTrigger ("Jump");
			currentAction = PlayerAction.JUMP;
		} else {
			this.Hit ();
		}
    }

	public void Jump2()
	{	
		//setObjectif
		targetPosition = transform.position + new Vector3 (distance, -distance, 0);
		float startPointX = transform.position.x;
		float startPointY = transform.position.y;

		float controlPointX = targetPosition.x;
		float controlPointY = transform.position.y;
		bezierUtil.setCurve (startPointX, startPointY, controlPointX, controlPointY, targetPosition, transform.position.z, speed);
		//Launch Animation
		animator.SetTrigger ("Fall");
		currentAction = PlayerAction.JUMP2;
}


    public void Dash()
    {
		if (!scanner.SomethingFarFront ()  && currentAction == PlayerAction.IDLE) {
			targetPosition = transform.position + new Vector3 (distance * 2, 0, 0);
			Debug.Log ("Dash");
			//Launch Animation
			animator.SetTrigger ("Dash");
			currentAction = PlayerAction.DASH;
		} else {
			Debug.Log ("bad Dash");
			this.Hit ();
		}
    }

    public void Stop()
    {
        Debug.Log("Stop");
		//Launch Animation
		animator.SetTrigger("Stop");

    }

	public bool mustFall(){
		return !this.scanner.SomethingDown ();
	}

	public void Fall(){
		targetPosition = transform.position - new Vector3 (0, distance, 0);
		animator.SetTrigger ("Fall");
		currentAction = PlayerAction.FALL ;
	}

    public void Hit() {
		//Launch Animation
		if (vulnerability) {
			//Debug.Log ("Hit");
            ShowMissedHind();
			animator.SetTrigger ("Hit");
			health--;

			if (health < 0 && !godMode) {
                Death();
			}
            else
            {
                vulnerability = false;
                StartCoroutine(StopInvulnerability());
            }
		}
    }

    public void Death()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.deathScream);
        Die.Invoke();
    }

	private bool mustMove(){
		return this.transform.position != this.targetPosition;
	}

	public void WalkToTarget(){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);
		endTarget ();
	}

	public void DashToTarget(){
		float step = speed * Time.deltaTime * 2;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);
		endTarget ();
	}

	public void FallToTarget(){
		float step = speed * Time.deltaTime * 2;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);
		endTarget ();
	}

	public void JumpToTarget(){
		this.transform.position = this.bezierUtil.UpdateCurve ();
		if (!this.mustMove ()) { //si fin
			if (mustFall ()) {
				if (!scanner.SomethingFront () && !scanner.SomethingFrontDown() && currentAction == PlayerAction.JUMP )
					Jump2 ();
				else
					Fall ();
			}
			else this.currentAction = PlayerAction.IDLE;
		}
	}

	public void endTarget(){
		if (!this.mustMove ()) {//si fin
			if (mustFall ()) Fall();
			else this.currentAction = PlayerAction.IDLE;
		}
	}

   IEnumerator StopInvulnerability(){
      yield return new WaitForSeconds(1f);
      vulnerability = true;
   }

	void FixedUpdate(){
		if (currentAction == PlayerAction.WALK) {
			this.WalkToTarget ();
		} else if (currentAction == PlayerAction.JUMP || currentAction == PlayerAction.JUMP2) {
			this.JumpToTarget ();
		} else if (currentAction == PlayerAction.DASH) {
			this.DashToTarget ();
		} else if (currentAction == PlayerAction.FALL) {
			this.FallToTarget ();
		}
	}

    public void ShowMissedHind()
    {
        missedHind.enabled = true;
        StartCoroutine(HideInSeconds(1f));
    }

    IEnumerator HideInSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        missedHind.enabled = false;
    }
}
