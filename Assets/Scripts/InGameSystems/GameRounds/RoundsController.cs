using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundsController : MonoBehaviour
{
    private void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void NextRound()
    {
        if(PlayerPrefs.HasKey("Round") == true)
        {
            int nextRound = PlayerPrefs.GetInt("Round");
            nextRound++;
            PlayerPrefs.SetInt("Round", nextRound);
        }
        else
        {
            //Убрать коментирование, если нужно будет продвижение по раундам.
            //PlayerPrefs.SetInt("Round", 1);
        }

        RestartScene();
    }

    public void AgainThisRound()
    {
        RestartScene();
    }
}