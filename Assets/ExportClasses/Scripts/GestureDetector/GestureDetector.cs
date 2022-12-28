using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public enum GestureType
{
    None,
    Tap,
    Pressed,
    DoubleTap,
    SwipeUp,
    SwipeDown,
    SwipeLeft,
    SwipeRight,
    PinchIn,
    PinchOut
}

public class Gesture
{
    public Gesture()
    {
        SetStandardValues();
    }

    public void SetStandardValues()
    {
        startPos = new Vector2(0, 0);
        endPos = new Vector2(0, 0);
        delta = new Vector2(0, 0);
        distance = 0;
        currentGesture = GestureType.None;
        touchLists.Clear();
    }

    [SerializeField] private float tapTime = 0.5f;
    private List<Touch> touchLists = new List<Touch>();
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 delta;
    private float distance;
    private GestureType currentGesture;

    #region Setters

    public void SetStartPos(Vector2 newStartPos)
    {
        startPos = newStartPos;
    }

    public void SetEndPos(Vector2 newEndPos)
    {
        endPos = newEndPos;
    }

    public void SetDelta(Vector2 newDelta)
    {
        delta = newDelta;
    }

    public void SetDistance(float newDistance)
    {
        distance = newDistance;
    }

    public void SetCurrentGesture(GestureType newCurrentGesture)
    {
        currentGesture = newCurrentGesture;
    }

    #endregion
    
    #region Getters

    public float GetTapTime()
    {
        return tapTime;
    }
    
    public Vector2 GetStartPos()
    {
        return startPos;
    }
    
    public Vector2 GetEndPos()
    {
        return endPos;
    }
    
    public Vector2 GetDelta()
    {
        return delta;
    }
    
    public float GetDistance()
    {
        return distance;
    }
    
    public GestureType GetCurrentGesture()
    {
        return currentGesture;
    }
    
    public List<Touch> GetTouchList()
    {
        return touchLists;
    }
    
    public Touch GetTouchListElement(int index)
    {
        return touchLists[index];
    }

    #endregion
}

public class GestureDetector : Singleton<GestureDetector>
{
    #region Variables & Properties

    public Gesture gesture= new Gesture();
    private float minDistance;
    private bool tapCourutine;
    public string curgest;
    
    #endregion
    
    #region MonoBehaviour

    protected override void Awake()
    {
        base.Awake();
        minDistance = Screen.height * 15 / 100;
    }

    private void Update()
    {
        VerifyInput();
        curgest = gesture.GetCurrentGesture().ToString();
    }
    
    #endregion

    #region Methods

    private void AssignTouchList()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            gesture.GetTouchList().Add(Input.GetTouch(i));
        }
    }
    
    private void VerifyInput()
    {

        AssignTouchList();
        
        switch (Input.touchCount)
        {
            case 0:

                ZeroTouch();
                break;

            case 1:
                OneTouch();
                break;
            case 2:

                TwoTouch();
                break;
        }
    }

    private void ZeroTouch()
    {
        gesture.SetStandardValues();
    }
    
    private void OneTouch()
    {
        switch (gesture.GetTouchListElement(0).phase)
        {
            case TouchPhase.Began:

                gesture.SetStartPos(gesture.GetTouchListElement(0).position);
                gesture.SetEndPos(gesture.GetTouchListElement(0).position);
                StartCoroutine(VerifyTimeTap());
                        
                break;
            case TouchPhase.Moved:
                
                    gesture.SetEndPos(gesture.GetTouchListElement(0).position);

                break;
            case TouchPhase.Ended:
                
                if (tapCourutine)
                {
                    gesture.SetCurrentGesture(GestureType.Tap);
                    StopCoroutine(VerifyTimeTap());
                }
                else
                {
                    gesture.SetCurrentGesture(GestureType.Pressed);
                }
                
                
                if (Vector3.Dot(gesture.GetTouchListElement(0).position,gesture.GetTouchListElement(0).position) > 0.85f)
                {
                    gesture.SetCurrentGesture(GestureType.Tap);
                }
                
                gesture.SetEndPos(gesture.GetTouchListElement(0).position);
                SetSwipe();
                
                break;
        }
    }

    private void TwoTouch()
    {
        
    }

    private void SetSwipe()
    {
        if (Mathf.Abs(gesture.GetEndPos().x - gesture.GetStartPos().x)>minDistance || Mathf.Abs(gesture.GetEndPos().y-gesture.GetStartPos().y)> minDistance)
        {
            if (Mathf.Abs(gesture.GetEndPos().x-gesture.GetStartPos().x) > Mathf.Abs(gesture.GetEndPos().y-gesture.GetStartPos().y))
            {
                if (gesture.GetEndPos().x > gesture.GetStartPos().x)
                {
                    gesture.SetCurrentGesture(GestureType.SwipeRight);
                }
                else
                {
                    gesture.SetCurrentGesture(GestureType.SwipeLeft);
                }
            }
            else
            {
                if (gesture.GetEndPos().y > gesture.GetStartPos().y)
                {
                    gesture.SetCurrentGesture(GestureType.SwipeUp);
                }
                else
                {
                    gesture.SetCurrentGesture(GestureType.SwipeDown);
                }
            }
        }
    }
    
    #region Courutines

    private IEnumerator VerifyTimeTap()
    {
        tapCourutine = true;
        
        Touch tapped = gesture.GetTouchListElement(0);
        yield return new WaitForSeconds(gesture.GetTapTime());

        tapCourutine = false;
    }

    #endregion
   

    #endregion
}

