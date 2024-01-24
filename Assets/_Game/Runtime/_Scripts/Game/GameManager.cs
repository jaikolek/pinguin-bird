using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using PinguinBird.Storage;

namespace PinguinBird.Game
{
    public class GameManager : MonoBehaviour
    {
        [System.Serializable]
        public struct LevelProgression
        {
            public float minScore;
            public float maxScore;
            public float speed;
        }

        [SerializeField] LevelProgression[] levelProgressions;
        [SerializeField] private Player player;

        private float speed;
        public float Speed
        {
            set
            {
                speed = value;
                OnSpeedChanged(value);
            }

            get => speed;
        }

        private UnityEvent<int> onScoreChanged;
        public UnityEvent<int> OnScoreChanged
        {
            get
            {
                if (onScoreChanged == null)
                {
                    onScoreChanged = new UnityEvent<int>();
                }

                return onScoreChanged;
            }
        }

        private int score;
        public int Score
        {
            set
            {
                score = value;
                OnScoreChanged.Invoke(value);
            }

            get => score;
        }

        private UnityEvent<EGameState> onGameStateChanged;
        public UnityEvent<EGameState> OnGameStateChanged
        {
            get
            {
                if (onGameStateChanged == null)
                {
                    onGameStateChanged = new UnityEvent<EGameState>();
                }

                return onGameStateChanged;
            }
        }

        [SerializeField] private EGameState gameState;
        public EGameState GameState
        {
            set
            {
                gameState = value;
                OnGameStateChanged.Invoke(value);
            }

            get => gameState;
        }

        private bool alreadyTouched = false;
        private bool firstInteraction;

        private void Start()
        {
            HUD.i.GameplayUI.ObjPanel.SetActive(false);
            HUD.i.GameOverUI.ObjPanel.SetActive(false);

            OnScoreChanged.AddListener(HUD.i.GameplayUI.OnScoreChanged);
            OnScoreChanged.AddListener(OnScoreChange);
            OnGameStateChanged.AddListener(OnGameStateChange);
        }

        private void Awake()
        {
            firstInteraction = true;

            Speed = 2f;
            player.SetCurrentGameManager(this);

            Score = 0;

            GameState = EGameState.Start;
            Pause();
        }

        private void Update()
        {
            switch (GameState)
            {
                case EGameState.Start:
                    if (InputManager.i.ScreenTouched || InputManager.i.KeyboardPressed || InputManager.i.MousePressed)
                    {
                        GameState = EGameState.Play;
                    }
                    break;

                case EGameState.Play:
                    if ((InputManager.i.ScreenTouched || InputManager.i.KeyboardPressed || InputManager.i.MousePressed) && !alreadyTouched)
                    {
                        player.Flap();
                        alreadyTouched = true;
                    }
                    else if (!(InputManager.i.ScreenTouched || InputManager.i.KeyboardPressed || InputManager.i.MousePressed) && alreadyTouched)
                    {
                        alreadyTouched = false;
                    }
                    break;

                case EGameState.GameOver:
                    if (InputManager.i.ScreenTouched || InputManager.i.KeyboardPressed || InputManager.i.MousePressed)
                    {
                        GameState = EGameState.Play;
                    }
                    break;

                default:
                    Debug.Log($"Err::Can't find any game state::{this.name}");
                    break;
            }
        }

        private void Play()
        {
            Score = 0;
            player.enabled = true;

            PipesManager.i.DestroyPipes();

            Time.timeScale = 1f;
        }

        private void Pause()
        {
            Time.timeScale = 0f;
            player.enabled = false;
        }

        public void GameOver()
        {
            Pause();
        }

        public void IncreaseScore()
        {
            Score += 1;
        }

        private void OnGameStateChange(EGameState gameState)
        {
            switch (gameState)
            {
                case EGameState.Start:
                    //  HUD.i.StartUI.ObjPanel.SetActive(true);
                    //  Pause();
                    break;

                case EGameState.Play:
                    if (firstInteraction)
                    {
                        firstInteraction = false;
                        HUD.i.StartUI.ObjPanel.SetActive(false);
                    }
                    else HUD.i.GameOverUI.ObjPanel.SetActive(false);

                    HUD.i.GameplayUI.ObjPanel.SetActive(true);
                    Play();
                    break;

                case EGameState.GameOver:
                    HUD.i.GameplayUI.ObjPanel.SetActive(false);
                    HUD.i.GameOverUI.ObjPanel.SetActive(true);

                    if (Score > LoadManager.i.HighScore)
                    {
                        LoadManager.i.HighScore = Score;
                    }

                    HUD.i.GameOverUI.SetFinalScore(Score);
                    
                    GameOver();
                    break;

                default:
                    break;
            }
        }

        private void OnSpeedChanged(float speed)
        {
            Parallax[] parallaxs = FindObjectsOfType<Parallax>();

            for (int i = 0; i < parallaxs.Length; i++)
            {
                switch (parallaxs[i].ParallaxType)
                {
                    case EParallaxType.Slow:
                        parallaxs[i].AnimationSpeed = (speed * 1 / 7) * 3 / 4;
                        break;

                    case EParallaxType.HalfMedium:
                        parallaxs[i].AnimationSpeed = (speed * 1 / 6) * 1 / 2;
                        break;

                    case EParallaxType.Medium:
                        parallaxs[i].AnimationSpeed = speed * 1 / 5;
                        break;

                    case EParallaxType.Normal:
                        parallaxs[i].AnimationSpeed = speed * 1 / 4;
                        break;

                    default:
                        Debug.Log($"Err::Can't find any parallax type::{this.name}");
                        break;
                }
            }

            PipesManager.i.Speed = speed * 2f;
        }

        private void OnScoreChange(int score)
        {
            for(int i = 0; i < levelProgressions.Length; i++)
            {
                if (score >= levelProgressions[i].minScore && score < levelProgressions[i].maxScore)
                {
                    if (Speed < levelProgressions[i].speed)
                    {
                        Speed = levelProgressions[i].speed;
                        break;
                    }
                }
            }
        }
    }
}
