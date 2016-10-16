using UnityEngine;
using Prime31.StateKit;

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
        }

        void gameOver()
        {
            _context.player.Die.RemoveListener(gameOver);
            _machine.changeState<GameOver>();
        }

        public override void update(float deltaTime)
        {

        }
    }
}
