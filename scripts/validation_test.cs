using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 
using Varjo.XR;
using static Varjo.XR.VarjoEyeTracking;

public class validation_test : MonoBehaviour
{
    GazeCalibrationQuality gaze_qual ;
    public TextMeshProUGUI text;

    void Start()
    {
        bool request = VarjoEyeTracking.RequestGazeCalibration();
        text = FindObjectOfType<TextMeshProUGUI>();  
        
    }


    // Update is called once per frame
    void Update()
    {
        gaze_qual = VarjoEyeTracking.GetGazeCalibrationQuality();

        if (gaze_qual.right == VarjoEyeTracking.GazeEyeCalibrationQuality.High)
        {
            text.text = "High";
            text.faceColor = Color.green;
        }

        else if (gaze_qual.right == VarjoEyeTracking.GazeEyeCalibrationQuality.Medium)
        {
            text.text = "Medium";
            text.faceColor = Color.white;
        }
        else if (gaze_qual.right == VarjoEyeTracking.GazeEyeCalibrationQuality.Low)
        {
            text.text = "Low";
            text.faceColor = new Color(255, 129, 0); // darkorange
        }

        else if (gaze_qual.right == VarjoEyeTracking.GazeEyeCalibrationQuality.Invalid)
        {
            text.text = "Invalid";
            text.faceColor = Color.red;
        }

    }


}
