using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using TMPro;
using DG.Tweening;

namespace PinguinBird.Game
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private GameObject objPanel;
        public GameObject ObjPanel => objPanel;
        [SerializeField] private TextMeshProUGUI txtScore;

        private Tween scaleTween;

        public void OnScoreChanged(int score)
        {
            scaleTween.Kill();
            scaleTween = txtScore.transform.DOScale(1.3f, .1f).SetLink(txtScore.gameObject).OnComplete(() => {
                txtScore.SetText($"{score}");
                txtScore.transform.DOScale(1f, .1f).SetLink(txtScore.gameObject);
            });
        }
    }
}
