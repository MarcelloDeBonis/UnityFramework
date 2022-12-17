using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InputManager :  MonoBehaviour
{ 
    public static InputManager Instance { get; private set; }

    //Swipe variable
    private Vector3 firstPos;
    private Vector3 lastPos;
    private float minDindistance;

    public Camera mainCamera;
    private GameObject player;
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        minDindistance = Screen.height * 15 / 100;
    }

    // Update is called once per frame
    void Update()
    {
        SwipeAndTouch();
        CameraZoom();
    }

    private void SwipeAndTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                firstPos = touch.position;
                lastPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lastPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lastPos = touch.position;
                if (Mathf.Abs(lastPos.x - firstPos.x)>minDindistance || Mathf.Abs(lastPos.y-firstPos.y)> minDindistance)
                {
                    if (Mathf.Abs(lastPos.x-firstPos.x) > Mathf.Abs(lastPos.y-firstPos.y))
                    {
                        if (lastPos.x > firstPos.x)
                        {
                            //right
                            player.transform.position += Vector3.right ;
                        }
                        else
                        {
                            //left
                            player.transform.position += Vector3.left;
                        }
                    }
                    else
                    {
                        if (lastPos.y > firstPos.y)
                        {
                            //right
                            player.transform.position += Vector3.up ;
                        }
                        else
                        {
                            //left
                            player.transform.position += Vector3.down;
                        }
                    }
                }
                else
                {
                    //single tap
                    player.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
                }
            }
        }
    }

    private void CameraZoom()
    { 
        float touchersPrevPosDif;
        float touchesCurPosDIf;
        float zoomMidifier;
        float zoomModifierSpeed = 0.1f;
        
        Vector2 firstTouchPrevPos;
        Vector2 secondTouchPrevPos;

        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchersPrevPosDif = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDIf = (firstTouch.position - secondTouch.position).magnitude;

            zoomMidifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchersPrevPosDif > touchesCurPosDIf)
            {
                mainCamera.orthographicSize += zoomMidifier;
            }

            if (touchersPrevPosDif < touchesCurPosDIf)
            {
                mainCamera.orthographicSize -= zoomMidifier;
            }
        }

        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 0.5f, 10f);

    }

}
