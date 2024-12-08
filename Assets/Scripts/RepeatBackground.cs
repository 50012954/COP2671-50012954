using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    void Start()
    {
        startPos = transform.position; // Store initial position
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; // Calculate half of background width
    }

    void Update()
    {
        // Reset background position when it moves beyond repeat width
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}