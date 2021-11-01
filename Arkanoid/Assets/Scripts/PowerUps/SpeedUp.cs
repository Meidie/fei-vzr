using UnityEngine;

namespace PowerUps
{
    public class SpeedUp : MonoBehaviour, IPowerUp
    {

        public void ApplyPowerUp()
        {
            var gameState = FindObjectOfType<GameState>();
            gameState.IncreaseSpeed();
            Destroy(gameObject);
        }
    }
}
