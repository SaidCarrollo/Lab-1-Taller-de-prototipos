using System.Collections;
using UnityEngine;

namespace Assets.Scrips.Semana1
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Snake player;
        public GameObject Apple;
        public Vector2 spawnArea = new Vector2(10, 10); // Tamaño del área de generación

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SpawnApple();
        }

        public void SpawnApple()
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                player.transform.position.y, // Asegurar que spawnee a la misma altura que la serpiente
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
            );

            Instantiate(Apple, spawnPosition, Quaternion.identity);
        }

        public void GameOver()
        {
            Debug.Log("¡Game Over!");
        }
    }
}