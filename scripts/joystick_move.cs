using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class joystick_move : MonoBehaviour
{
    // Slider var
    public float speed = 0.02f;
    private GameObject mainslider;
    private float moving;
    private List<List<float>> values = new List<List<float>>();

    // Player var

    [SerializeField] GameObject player;
    private GameObject cam;

    float save_time = 0.0f;

    public int trial_num = 1;

    string m_path;
    string data_path;
    [SerializeField] string data_dir;

    public bool eye_trac_bool = false;

    public void Start ()
    {
        mainslider = gameObject.transform.Find("Slider").gameObject; // Canvas 안에 있는 자식 슬라이더 선언

        mainslider.GetComponent<Slider>().value = 4;

        mainslider.gameObject.SetActive(false);

        cam = GameObject.FindGameObjectWithTag("eye_box");
        player = cam.GetComponent<main_script>().player;

        
    }

    public void FixedUpdate()
    {
        // 영상이 재생되면 슬라이더 Enabled & silider value를 list에 저장 
        if (player.GetComponent<VideoPlayer>().isPlaying == true)
        {
            mainslider = gameObject.transform.Find("Slider").gameObject;

            mainslider.SetActive(true);  // << 슬라이더 Activate
            eye_trac_bool = true;

            moving = speed * Input.GetAxis("joy_x");

            if (Mathf.Abs(Input.GetAxis("joy_x")) >= 0.1f)
            {
                mainslider.GetComponent<Slider>().value += moving;
            }

            save_time += Time.deltaTime;
            values.Add(new List<float> { save_time, mainslider.GetComponent<Slider>().value });
        }

        // 영상이 멈추면 list를 CSV로 저장
        else if (player.GetComponent<VideoPlayer>().isPlaying == false & save_time != 0.0f)
        {
            mainslider.SetActive(false); // << 슬라이더 non - activate
            eye_trac_bool = false;

            m_path = Application.dataPath;
            data_dir = $"/data/{SceneManager.GetActiveScene().name}";
            data_path = m_path + data_dir;


            // 데이터 저장
            StreamWriter save_file = new StreamWriter(data_path + $"/trial_{trial_num}.csv");
            save_file.WriteLine("time, rating");

            foreach (List<float> i in values)
            {
                save_file.WriteLine("{0}, {1}", i[0].ToString(), i[1].ToString());
            }

            save_file.Close();
            trial_num++;
            values = new List<List<float>>();
            Debug.Log("데이터 저장됨");

            // 슬라이더 & player 초기화
            mainslider = gameObject.transform.Find("Slider").gameObject;
            mainslider.GetComponent<Slider>().value = 4;

            player = cam.GetComponent<main_script>().player;
            save_time = 0.0f;

        }

    }

}

    
