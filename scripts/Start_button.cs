using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement ;
using UnityEngine.UI;
using Varjo.XR;
using static Varjo.XR.VarjoHeadsetIPD;

public class Start_button : MonoBehaviour
{
    static public List<string> scenes;
    static public int scence_num = 0;
    public TMP_InputField block_order; 

    public void On_click_Start () 
    {
        scence_num = 0;
        Debug.Log($"scene_num = {scence_num}");
        List<string> scene_names = new List<string>() { "HMHB", "HMLB", "LMHB", "LMLB" };
        List<List<string>> permutations = Util.PermutationGenerator.GeneratePermutations(scene_names);
        scenes = Return_Blocks(permutations, int.Parse(block_order.text));
        bool set_ipd = SetInterPupillaryDistanceParameters(IPDAdjustmentMode.Automatic);
        Open_scene(scenes[scence_num]);

        scence_num++;



        foreach (string scene in scenes )
        {
            Debug.Log(scene);
        }
        Debug.Log("실험 시작");

    }

    public void On_Click_Sample()
    {
        scence_num = 0;

        string sample = gameObject.name; 
        bool set_ipd = SetInterPupillaryDistanceParameters( IPDAdjustmentMode.Automatic);
        Open_scene(sample);
    }


    public void Open_scene(string scene)
    { 
        SceneManager.LoadScene(scene);
    }

    public List<string> Return_Blocks(List<List<string>> blocks, int block_order)

    { 
        List<string> scence_name = blocks[block_order];

        return scence_name;
    }


}
