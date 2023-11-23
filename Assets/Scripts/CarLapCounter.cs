using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{
    int passedCheckPointNumber = 0;
    float passedTime = 0;

    int numberOfPassedCheckpoints = 0;

    // Events
    public event Action<CarLapCounter> OnPassCheckpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            Checkpoint checkpoint = collision.GetComponent<Checkpoint>();

            // Make sure that the car is passing the checkpoints in the correct order. The correct checkpoints must have exactly 1 higher value than the passed checkpoint
            if (passedCheckPointNumber + 1 == checkpoint.checkPointNumber)
            {
                passedCheckPointNumber = checkpoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                // Store the time at the checkpoint
                passedTime = Time.time;

                // Invoke the passed checkpoint event
                OnPassCheckpoint?.Invoke(this);
            }
        }
    }
}
