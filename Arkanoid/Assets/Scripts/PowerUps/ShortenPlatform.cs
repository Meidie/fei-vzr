using UnityEngine;

namespace PowerUps
{
    public class ShortenPlatform : MonoBehaviour, IPowerUp
    {
        private const float ScaleFactor = 1.2f;

        public void ApplyPowerUp()
        {
            var platform = GameObject.FindGameObjectWithTag("Platform").GetComponent<Platform>();
            var currentScale = platform.transform.localScale;
            var newXScale = currentScale.x / ScaleFactor;
            platform.MaxXPosition -= newXScale - 1;
            platform.MinXPosition = newXScale;
            platform.gameObject.transform.localScale =
                new Vector3(newXScale, currentScale.y, currentScale.z);
            Destroy(gameObject);
        }
    }
}