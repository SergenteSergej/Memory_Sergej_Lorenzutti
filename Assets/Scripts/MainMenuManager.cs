using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGameEasy()
    {
        GameSettings.rows = 2;
        GameSettings.columns = 3;
        SceneManager.LoadScene("GameScene");
    }

    public void StartGameMedium()
    {
        GameSettings.rows = 4;
        GameSettings.columns = 4;
        SceneManager.LoadScene("GameScene");
    }

    public void StartGameHard()
    {
        GameSettings.rows = 5;
        GameSettings.columns = 6;
        SceneManager.LoadScene("GameScene");
    }
    
}
public static class GameSettings
{
    public static int rows;
    public static int columns;
}