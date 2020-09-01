using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    [SerializeField,Range(0,100)]
    float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;
    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);
    [SerializeField, Range(0f, 1)]
    float bounceNess = 0.5f;
    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        //playerInput.Normalize();
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        //Vector3 acceleration = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        //velocity += acceleration * Time.deltaTime;
        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        //if (velocity.x < desiredVelocity.x)
        //{
        //    velocity.x = Mathf.Min(velocity.x + maxSpeedChange, desiredVelocity.x);
        //}else if (velocity.x < desiredVelocity.x)
        //{
        //    velocity.x = Mathf.Min(velocity.x - maxSpeedChange, desiredVelocity.x);
        //}
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = displacement + transform.localPosition;
        if (!allowedArea.Contains(new Vector2(newPosition.x,newPosition.z)))
        {
            //newPosition = transform.localPosition;
            //newPosition.x = Mathf.Clamp(newPosition.x, allowedArea.xMin, allowedArea.xMax);
            //newPosition.z = Mathf.Clamp(newPosition.z, allowedArea.yMin, allowedArea.yMax);
            if(newPosition.x < allowedArea.xMin)
            {
                newPosition.x = allowedArea.xMin;
                velocity.x = -velocity.x*bounceNess;
            }else if(newPosition.x > allowedArea.xMax)
            {
                newPosition.x = allowedArea.xMax;
                velocity.x = -velocity.x*bounceNess;
            }
            if (newPosition.z < allowedArea.yMin)
            {
                newPosition.z = allowedArea.yMin;
                velocity.z = -velocity.z*bounceNess;
            }
            else if (newPosition.z > allowedArea.yMax)
            {
                newPosition.z = allowedArea.yMax;
                velocity.z = -velocity.z*bounceNess;
            }
        }
        transform.localPosition = newPosition;

    }
}
