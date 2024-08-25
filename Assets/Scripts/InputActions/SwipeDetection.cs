using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public enum DIRECTION { LEFT,RIGHT,UP,DOWN};
    [SerializeField]
    private float minimumDist = .2f;
    [SerializeField]
    private float maximumTime = 1;
    [SerializeField, Range(0f,1f)]
    private float directionThreshold =0.9f;
    [SerializeField]
    GameObject trail;
    [SerializeField]
    private float zOffset;
    InputManager inputManager;


    private Vector2 startPos;
    private float startTime;
    private Vector2 endPos;
    private float endTime;
    private Coroutine trailCoroutine;
    private void Awake()
    {
        inputManager = InputManager.instance;
        trail.GetComponent<TrailRenderer>().widthMultiplier = 1/(1+zOffset);
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
        trailCoroutine = StartCoroutine(TrailUpdate());
        OnSwipe += debugDirection;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
        StopCoroutine(trailCoroutine);
        OnSwipe -= debugDirection;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPos = position;
        startTime = time;
        trail.transform.position = position;
        trail.SetActive(true);
        
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPos = position;
        endTime = time;
        trail.SetActive(false);
        DetectSwipe();
    }

    private IEnumerator TrailUpdate()
    {
        while (true)
        {
            if (zOffset != 0)
            {
                trail.transform.localPosition = new Vector3(inputManager.TouchPosition().x, inputManager.TouchPosition().y, zOffset);
            }
            else
            {
                trail.transform.position = new Vector3(inputManager.TouchPosition().x, inputManager.TouchPosition().y, zOffset);
            }
            
            yield return null;
        }
    } //update trail position once a frame
    private void DetectSwipe()
    {
        if (Vector3.Distance(startPos, endPos) < minimumDist) return;
        if ((endTime - startTime) <= maximumTime)
        {
            Debug.DrawLine(startPos, endPos, Color.red, 5f); //draw debug line
            Vector3 direction = endPos - startPos;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            OnSwipe(startPos, DIRECTION.LEFT);
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            OnSwipe(startPos, DIRECTION.RIGHT);
        }
        else if (Vector2.Dot(Vector2.up,direction) > directionThreshold)
        {
            OnSwipe(startPos, DIRECTION.UP);
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            OnSwipe(startPos, DIRECTION.DOWN);
        }
    }
    private void debugDirection(Vector2 pos, DIRECTION direction)
    {
        Debug.Log(direction);
    }

    public delegate void SwipeEvent(Vector2 startPosition, DIRECTION direction);
    public event SwipeEvent OnSwipe;

}
