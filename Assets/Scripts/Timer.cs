using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timerText;
    static float timer;
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);

            string time = string.Format("{0:0}:{1:00}", minutes, seconds);

            timerText.text = time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        started = true;
    }
}
