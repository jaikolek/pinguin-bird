using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PinguinBird
{
    public class FrameRateDebugging : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txtFPS;
        private float fps;

        private void Awake()
        {
            InvokeRepeating(nameof(GetFPS), 1, 1);
        }

        private void GetFPS()
        {
            fps = (int)(1f / Time.unscaledDeltaTime);
            txtFPS.SetText($"FPS: {fps}");
        }

    }
}
