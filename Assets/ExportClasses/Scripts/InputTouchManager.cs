using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouchManager : Singleton <InputTouchManager>
{
    #region Variables & Properties

    public enum EnumTouchAction
    {
        NoTouching,
        Tap,
        DoubleTap,
        Drag,
        Slide,
        Pinch,
        Spread,
        Pressing,
    }

    public EnumTouchAction touchAction = EnumTouchAction.NoTouching;
    [SerializeField] private float TapDuration;
    Touch firstTouch;

    private bool timerTouchingFirstTime = false;
    private bool timerTouchingSecondTime = false;
    private bool IsPressing = false;
    private bool isItCounting = false;
    
    #endregion

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        touchAction = EnumTouchAction.NoTouching;
    }

    // Update is called once per frame
    void Update()
    {
        VerifyInput();
    }

    #endregion

    #region Methods
    
    private void VerifyInput()
    {
        switch (Input.touchCount)
        {
            case 0:
                
                touchAction = EnumTouchAction.NoTouching;
                IsPressing = false;
                break;
            
            case 1:
                
                firstTouch = Input.GetTouch(0);
                switch (firstTouch.phase)
                {
                    case TouchPhase.Began :

                        switch (touchAction)
                        {
                            case EnumTouchAction.NoTouching:
                                if (!timerTouchingFirstTime && !timerTouchingSecondTime)
                                {
                                    StartCoroutine(VerifyTimeTap());
                                    touchAction = EnumTouchAction.Tap;
                                }
                                else
                                {
                                    if (timerTouchingFirstTime)
                                    {
                                    
                                        StartCoroutine((VerifyTimeDoubleTap()));
                                        touchAction = EnumTouchAction.DoubleTap;

                                    }
                                    else
                                    {
                                        if (timerTouchingSecondTime)
                                        {
                                            touchAction = EnumTouchAction.DoubleTap;
                                        }
                                    }
                                }

                                break;
                            
                            case EnumTouchAction.Tap :
                                touchAction=EnumTouchAction.NoTouching;
                                break;
                            
                        }
                        
                        break;
                    case TouchPhase.Moved or TouchPhase.Stationary:
                        if (!IsPressing)
                        {
                            if (!isItCounting)
                            {
                                switch (touchAction)
                                {
                                    case EnumTouchAction.Tap or EnumTouchAction.DoubleTap:
                                        StartCoroutine((VerifyTimePressing()));
                                        break;
                                }
                            }
                        }
                        else
                        {
                            touchAction = EnumTouchAction.Pressing;
                        }
                        
                            
                        
                            //
                        
                        
                       
                        break;
                }
                break;
            
            case 2:
                break;
        }
    }
    
    private IEnumerator VerifyTimeTap()
    {
        float normalizedTime1 = 0;
        timerTouchingFirstTime = true;
        while(normalizedTime1 <= 1f)
        {
            normalizedTime1 += Time.deltaTime / TapDuration;
            yield return null;
        }

       timerTouchingFirstTime = false;
        
    }
    
    private IEnumerator VerifyTimeDoubleTap()
    {
        timerTouchingFirstTime = false;
        timerTouchingSecondTime = true;
        float normalizedTime2 = 0;
        while(normalizedTime2 <= 1f)
        {
            normalizedTime2 += Time.deltaTime / TapDuration;
            yield return null;
        }
        
        timerTouchingSecondTime = false;
    }

    private IEnumerator VerifyTimePressing()
    {
        isItCounting = true;
        float normalizedTime3 = 0;
        IsPressing = false;
        while(normalizedTime3 <= 1f)
        {
            normalizedTime3 += Time.deltaTime / TapDuration;
            yield return null;
        }

        if (touchAction == EnumTouchAction.Tap || touchAction == EnumTouchAction.DoubleTap)
        {
            IsPressing = true;
        }

        isItCounting = false;
    }
    
    #endregion
}
