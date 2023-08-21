using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieMovement : MonoBehaviour
{
    private bool isRolling;

    [SerializeField] private float height = 384.0f; // 256 o 384
    private float faceHeight = 64.0f;
    private int result;

    private Vector3 initialPosition;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float maxDistance = 4.0f; //

    // speed
    private float initialSpeed = 20.0f; //
    private float minSpeed; //
    private float speed;
    private float firstDelay = 1.0f; //
    private float repeatRate = 0.5f; //
    private float speedMultiplier = 0.3f; //

    private float timePassed;
    private float rollDuration; //

    [SerializeField] private DiceManager diceManager;

    public DiceTesting values; // for testing

    private RectTransform rt;
    private float scale;
    

    void Start()
    {
        rt = GetComponentInChildren<RectTransform>();

        // for testing
        initialSpeed = values.initialSpeed;
        minSpeed = values.minSpeed;
        firstDelay = values.firstDelay;
        repeatRate = values.repeatRate;
        speedMultiplier = values.speedMultiplier;
        maxDistance = values.maxDistance;
        rollDuration = values.rollDuration;
        //
    }

    void FixedUpdate()
    {
        if (isRolling)
        {
            RollAnimation();
        }
    }
    public void StartAnimation(int faceIndex)
    {
        startPosition = transform.position;
        scale = rt.lossyScale.x;

        speed = initialSpeed * scale;
        result = faceIndex;
        targetPosition = new Vector3(startPosition.x, startPosition.y + (faceHeight * scale * result), startPosition.z);
        isRolling = true;
        InvokeRepeating("DecreaseSpeed", firstDelay, repeatRate);
    }

    void RollAnimation()
    {
        transform.Translate(Vector3.up * Time.fixedDeltaTime * speed);
        timePassed += Time.fixedDeltaTime;

        if (transform.position.y > startPosition.y + height * scale)
        {
            transform.position = startPosition;
        }

        if (timePassed > rollDuration && Vector3.Distance(transform.position, targetPosition) < maxDistance * scale)
        {
            transform.position = targetPosition;
            EndRollAnimation();
        }
    }

    void DecreaseSpeed()
    {
        speed *= speedMultiplier;
        if (speed < minSpeed * scale)
        {
            CancelInvoke();
        }
    }

    void EndRollAnimation()
    {
        isRolling = false;
        CancelInvoke();
        timePassed = 0;

        IEnumerator coroutine = diceManager.EndOneRoll(result);
        StartCoroutine(coroutine);
    }

    public void ResetPosition()
    {
        if (scale == rt.lossyScale.x)
        {
            transform.position = startPosition;
            return;
        }
        transform.position = startPosition * (rt.lossyScale.x / scale);
    }

}