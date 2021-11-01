using UnityEngine;

namespace PowerUps
{
    public class SlowDown : MonoBehaviour, IPowerUp
    {
        public void ApplyPowerUp()
        {
            var gameState = FindObjectOfType<GameState>();
            gameState.DecreaseSpeed();
            Destroy(gameObject);
        }
    }
}