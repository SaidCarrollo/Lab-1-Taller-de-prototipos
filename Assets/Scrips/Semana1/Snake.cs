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

        private Vector3 direction = Vector3.right; // Cambiado a Vector3
        private float moveDelay = 0.2f;
        private float moveTimer = 0f;
        private float fixedY; // Para mantener la serpiente en el plano 3D

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
            fixedY = transform.position.y; // Guardar la altura inicial de la serpiente
        }

        private void Update()
        {
            HandleInput();
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveDelay)
            {
                moveTimer = 0f;
                ValidateMovement();
            }
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.W) && direction != Vector3.back)
                direction = Vector3.forward;
            else if (Input.GetKeyDown(KeyCode.S) && direction != Vector3.forward)
                direction = Vector3.back;
            else if (Input.GetKeyDown(KeyCode.A) && direction != Vector3.right)
                direction = Vector3.left;
            else if (Input.GetKeyDown(KeyCode.D) && direction != Vector3.left)
                direction = Vector3.right;

            if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector3.back)
                direction = Vector3.forward;
            else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector3.forward)
                direction = Vector3.back;
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector3.right)
                direction = Vector3.left;
            else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector3.left)
                direction = Vector3.right;
        }

        private void ValidateMovement()
        {
            Vector3 newPosition = transform.position + direction;
            newPosition.y = fixedY; // Mantiene la serpiente en el plano
            transform.position = newPosition;

            CheckMovement();

            for (int i = snakeSegments.Count - 1; i > 0; i--)
            {
                snakeSegments[i].position = snakeSegments[i - 1].position;
            }
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Apple"))
            {
                Destroy(other.gameObject);
                UpdateLength();
                scoreManager.UpdateScore(10);
                GameManager.Instance.SpawnApple();
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