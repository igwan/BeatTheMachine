using UnityEngine;
using Prime31.StateKit;
using System.Collections;

namespace GameState
{
    public class Game : SKState<GameManager>
    {
        public override void begin()
        {
        #if DEBUG
            Debug.Log("Game start");
        #endif

            SoundManager.Instance.musicTempo.enabled = true;
            SoundManager.Instance.StartGameMusic();
            _context.player.Die.AddListener(gameOver);
            MapManager.Instance.EndLevel.AddListener(win);
        }

        void gameOver()
        {
            _context.player.Die.RemoveListener(gameOver);
			_context.StartCoroutine (WaitAndGameOver ());
		}

		IEnumerator WaitAndGameOver()
		{
			yield return new WaitForSeconds (3f);
			_machine.changeState<GameOver> ();
		}


        void win()
        {
            MapManager.Instance.EndLevel.RemoveListener(win);
            _machine.changeState<Win>();
        }

        public override void update(float deltaTime)
        {

        }
    }
}
