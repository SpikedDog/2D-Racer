using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class CarLapCounter : MonoBehaviour
{
    int passedCheckPointNumber = 0;
    float passedTime = 0;

    int numberOfPassedCheckpoints = 0;

    int lapsCompleted = 0;
    const int lapsToComplete = 2;

    bool isRaceComplete = false;

    public Text timerText;
    static float timer;

    public Text lapText;

    // Events
    public event Action<CarLapCounter> OnPassCheckpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            //Once a car has completed the race we dont need to check any checkpoints or laps
            if (isRaceComplete)
                return;
                
            Checkpoint checkpoint = collision.GetComponent<Checkpoint>();

            // Make sure that the car is passing the checkpoints in the correct order. The correct checkpoints must have exactly 1 higher value than the passed checkpoint
            if (passedCheckPointNumber + 1 == checkpoint.checkPointNumber)
            {
                passedCheckPointNumber = checkpoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                // Store the time at the checkpoint
                passedTime = Time.time;

                // Checking if we passed the finish line
                if (checkpoint.isFinishLine)
                {
                    lapText.text = "Lap: " + timerText.text;
                    timer = 0.0f;

                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    if (lapsCompleted >= lapsToComplete)
                        isRaceComplete = true;
                }

                // Invoke the passed checkpoint event
                OnPassCheckpoint?.Invoke(this);
            }
        }
    }
}
