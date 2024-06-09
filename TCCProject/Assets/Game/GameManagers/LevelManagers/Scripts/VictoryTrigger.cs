using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [SerializeField]LevelTimer time;
    [SerializeField ]LevelManager levelManager;
   public void WinEvent()
   {
        if(time.currentTime >=  1 )
        {
            levelManager.isPaused = true;
            levelManager.winner = true;
        }
   }
}
