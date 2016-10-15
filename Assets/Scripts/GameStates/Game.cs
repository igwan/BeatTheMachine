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

            _context.soundManager.Instance.StartGameMusic();
            _context.EndGameEvent.AddListener(gameEnd);
        }

        void gameEnd()
        {
            _context.EndGameEvent.RemoveListener(gameEnd);
            _machine.changeState<PostGame>();
        }

        public override void update(float deltaTime)
        {

        }
    }
}
