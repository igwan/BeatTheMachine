using UnityEngine;
using Prime31.StateKit;
using UnityEngine.SceneManagement;

namespace GameState
{
    public class Restart : SKState<GameManager>
    {
        public override void begin()
        {
        #if DEBUG
            Debug.Log("Restart");
        #endif
        }

        public override void reason()
        {
			Scene scene = SceneManager.GetActiveScene ();
			SceneManager.LoadScene (scene.name);
			_machine.changeState<PreGame> ();
        }

        public override void update(float deltaTime)
        {
        }
    }
}
