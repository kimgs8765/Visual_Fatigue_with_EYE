using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ExportGazeBoxCoor : MonoBehaviour
{
    private string csvSeparator = ",";
    private string csvFileName = "GazeBoxCoor";
    private string[] csvHeaders = new string[3] {"X", "Y", "Z"};
    private string csvDirectoryName = "GazeBoxCoor";
    private string timeStampHeader = "CaptureTime";

    private int trial_num ;
    private float save_time = 0.0f;


    string GetDirectoryPath()
    {
            string dir = Application.dataPath + "/data/" + SceneManager.GetActiveScene().name ;

        return dir;
    }


    string GetFilePath( int trial_num)
    {
        return GetDirectoryPath() + "/" + csvFileName + "_" +  trial_num +".csv" ;
    }


    public void CreateCsv( int trail_num)
    {
        using (StreamWriter sw = File.CreateText(GetFilePath(trail_num)))
        {
            string finalString = "";
            for (int i =0; i < csvHeaders.Length; i++)
            {
                if(finalString != "")
                {
                    finalString += csvSeparator;
                }
                finalString += csvHeaders[i];
            }
            finalString += csvSeparator + timeStampHeader;
            sw.WriteLine(finalString);
        }
    }

    public void AppendToCsv(float[] floats, int trial_num, float save_time)
    {
        using (StreamWriter sw = File.AppendText(GetFilePath(trial_num)))
        {
            string finalString = "";
            for ( int i = 0; i < floats.Length; i++) { 
                if(finalString != "")
                {
                    finalString += csvSeparator;
                }
                finalString += floats[i];
            }
            finalString += csvSeparator +  save_time;
            sw.WriteLine(finalString);
        }

    }


    void FixedUpdate()
    {
        /// 조이스틱 시작할 때 같이 시작하고 끝내기 
        trial_num = GameObject.FindGameObjectWithTag("canvas").GetComponent<joystick_move>().trial_num;

        if (GameObject.FindGameObjectWithTag("canvas").transform.Find("Slider").gameObject.activeSelf)
        {
            if (!File.Exists(GetFilePath(trial_num)))
            {
                CreateCsv(trial_num);
            }

            save_time += Time.deltaTime; 

            // Add x,y,z coordinates to the list
            float[] GazeBoxCoor = new float[3];
            GazeBoxCoor[0] = transform.localPosition.x;
            GazeBoxCoor[1] = transform.localPosition.y;
            GazeBoxCoor[2] = transform.localPosition.z;
            AppendToCsv(GazeBoxCoor, trial_num, save_time);
            //print(GazeBoxCoor[0] + "Y" + GazeBoxCoor[1] + "Z" + GazeBoxCoor[2]);
        }

        else { save_time = 0.0f; }

    }
}
