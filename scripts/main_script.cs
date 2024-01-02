using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Varjo.XR;
using UnityEngine.Scripting;
using System.IO;
using UnityEngine.AI;
using static Varjo.XR.VarjoEyeTracking;
using static Varjo.XR.VarjoHeadsetIPD;

public class main_script : MonoBehaviour
{
    private GameObject screen;
    [SerializeField] private GameObject[] players;
    public GameObject player;
    private RaycastHit hit;
    [SerializeField] private Transform target;

    [SerializeField] private float max_time = 5.0f;
    [SerializeField] private float break_time = 5.0f;
    private float hit_time = 0.0f;
    private float playing_time = 0.0f;

    private int video_num = 0;
    // public Start_button str = new Start_button();

    private void OnEnable()
    {
        screen = GameObject.FindGameObjectWithTag("vplayer");

        players = Util.GetChildren(screen);

        player= players[video_num];
    }

    public void Start()
    {
        // �Ź� Calibration�� �ʿ��� ��� 
        bool request = VarjoEyeTracking.RequestGazeCalibration();
    }

    public void FixedUpdate()
    {
        // 3�ʰ� ������ ���� �� ���� ���
        gameObject.transform.position = Camera.main.transform.position;
        gameObject.transform.rotation = Camera.main.transform.rotation;

        Vector3 direction = target.transform.position - gameObject.transform.position;

        if (Physics.Raycast(gameObject.transform.position, direction, out hit, 10))
        {
            Debug.DrawRay(gameObject.transform.position, direction, Color.red);

            if (hit.collider.CompareTag("w_point"))
            {
                hit_time += Time.deltaTime;
                // Debug.Log(hit_time);
            }
            else { hit_time = 0.0f; }
        }


        // ���� ��� 
        if (hit_time >= 3.0f) // 3�ʷ� �ٲ�� �� 
        {
            player.GetComponent<Renderer>().material.color = Color.white;
            player.GetComponent<VideoPlayer>().Play();
            player.transform.GetChild(0).gameObject.SetActive(false);
            hit_time = 0.0f;
        }

        // ���� ����ð� 30�� 
        if (player.GetComponent<VideoPlayer>().isPlaying == true)
        {
            playing_time += Time.deltaTime;

            if (playing_time >= max_time)  // �׽�Ʈ�� 10�� video_Suffle.videos[v_index]; 
            {
                video_num++;
                player.GetComponent<VideoPlayer>().Pause();
                player.SetActive(false);

                // player ��ȯ
                try
                {
                    player = players[video_num];
                    player.SetActive(true);
                    player.GetComponent<VideoPlayer>().Prepare();
                    playing_time = 0.0f;

                    StartCoroutine(Util.DelayTime(player, break_time));
                    Debug.Log("5�� ��ٸ�����");
                }
                catch
                { 
                    Debug.Log($"���� SSQ_{Start_button.scence_num}���� �̵���");
                    StartCoroutine(Util.Move2SSQ($"SSQ_{Start_button.scence_num}", 2));
                    //Util.Move2Next_scene($"SSQ_{Start_button.scence_num}";
                }

            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc");
            Util.Back2Main();
        }
    }
}
