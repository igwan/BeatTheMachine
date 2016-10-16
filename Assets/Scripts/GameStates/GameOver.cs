using UnityEngine;
using System.Collections;
using Prime31.StateKit;

namespace GameState
{
    public class GameOver : SKState<GameManager>
    {
        public override void begin()
        {
        #if DEBUG
            Debug.Log("GameOver start");
        #endif
            _context.GameOverUI.SetActive(true);
            _context.StartCoroutine(WaitAndRestart(_context.postGameDuration));
        }

        IEnumerator WaitAndRestart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _context.GameOverUI.SetActive(false);
            _machine.changeState<GameState.IntroScreen>();
        }

        public override void update(float deltaTime)
        {

        }
    }
}
