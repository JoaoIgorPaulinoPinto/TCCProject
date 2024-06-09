
using System.Globalization;
using UnityEngine;

using UnityEngine.SceneManagement;
public class GameManagerInstance : MonoBehaviour
{
    private static GameManagerInstance instance;

    public string CurrentLevelName;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

  
    public void StartGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
  
    public static GameManagerInstance Instance
    {
        get { return instance; }
    }

}
