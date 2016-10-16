using UnityEngine;
using System.Collections;

public class helpScript : MonoBehaviour {

	SpriteRenderer sprite ;

	// Use this for initialization
	void Start () {
		SoundManager.Instance.musicTempo.AddBeginEvent (PopSprite);
		SoundManager.Instance.musicTempo.AddEndEvent(DeleteSprite);
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}

	void DeleteSprite(){
		sprite.enabled = false;
	}

	void PopSprite(){
		sprite.enabled = true;
	}
}
