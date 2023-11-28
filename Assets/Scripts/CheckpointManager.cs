using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckpointManager : MonoBehaviour
{
    public List<CarLapCounter> CarLapCounters = new List<CarLapCounter>();

    // Start is called before the first frame update
    void Start()
    {
        // Get Car lap count in the scene
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        // Store the lap count in a list
        CarLapCounters = carLapCounterArray.ToList<CarLapCounter>();

        // Hook up the passed checkpoint event
        foreach (CarLapCounter LapCounter in CarLapCounters)
            LapCounter.OnPassCheckpoint += OnPassCheckpoint;
    }

    void OnPassCheckpoint(CarLapCounter carLapCounter)
    {
        Debug.Log($"Event: {carLapCounter.gameObject.name} passed a checkpoint");
    }
}
