using UnityEngine;

namespace PowerUps
{
    public class EnlargeBall : MonoBehaviour, IPowerUp
    {
        public void ApplyPowerUp()
        {
            var balls = GameObject.FindGameObjectsWithTag("Ball");

            foreach (var ball in balls)
            {
                var currentScale = ball.transform.localScale;
                ball.transform.localScale =
                    new Vector3(currentScale.x * 1.5f, currentScale.y * 1.5f, currentScale.z * 1.5f);
            }
            Destroy(gameObject);
        }
    }
}