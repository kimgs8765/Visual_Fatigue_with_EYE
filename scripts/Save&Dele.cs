using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Save_and_Del
{
    public static String Make_file()
    {
        string m_path = Application.dataPath;
        string data_path = m_path + "/data";
        string scene_path = data_path + "/SSQ";

        if (!Directory.Exists(data_path))
        {
            Directory.CreateDirectory(data_path);
        }
        if (!Directory.Exists(scene_path))
        { 
            Directory.CreateDirectory(scene_path); 
        }

        return scene_path;

    }

    public static void Save(List<GameObject> every_sldiers)
    {
        string scene = SceneManager.GetActiveScene().name;
        string scene_path = Make_file();
        string file_name = scene_path + $"/{scene}.csv"; 

        using StreamWriter writer = new StreamWriter(file_name);

        foreach (GameObject slider in every_sldiers)
        {
            writer.Write(slider.transform.name + ',');
            writer.WriteLine(slider.GetComponent<Slider>().value);
        }

        Debug.Log("저장완료");
    }
    


}



        
    






