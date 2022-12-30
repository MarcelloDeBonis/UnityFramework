using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public enum GestureType
{
    None,
    Tap,
    Pressing,
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
        currentGesture = GestureType.None;
        touchLists.Clear();
    }

    private List<Touch> touchLists = new List<Touch>();
    private Vector2 startPos;
    private Vector2 endPos;
    private GestureType currentGesture;
    private float timePressing;

    #region Setters

    public void SetTimePressing(float newTimePressing)
    {
        timePressing = newTimePressing;
    }
    
    public void SetStartPos(Vector2 newStartPos)
    {
        startPos = newStartPos;
    }

    public void SetEndPos(Vector2 newEndPos)
    {
        endPos = newEndPos;
    }

    public void SetCurrentGesture(GestureType newCurrentGesture)
    {
        currentGesture = newCurrentGesture;
    }

    #endregion
    
    #region Getters

    public float GetTimePressing()
    {
        return timePressing;
    }
    
    public Vector2 GetStartPos()
    {
        return startPos;
    }
    
    public Vector2 GetEndPos()
    {
        return endPos;
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
    private bool tapCourutine = false;
    private bool lastGestureTap = false;
    private int touchCount;

    [SerializeField] private float tapTime;
    [SerializeField] private float doubleTapTime;
    
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
        Touch touch = Input.GetTouch(0);
        
        switch (touch.phase)
        {
            case TouchPhase.Began:
                
                gesture.SetStartPos(touch.position);
                gesture.SetEndPos(touch.position);

                CheckTapCounter();

                break;
            case TouchPhase.Moved:

                if (tapCourutine)
                {
                    float timePressing = gesture.GetTimePressing();
                    gesture.SetTimePressing(timePressing+=Time.deltaTime);
                }
                gesture.SetEndPos(touch.position);

                break;
            case TouchPhase.Ended:
                
                gesture.SetEndPos(touch.position);

                switch (tapCourutine)
                {
                    case true:
                        
                        tapCourutine = false;
                        break;
                    
                    case false:

                        gesture.SetTimePressing(0);
                        StartCoroutine(VerifyDoubleTap());
                        gesture.SetCurrentGesture(GestureType.Tap);
                        
                    break;
                }

                SetSwipe();
                
                break;
        }
    }

    private void CheckTapCounter()
    {
        switch (lastGestureTap)
        {
            case true:
                gesture.SetCurrentGesture(GestureType.DoubleTap);
                break;
                
            case false :
                StartCoroutine(VerifyTimeTap());
                break;
        } 
    }
    
    private void TwoTouch()
    {
        float touchersPrevPosDif;
        float touchesCurPosDIf;
        
        Vector2 firstTouchPrevPos;
        Vector2 secondTouchPrevPos;

            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchersPrevPosDif = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDIf = (firstTouch.position - secondTouch.position).magnitude;
            
            if (touchersPrevPosDif > touchesCurPosDIf)
            {
                //Pin in
                gesture.SetCurrentGesture(GestureType.PinchIn);
            }

            if (touchersPrevPosDif < touchesCurPosDIf)
            {
                //Pinc out
                gesture.SetCurrentGesture(GestureType.PinchOut);
            }
        
        
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

        Touch tapped = gesture.GetTouchListElement(0);
        yield return new WaitForSeconds(tapTime);
        if (Vector3.Dot(gesture.GetStartPos(), tapped.position) > 0.85f)
        {
            tapCourutine = true;
            gesture.SetCurrentGesture(GestureType.Pressing);
        }

    }

    private IEnumerator VerifyDoubleTap()
    {
        lastGestureTap = true;
        yield return new WaitForSeconds(doubleTapTime);
        lastGestureTap = false;
    }
    
    #endregion
   

    #endregion
}

