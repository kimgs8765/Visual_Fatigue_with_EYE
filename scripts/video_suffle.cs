using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class video_suffle : MonoBehaviour
{

    [SerializeField] private VideoClip v1 = null;
    [SerializeField] private VideoClip v2 = null;
    [SerializeField] private VideoClip v3 = null;
    [SerializeField] private VideoClip v4 = null;
    // [SerializeField] private VideoClip v5 = null;

    private VideoClip[] clips;
    private GameObject[] videos;

    private string cur_dir;
    private string data_dir;
    private string scene; 

    // Start is called before the first frame update
    void Awake()
    {
        cur_dir = Application.dataPath;
        data_dir = cur_dir + "/data";
        scene = SceneManager.GetActiveScene().name;

        clips = new VideoClip[4] { v1, v2, v3, v4 }; //, v5 };
        clips = Util.ShuffleArray(clips);

        try
        {
            Directory.CreateDirectory(data_dir);

            try { Directory.CreateDirectory($"{data_dir}/{scene}"); }

            catch { return; }
        }

        catch { return; }


        StreamWriter file = new StreamWriter($"{data_dir}/{scene}/video_order.csv");
        file.WriteLine("order_1,order_2,order_3,order_4"); //,order_5");
        for (int clip_num = 0; clip_num < clips.Length; clip_num++)
        {
            file.Write($"{clips[clip_num].name},"); 
        }
        file.Close();

        videos = new GameObject[clips.Length];
        videos = Util.GetChildren(gameObject);

        for (int video_num = 0; video_num < videos.Length; video_num++)
        {
            videos[video_num].GetComponent<VideoPlayer>().clip = clips[video_num];
        }

    }

}

