using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsScreen; 
    public GameObject MenuScreen;

    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OptionsOpen()
    {
        MenuScreen.SetActive(false);
        OptionsScreen.SetActive(true);

    }
    public void OptionsClose()
    {
        OptionsScreen.SetActive(false);
        MenuScreen.SetActive(true);

    }
}
