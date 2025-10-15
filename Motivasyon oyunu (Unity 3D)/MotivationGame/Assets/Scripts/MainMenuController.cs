using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadVillageScene()
    {
        SceneManager.LoadScene("VillageScenes");
    }

    public void LoadCityScene()
    {
        SceneManager.LoadScene("CityScenes");
    }

    public void LoadBeachScene()
    {
        SceneManager.LoadScene("BeachScenes");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapatýldý");
    }
}
