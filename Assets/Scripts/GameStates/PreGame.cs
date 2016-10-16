using UnityEngine;
using Prime31.StateKit;

namespace GameState
{
    public class PreGame : SKState<GameManager>
    {
        public override void begin()
        {
        #if DEBUG
            Debug.Log("PreGame start");
        #endif
            SoundManager.Instance.PlayMusic(SoundManager.Instance.mainTheme);

            // wait a little before
            _machine.changeState<Game>();
        }

        public override void reason()
        {
        }

        public override void update(float deltaTime)
        {
        }
    }
}
