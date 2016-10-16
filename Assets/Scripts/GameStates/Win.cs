using UnityEngine;
using System.Collections;
using Prime31.StateKit;

namespace GameState
{
    public class Win : SKState<GameManager>
    {
        public override void begin()
        {
        #if DEBUG
            Debug.Log("Win start");
        #endif
            _context.WinUI.SetActive(true);
            _context.StartCoroutine(WaitAndRestart(_context.postGameDuration));
        }

        IEnumerator WaitAndRestart(float duration)
        {
            yield return new WaitForSeconds(duration);
            _context.WinUI.SetActive(false);
            _machine.changeState<GameState.IntroScreen>();
        }

        public override void update(float deltaTime)
        {

        }
    }
}
