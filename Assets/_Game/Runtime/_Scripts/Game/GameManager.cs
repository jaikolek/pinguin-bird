using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinBird.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private float speed;
        public float Speed
        {
            set
            {
                speed = value;
                ChangeSpeed(value);
            }

            get => speed;
        }

        private void Awake()
        {
            Speed = 2f;
        }

        private void Update()
        {
            if (InputManager.i.ScreenTouched || InputManager.i.KeyboardPressed || InputManager.i.MousePressed)
            {
                player.Flap();
            }
        }

        private void ChangeSpeed(float speed)
        {
            Parallax[] parallaxs = World.i.Parallaxs;

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
        }
    }
}
