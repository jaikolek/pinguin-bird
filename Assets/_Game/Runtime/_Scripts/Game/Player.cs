using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinBird.Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float gravity = -18f;
        [SerializeField] private float strength = 7f;
        [SerializeField] private float tilt = 5f;

        private GameManager currentGameManager;

        private Vector3 direction;

        private void Update()
        {
            //  Apply gravity and direction
            direction.y += gravity * Time.deltaTime;
            transform.position += direction * Time.deltaTime;

            //  Tilt
            Vector3 rotation = transform.eulerAngles;
            rotation.z = direction.y * tilt;
            transform.eulerAngles = rotation;
        }

        public void SetCurrentGameManager(GameManager gameManager)
        {
            currentGameManager = gameManager;
        }

        public void Flap()
        {
            direction = Vector3.up * strength;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Obstacle")
            {
                currentGameManager.GameOver();
            }
            else if (collision.tag == "Score")
            {
                currentGameManager.IncreaseScore();
            }
        }
    }
}
