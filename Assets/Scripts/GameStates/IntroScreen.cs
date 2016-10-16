using UnityEngine;
using Prime31.StateKit;
using System.Collections;

namespace GameState
{
    public class IntroScreen : SKState<GameManager>
    {
        public override void begin()
        {
        #if DEBUG
            Debug.Log("IntroScreen start");
        #endif
			SoundManager.Instance.PlayMusic(SoundManager.Instance.titleScreenMusic);
            _context.StartCoroutine(WaitAndAnnounce());
			_context.TitleScreen.SetActive (true);
        }

        IEnumerator WaitAndAnnounce()
        {
            yield return new WaitForSeconds(1f);
            SoundManager.Instance.PlaySound(SoundManager.Instance.titleScreenAnnounce);
        }

        public override void reason()
        {
            if(Input.anyKeyDown)
            {
                SoundManager.Instance.StopMusic();
				_context.TitleScreen.SetActive (false);
                _machine.changeState<PreGame>();
            }
        }

        public override void update(float deltaTime)
        {
        }
    }
}
