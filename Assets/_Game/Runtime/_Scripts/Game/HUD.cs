using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PinguinBird.Game
{
    public class HUD : SceneSingleton<HUD>
    {
        [SerializeField] private StartUI startUI;
        public StartUI StartUI => startUI;

        [SerializeField] private GameplayUI gameplayUI;
        public GameplayUI GameplayUI => gameplayUI;

        [SerializeField] private GameOverUI gameOverUI;
        public GameOverUI GameOverUI => gameOverUI;
    }
}
