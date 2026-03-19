using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenue : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); 
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
