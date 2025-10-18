using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private PatientFetcher loader;
    [SerializeField] private MainMenuController mainMenuController; // Ana menüde kullanýlacak event kontrolü


    public delegate void delegates(string patientname);
    public delegates mainMenuDelegates;
    
    void Start()
    {
        mainMenuDelegates+=mainMenuController.WelcomeTextChanger;
        
        loader.LoadPatient();

        mainMenuDelegates(loader.patient.patientName);


    }

}
