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
            this.transform.position += direction * Time.deltaTime;

            //  Tilt
            Vector3 rotation = this.transform.eulerAngles;
            rotation.z = direction.y * tilt;
            this.transform.eulerAngles = rotation;
        }

        private void OnEnable()
        {
            Vector3 position = this.transform.position;
            position.y = 0f;
            this.transform.position = position;
            direction = Vector3.zero;
        }

        public void SetCurrentGameManager(GameManager gameManager)
        {
            currentGameManager = gameManager;
        }

        public void Flap()
        {
            AudioManager.i.PlaySFXOneShot(EAudioType.Flap.ToString());
            direction = Vector3.up * strength;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Obstacle")
            {
                AudioManager.i.PlaySFXOneShot(EAudioType.Hit.ToString());
                currentGameManager.GameState = EGameState.GameOver;
            }
            else if (collision.tag == "Score")
            {
                AudioManager.i.PlaySFXOneShot(EAudioType.Point.ToString());
                currentGameManager.IncreaseScore();

                // level progression
                
            }
        }
    }
}
