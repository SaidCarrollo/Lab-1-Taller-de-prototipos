using System.Collections;
using UnityEngine;

namespace Assets.Scrips.Semana1
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }

        public Snake player;
        public GameObject apple;

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

        public void SpawnApple()
        {
            Vector2 position = new Vector2(Random.Range(-9, 9), Random.Range(-9, 9));
            Instantiate(apple, position, Quaternion.identity);
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            // Reiniciar o mostrar pantalla de Game Over
        }
    }
}