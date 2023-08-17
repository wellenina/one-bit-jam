using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMovement : MonoBehaviour
{
    private bool isRolling;

    [SerializeField] private float height = 384.0f; // 256 o 384
    private float faceHeight = 64.0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float maxDistance = 4.0f; //

    // speed
    private float initialSpeed = 20.0f; //
    private float speed;
    private float firstDelay = 1.0f; //
    private float repeatRate = 0.5f; //
    private float speedMultiplier = 0.3f; //

    // for testing
    public DiceTesting values;
    

    void Start()
    {
        startPosition = transform.position;

        // for testing
        initialSpeed = values.initialSpeed;
        firstDelay = values.firstDelay;
        repeatRate = values.repeatRate;
        speedMultiplier = values.speedMultiplier;
        maxDistance = values.maxDistance;
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
        transform.position = startPosition;
	    speed = initialSpeed;

        targetPosition = new Vector3(startPosition.x, startPosition.y + (faceHeight * faceIndex), startPosition.z);
        isRolling = true;
        InvokeRepeating("DecreaseSpeed", firstDelay, repeatRate);
    }

    void RollAnimation()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (transform.position.y > startPosition.y + height)
            {
                transform.position = startPosition;
            }
    }

    void DecreaseSpeed()
    {
        speed *= speedMultiplier;
    }

}