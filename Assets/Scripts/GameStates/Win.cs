using UnityEngine;
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
        }

        public override void update(float deltaTime)
        {

        }
    }
}
