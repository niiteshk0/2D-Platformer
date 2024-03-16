using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float smoothTime;

    [SerializeField] Transform target;
    Vector3 velocity = Vector3.zero;

    [Header("Axis Limitaion")]
    [SerializeField] Vector3 offset;
    [SerializeField] Vector2 xLimit;
    [SerializeField] Vector2 yLimit;
        
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), -10);  // for the bounding

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
