using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMovement : MonoBehaviour
{
    private bool isRolling;

    [SerializeField] private float height = 384.0f; // 256 o 384
    private float faceHeight = 64.0f;
    private int result;

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

    // for testing
    public DiceTesting values;
    

    void Start()
    {
        startPosition = transform.position;

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

    void Update()
    {
        if (isRolling)
        {
            RollAnimation();
        }
    }
    public void StartAnimation(int faceIndex)
    {
        speed = initialSpeed;
        result = faceIndex;
        targetPosition = new Vector3(startPosition.x, startPosition.y + (faceHeight * result), startPosition.z);
        isRolling = true;
        InvokeRepeating("DecreaseSpeed", firstDelay, repeatRate);
    }

    void RollAnimation()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        timePassed += Time.deltaTime;

        if (transform.position.y > startPosition.y + height)
        {
            transform.position = startPosition;
        }

        if (timePassed > rollDuration && Vector3.Distance(transform.position, targetPosition) < maxDistance)
        {
            transform.position = targetPosition;
            EndRollAnimation();
        }
    }

    void DecreaseSpeed()
    {
        speed *= speedMultiplier;
        if (speed < minSpeed)
        {
            CancelInvoke();
        }
    }

    void EndRollAnimation()
    {
        isRolling = false;
        CancelInvoke();
        //speed = initialSpeed;
        timePassed = 0;
        diceManager.EndOneRoll(result);
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }

}