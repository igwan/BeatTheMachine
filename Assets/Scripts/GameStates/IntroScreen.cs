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
			_context.TitleScreen.SetActive (true);
        }

        public override void reason()
        {
			if (Input.anyKeyDown) {
				_context.TitleScreen.SetActive (false);
				_machine.changeState<PreGame> ();
			}
        }

        public override void update(float deltaTime)
        {
        }
    }
}
