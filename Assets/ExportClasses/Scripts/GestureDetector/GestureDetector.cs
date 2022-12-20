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
        public Vector2 startPos;
        public Vector2 endPos;
        public Vector2 delta;
        public float distance;
        public GestureType currentGesture;
    }
    
    #endregion

    private Gesture gesture;
    public string currentGesture;
    
    
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
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                gesture.startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                gesture.endPos = touch.position;
                gesture.delta = gesture.endPos - gesture.startPos;
                gesture.distance = gesture.delta.magnitude;

                if (gesture.distance > 50f)
                {
                    // Swipe
                    if (Mathf.Abs(gesture.delta.x) > Mathf.Abs(gesture.delta.y))
                    {
                        // Horizontal swipe
                        if (gesture.delta.x > 0)
                        {
                            gesture.currentGesture = GestureType.SwipeRight;
                        }
                        else
                        {
                            gesture.currentGesture = GestureType.SwipeLeft;
                        }
                    }
                    else
                    {
                        // Vertical swipe
                        if (gesture.delta.y > 0)
                        {
                            gesture.currentGesture = GestureType.SwipeUp;
                        }
                        else
                        {
                            gesture.currentGesture = GestureType.SwipeDown;
                        }
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (gesture.distance < 50f)
                {
                    // Tap
                    gesture.currentGesture = GestureType.Tap;
                }
            }
        }
        else
        {
            // No gesture
            gesture.currentGesture = GestureType.None;
        }

        currentGesture = gesture.currentGesture.ToString();
    }

    #endregion
}

