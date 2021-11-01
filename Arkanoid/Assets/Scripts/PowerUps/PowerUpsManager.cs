using UnityEngine;

namespace PowerUps
{
    public class PowerUpsManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] powerUpPrefabs;


        public void SpawnPowerUp(Vector2 position)
        {
            if (Random.value <= 0.2)
            {
                var powerUp = Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)]);
                powerUp.transform.position = position;
            }
        }
    }
}