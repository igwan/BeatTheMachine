using UnityEngine;
using Prime31.StateKit;

namespace GameState
{
    public class IntroScreen : SKState<GameManager>
    {
        public override void begin()
        {
        #if DEBUG
            Debug.Log("IntroScreen start");
        #endif
        }

        public override void reason()
        {
            // test for input before
            _machine.changeState<PreGame>();
        }

        public override void update(float deltaTime)
        {
        }
    }
}
