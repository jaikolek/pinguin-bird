using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinBird.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;

        private float speed;
        public float Speed
        {
            set
            {
                speed = value;
                ChangeSpeed(value);
            }

            get => speed;
        }

        private int score;
        public int Score
        {
            set
            {
                score = value;
                //  todo:: change UI
            }

            get => score;
        }

        private int highScore;

        private void Awake()
        {
            Speed = 2f;
            player.SetCurrentGameManager(this);

            Score = 0;
            highScore = 0;
        }

        private void Update()
        {
            if (InputManager.i.ScreenTouched || InputManager.i.KeyboardPressed || InputManager.i.MousePressed)
            {
                player.Flap();
            }
        }

        public void GameOver()
        {
            Debug.Log($"Game Over");
        }

        public void IncreaseScore()
        {
            Score += 1;
            Debug.Log($"Score: {Score}");
        }

        private void ChangeSpeed(float speed)
        {
            Parallax[] parallaxs = FindObjectsOfType<Parallax>();

            for (int i = 0; i < parallaxs.Length; i++)
            {
                switch (parallaxs[i].ParallaxType)
                {
                    case EParallaxType.Slow:
                        parallaxs[i].AnimationSpeed = (speed * 1 / 7) * 3 / 4;
                        Debug.Log($"Slow: {parallaxs[i].AnimationSpeed}");
                        break;

                    case EParallaxType.HalfMedium:
                        parallaxs[i].AnimationSpeed = (speed * 1 / 6) * 1 / 2;
                        Debug.Log($"HalfMedium: {parallaxs[i].AnimationSpeed}");
                        break;

                    case EParallaxType.Medium:
                        parallaxs[i].AnimationSpeed = speed * 1 / 5;
                        Debug.Log($"Medium: {parallaxs[i].AnimationSpeed}");
                        break;

                    case EParallaxType.Normal:
                        parallaxs[i].AnimationSpeed = speed * 1 / 4;
                        Debug.Log($"Normal: {parallaxs[i].AnimationSpeed}");
                        break;

                    default:
                        Debug.Log($"Err::Can't find any parallax type::{this.name}");
                        break;
                }
            }

            PipesManager.i.Speed = speed * 2f;
        }
    }
}
