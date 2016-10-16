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
			SoundManager.Instance.PlayMusic(SoundManager.Instance.titleScreen);
			_context.TitleScreen.SetActive (true);
        }

        public override void reason()
        {
            if(Input.anyKeyDown)
            {
                SoundManager.Instance.StopMusic();
			context.TitleScreen.SetActive (false);
                _machine.changeState<PreGame>();
            }
        }

        public override void update(float deltaTime)
        {
        }
    }
}
