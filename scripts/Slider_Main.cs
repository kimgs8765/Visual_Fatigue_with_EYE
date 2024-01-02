using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Slider_Main : MonoBehaviour
{
    [SerializeField]Slider curr_slider;
    float threshold_x = 0;
    float threshold_y = 0;
    float threshold_z = 0;

    [SerializeField]int slider_num = 0;
    [SerializeField] int slider_group_num = 0;
    List<GameObject> sliders = new List<GameObject>();
    
    GameObject sign;
    GameObject handle;

    Transform canvas;
    [SerializeField] Transform selected_group;
    List<GameObject> slider_groups;

    //public Start_button str = new Start_button();

    // Start is called before the first frame update
    void Start()
    { 
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        slider_groups = Get_Children(canvas);
        selected_group = slider_groups[slider_group_num].transform;

        sliders = Get_Children(selected_group);
        handle = Get_Last_Chlid(sliders[slider_num].transform);
        sign = canvas.GetChild(canvas.childCount - 1).gameObject;

        curr_slider = sliders[slider_num].GetComponent<Slider>();
        curr_slider.value = 4;

        Slider_pos.Move_sign_to_pos(sign, handle);
    }

    // Update is called once per frame
    void Update()
    {
        slider_groups[slider_group_num].SetActive(false);

        float y = Input.GetAxis("joy_y");
        float x = Input.GetAxis("joy_x");
        float z = Input.GetAxis("Submit");

        Chage_vlaue(x);
        Chage_slider(y);
        Change_group(z);

        slider_groups[slider_group_num].SetActive(true);
        selected_group = slider_groups[slider_group_num].transform;
        sliders = Get_Children(selected_group);

        Slider_action(slider_num, sign, handle);

        if (slider_group_num >= slider_groups.Count - 1 )
        {
            slider_num = 0;

            if (curr_slider.value == 1 && threshold_z >= 10)
            {
                List<GameObject> every_sliders = new List<GameObject>();

                foreach (GameObject slider_group in slider_groups.GetRange(0, slider_groups.Count - 1))
                {
                    sliders = Get_Children(slider_group.transform);

                    foreach (GameObject slider in sliders.GetRange(0, sliders.Count - 1))
                    {
                        every_sliders.Add(slider);
                    }

                }

                Save_and_Del.Save(every_sliders);
                threshold_z = 0;

                if(Start_button.scence_num > 0 && Start_button.scence_num < Start_button.scenes.Count)
                {
                    Util.Move2Next_Scene(Start_button.scenes[Start_button.scence_num]);
                    Debug.Log($"{Start_button.scenes[Start_button.scence_num]}¾À ÀÌµ¿ÇÔ");
                    Start_button.scence_num++;
                }

                else {Util.Back2Main(); }

                

            }

            else if(curr_slider.value == 0 && threshold_z >= 10)
            {
                slider_groups[slider_group_num].SetActive(false);
                slider_group_num = slider_groups.Count - 2; 
                slider_num = 0; 
            }
        }

    }


    //---------------------------------------Custom Function------------------------------------------//

    List<GameObject> Get_Children(Transform parent)
    {
        int num_children = parent.childCount;
        List<GameObject> children = new List<GameObject>();

        for (int child_num = 0; child_num < num_children; child_num++)
        {
            GameObject child = parent.GetChild(child_num).gameObject;
            if (child.name != "sign")
            {
                children.Add(child);
            }

        }

        return children;
    }

    GameObject Get_Last_Chlid(Transform parent)
    {
        GameObject last_chlid;

        while (parent.childCount != 0)
        {
            last_chlid = parent.GetChild(parent.childCount - 1).gameObject;
            parent = last_chlid.transform;
        }
        last_chlid = parent.gameObject;

        return last_chlid;
    }

    void Slider_action( int slider_num, GameObject sign, GameObject handle)
    {
        curr_slider = sliders[slider_num].GetComponent<Slider>();
        handle = Get_Last_Chlid(sliders[slider_num].transform);
        Slider_pos.Move_sign_to_pos(sign, handle);
    }

    void Chage_vlaue(float x)
    {
        threshold_x += x;

        if (x != 0)
        {
            if (threshold_x > 15)
            {
                curr_slider.value += 1;
                threshold_x = 0;
            }

            if (threshold_x < -15)
            {
                curr_slider.value -= 1;
                threshold_x = 0;
            }
        }
        else { threshold_x = 0; }

    }

    void Chage_slider(float y)
    {
        threshold_y += y;

        if (Mathf.Abs(y) >= 0.1)
        {
            if (threshold_y > 30)
            {
                slider_num -= 1;

                if (slider_num < 0)
                {
                    slider_num = 0;
                }

                threshold_y = 0;
            }

            if (threshold_y < -30)
            {
                slider_num += 1;

                if (slider_num >= sliders.Count - 1)
                {
                    slider_num = sliders.Count - 1;
                }

                threshold_y = 0;
            }
        }
        else { threshold_y = 0; }

    }

    float Change_group(float z)
    {
        threshold_z += z;

        if (slider_num == sliders.Count - 1 & z != 0)
        {
            if (threshold_z >= 15)
            {
                int add = (curr_slider.value == 0) ? -1 : 1;
                slider_group_num += add;
                Slider_action(slider_num, sign, handle);
                curr_slider.value = 1;

                threshold_z = 0;
                slider_num= 0;

            }

            //if (slider_group_num >= slider_groups.Count - 1)
            //{
            //    slider_num = 0;
            //    slider_group_num = slider_groups.Count - 1;
            

            //}

            if (slider_group_num < 0)
            {
                slider_group_num = 0;
                slider_num= 0;

            }

        }

        else if (slider_num == sliders.Count-1  & z == 0)
        { threshold_z = 0; }

        return threshold_z; 
    }
}

