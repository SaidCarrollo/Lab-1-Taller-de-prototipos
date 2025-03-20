using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scrips.Semana1
{
    public class Snake : MonoBehaviour
    {
        public static Snake Instance { get; private set; }

        public int Length = 1;
        public ScoreManager scoreManager;

        private List<Transform> snakeSegments = new List<Transform>();
        public Transform segmentPrefab;

        private Vector2 direction = Vector2.right;

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
            snakeSegments.Add(transform);
        }

        private void Update()
        {
            ValidateMovement();
        }

        public void MoveSnake(Vector2 dir)
        {
            direction = dir;
        }

        private void ValidateMovement()
        {
            Vector3 newPosition = transform.position;
            newPosition.x += direction.x;
            newPosition.y += direction.y;
            transform.position = newPosition;

            CheckMovement();
        }

        private void CheckMovement()
        {
            for (int i = 1; i < snakeSegments.Count; i++)
            {
                if (transform.position == snakeSegments[i].position)
                {
                    GameManager.Instance.GameOver();
                }
            }
        }

        public void UpdateLength()
        {
            Transform segment = Instantiate(segmentPrefab);
            segment.position = snakeSegments[snakeSegments.Count - 1].position;
            snakeSegments.Add(segment);
            Length++;
        }
    }
}