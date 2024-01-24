using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinBird.Game
{
    public class InputManager : AppSingleton<InputManager>
    {
        public bool ScreenTouched { private set; get; }
        public bool KeyboardPressed { private set; get; }
        public bool MousePressed { private set; get; }

        protected override void Awake()
        {
            base.Awake();

            Application.targetFrameRate = Config.fpsTarget;
        }

        void Update()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        ScreenTouched = true;
                    }
                    else
                    {
                        ScreenTouched = false;
                    }
                }
                else
                {
                    ScreenTouched = false;
                }
            }
            
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    KeyboardPressed = true;
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    KeyboardPressed = false;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    MousePressed = true;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    MousePressed = false;
                }
            }
        }
    }
}
