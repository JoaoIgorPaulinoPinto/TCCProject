using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public  static UIManager UIManagerInstance;

    private void Awake()
    {
        if (UIManagerInstance == null)
        {

            UIManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //TODO:: FAZER AS UIs

    public void RestartLevel(string msg)
    {
        //reiniciar o level
        print(msg);
    }

    public void LostLevel(string msg)
    {
        print(msg);
        //reiniciar o level
    }
    public void WinnedLevel( string msg)
    {
        print (msg);
        //reiniciar o level
    }

    public void BackToMenu(string msg)
    {
        // volta para o menu 
        print(msg);
    }

    public void StartLevel(string msg)
    {
        //comeca a contagem de tempo e o jogador pode se mover
        print(msg);
    }
    public void Pause(string msg)
    { 

         print(msg);
    }


}
