using UnityEngine;

namespace PowerUps
{
    public class AddBall : MonoBehaviour, IPowerUp
    {

        [SerializeField]
        private GameObject ballPrefab;
        public void ApplyPowerUp()
        {
            var newBall = Instantiate(ballPrefab);
            newBall.GetComponent<BallAutoLaunch>().Platform = GameObject.FindGameObjectWithTag("Platform").GetComponent<Platform>();
            Destroy(gameObject);
        }
    }
}
