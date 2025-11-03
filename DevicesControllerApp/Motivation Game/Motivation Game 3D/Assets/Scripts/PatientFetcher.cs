using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatientFetcher : MonoBehaviour
{

    public string fileName = "patient.json";
    public PatientData patient;

    public bool LoadPatient()   // Hasta bilgisini json.'dan unity'ye yükleme
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            patient = JsonUtility.FromJson<PatientData>(json);
            Debug.Log($"Hasta bilgisi yüklendi: {patient.patientName}");
            return true;
        }
        else
        {
            Debug.LogError("JSON dosyasý bulunamadý: " + path);
            return false;
        }




    }
}
