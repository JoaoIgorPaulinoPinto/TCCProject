using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isPlaying; 
    public bool isPaused;
    public string currentLevelName;
    public bool loser; 
    public bool winner;


    //TODO:: FAZER OS CONTROLADORES, DEIXAR O TEMPO PARADO, ANIMACOES ETC

    private UIManager uiManager; // Referência para a instância UIManager

    private void Awake()
    {
        uiManager = UIManager.UIManagerInstance; // Obtém a instância do UIManager
    }
    private void Update()
    {
        if (loser)
        {
            uiManager.LostLevel("PERDEU O JOGO");
        }
        else if (winner) {

            uiManager.WinnedLevel("GANHOU O JOGO");
           
        }
    }
}
