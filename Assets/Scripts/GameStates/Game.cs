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
        }

        public override void update(float deltaTime)
        {

        }
    }
}
