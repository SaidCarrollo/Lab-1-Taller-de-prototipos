using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scrips.Semana1
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public Snake player;
        public GameObject Apple;
        public Vector2 spawnArea = new Vector2(10, 10);

        private void Start()
        {
            SpawnApple();
        }

        public void SpawnApple()
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                player.transform.position.y,
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
            );
            Instantiate(Apple, spawnPosition, Quaternion.identity);
        }

        public void GameOver()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("¡Game Over!");
        }
    }
}