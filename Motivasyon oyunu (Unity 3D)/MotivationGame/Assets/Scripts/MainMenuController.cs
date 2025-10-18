using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI welcomeText;

    

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

    public void WelcomeTextChanger(string patientName) { 
    
        welcomeText.text = patientName+" hoþ geldiniz.";

    }

}
