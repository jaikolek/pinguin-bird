using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace PinguinBird.Game
{
    public class MainCamController : SceneSingleton<MainCamController>
    {
        [SerializeField] private Camera mainCam;

        private Vector3 initCamLocalPos;

        private Vector3 prevPosition;
        public float prevOrthoSize { get; private set; }

        public float initOrthoSize { get; private set; }

        private Tween orthoSizeTween;
        private Tween shakeTween;

        public UnityEvent onChangePosition { get; private set; }
        public UnityEvent onChangeOrthoSize { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            initCamLocalPos = mainCam.transform.localPosition;
            initOrthoSize = mainCam.orthographicSize;

            prevPosition = transform.position;
            prevOrthoSize = initOrthoSize;

            onChangePosition = new UnityEvent();
            onChangeOrthoSize = new UnityEvent();
        }

        private void Update()
        {
            if (transform.position != prevPosition)
            {
                onChangePosition.Invoke();
                prevPosition = transform.position;
            }
            if (prevOrthoSize != mainCam.orthographicSize)
            {
                onChangeOrthoSize.Invoke();
                prevOrthoSize = mainCam.orthographicSize;
            }
        }

        public float orthoSize => mainCam.orthographicSize;

        public Tween DoOrthoSizeAnim(float endValue, float duration)
        {
            orthoSizeTween.Kill();
            orthoSizeTween = mainCam.DOOrthoSize(endValue, duration).SetLink(gameObject);
            return orthoSizeTween;
        }

        public void DoShakeAnim(float duration = .16f, float strength = .12f, int vibrato = 25)
        {
            shakeTween.Kill();
            shakeTween = mainCam.transform.DOShakePosition(duration, strength, vibrato).SetLink(gameObject)
                .OnComplete(() => shakeTween = mainCam.transform.DOLocalMove(initCamLocalPos, .1f)
                    .SetLink(gameObject));
        }

        public Vector2 ScreenToWorldPos(Vector2 screenPosition)
        {
            return mainCam.ScreenToWorldPoint(screenPosition);
        }

        public Vector2 WorldToScreenPos(Vector3 worldPosition)
        {
            return mainCam.WorldToScreenPoint(worldPosition);
        }
    }
}