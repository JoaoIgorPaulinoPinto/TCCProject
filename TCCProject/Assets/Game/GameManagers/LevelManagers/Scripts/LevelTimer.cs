using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public ScriptableObjectLevels levelStts;
    public float currentTime;
   
    [SerializeField] TextMeshProUGUI timerText;


    bool stop;
    private void Start()
    {
        currentTime = levelStts.duration;
    }

    private void Update()
    {
        if (!stop)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                print("VOCÊ PERDEU");
                timerText.text = "00:00";
            }

            if (currentTime < 5)
            {
                timerText.color = Color.red;
            }
        }
      
    }
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
