using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    //Components
    TopDownController topDownCarController;

    //Awake is called when the script istance is being loaded
    void Awake()
    {
        topDownCarController = GetComponent<TopDownController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        topDownCarController.SetInputVector(inputVector);
    }
}
