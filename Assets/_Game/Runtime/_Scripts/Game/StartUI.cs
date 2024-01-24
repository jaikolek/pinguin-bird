using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using TMPro;
using DG.Tweening;

namespace PinguinBird.Game
{
    public class StartUI : MonoBehaviour
    {
        [SerializeField] private GameObject objPanel;
        public GameObject ObjPanel => objPanel;
        [SerializeField] private Transform trfPanel;
        [SerializeField] private Image imgTap;
        [SerializeField] private Color[] colorsBlink;

        private bool blinked = false;

        private Tween scaleTween;
        private Tween tapTween;

        private void Awake()
        {
            //  trfPanel.gameObject.SetActive(false);
        }

        private void Update()
        {
            //if (!blinked)
            //{
            //    blinked = true;
            //    BlinkTapImage();
            //}
        }

        public void Show()
        {
            ObjPanel.SetActive(true);

            scaleTween.Kill();
            scaleTween = trfPanel.DOScale(0f, 0f).SetLink(trfPanel.gameObject).OnComplete(() => {
                trfPanel.gameObject.SetActive(true);
                trfPanel.DOScale(1.2f, .2f).SetLink(trfPanel.gameObject).OnComplete(() => {
                    trfPanel.DOScale(1f, .1f).SetLink(trfPanel.gameObject);
                });
            });
        }

        public void Hide()
        {
            scaleTween.Kill();
            scaleTween = trfPanel.DOScale(1.2f, .1f).SetLink(trfPanel.gameObject).OnComplete(() => {
                trfPanel.gameObject.SetActive(true);
                trfPanel.DOScale(0f, .2f).SetLink(trfPanel.gameObject).OnComplete(() => {
                    trfPanel.gameObject.SetActive(false);
                    ObjPanel.SetActive(false);
                });
            });
        }

        private void BlinkTapImage()
        {
            tapTween.Kill();
            tapTween = imgTap.DOColor(colorsBlink[1], .4f).SetLink(imgTap.gameObject).OnComplete(() => {
                imgTap.DOColor(colorsBlink[0], .4f).SetLink(imgTap.gameObject).OnComplete(() => {
                    blinked = false;
                });
            });
        }
    }
}
