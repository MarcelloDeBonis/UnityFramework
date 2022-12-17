using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class GestureDetector : Singleton<GestureDetector>
{
    #region Variables & Properties
    
    public enum GestureType
    {
        None,
        Tap,
        //DoubleTap
        SwipeUp,
        SwipeDown,
        SwipeLeft,
        SwipeRight,
        PinchIn,
        PinchOut
    }

    public class Gesture
    {
        private Vector2 startPos;
        private Vector2 endPos;
        private Vector2 delta;
        private float distance;
        public GestureType currentGesture;
    }

    #endregion

    private Gesture gesture;
    
    #region MonoBehaviour
    private void Update()
    {
        VerifyInput();
    }
    
    #endregion

    #region Methods

    private void VerifyInput()
    {
        gesture = new Gesture();
        
        if (Touchscreen.current.touches.Count > 0)
        {
            Touch touch = Touchscreen.current.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                endPos = touch.position;
                delta = endPos - startPos;
                distance = delta.magnitude;

                if (distance > 50f)
                {
                    // Swipe
                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    {
                        // Horizontal swipe
                        if (delta.x > 0)
                        {
                            currentGesture = GestureType.SwipeRight;
                        }
                        else
                        {
                            currentGesture = GestureType.SwipeLeft;
                        }
                    }
                    else
                    {
                        // Vertical swipe
                        if (delta.y > 0)
                        {
                            currentGesture = GestureType.SwipeUp;
                        }
                        else
                        {
                            currentGesture = GestureType.SwipeDown;
                        }
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (distance < 50f)
                {
                    // Tap
                    currentGesture = GestureType.Tap;
                }
            }
        }
        else
        {
            // No gesture
            currentGesture = GestureType.None;
        }
    }

    #endregion
}

