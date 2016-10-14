using UnityEngine;
using Prime31.StateKit;

namespace GameState
{
    public class PostGame : SKState<GameManager>
    {
        public override void begin()
        {
        #if DEBUG
            Debug.Log("PostGame start");
        #endif
        }

        public override void update(float deltaTime)
        {

        }
    }
}
