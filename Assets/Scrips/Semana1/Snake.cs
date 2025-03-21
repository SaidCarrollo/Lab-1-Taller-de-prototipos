using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Assets.Scrips.Semana1
{
    public class Snake : MonoBehaviour
    {

        public int Length = 1;
        public ScoreManager scoreManager;

        private List<Transform> snakeSegments = new List<Transform>();
        public Transform segmentPrefab;

        private Vector3 direction = Vector3.right; // Cambiado a Vector3
        private float moveDelay = 0.2f;
        private float moveTimer = 0f;
        private float fixedY; // Para mantener la serpiente en el plano 3D

        private SnakeControls controls;
        private Vector2 inputDirection;

        private void Awake()
        {


            controls = new SnakeControls();
            controls.Gameplay.Move.performed += ctx => inputDirection = ctx.ReadValue<Vector2>();
            controls.Enable();
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
            if (inputDirection.y > 0 && direction != Vector3.back)
                direction = Vector3.forward;
            else if (inputDirection.y < 0 && direction != Vector3.forward)
                direction = Vector3.back;
            else if (inputDirection.x < 0 && direction != Vector3.right)
                direction = Vector3.left;
            else if (inputDirection.x > 0 && direction != Vector3.left)
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
                scoreManager.UpdateScore(10);
                GameManager.Instance.SpawnApple();
                UpdateLength();
            }
        }

        public void UpdateLength()
        {
            Transform segment = Instantiate(segmentPrefab);
            segment.position = snakeSegments[snakeSegments.Count - 1].position;
            snakeSegments.Add(segment);
            Length++;
        }

        private void OnDestroy()
        {
            controls.Disable();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Block")
            {
                Destroy(gameObject);
            }
        }
    }
}
