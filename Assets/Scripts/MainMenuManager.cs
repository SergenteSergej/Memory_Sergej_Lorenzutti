using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGameEasy()
    {
        PlayerPrefs.SetInt("rows", 3);
        PlayerPrefs.SetInt("columns", 4);
        SceneManager.LoadScene("SampleScene"); // o il nome della tua scena di gioco
    }

    public void StartGameMedium()
    {
        PlayerPrefs.SetInt("rows", 4);
        PlayerPrefs.SetInt("columns", 5);
        SceneManager.LoadScene("SampleScene");
    }

    public void StartGameHard()
    {
        PlayerPrefs.SetInt("rows", 6);
        PlayerPrefs.SetInt("columns", 6);
        SceneManager.LoadScene("SampleScene");
    }
}