using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Slider_pos
{
    public static Vector3 Get_slider_pos(GameObject obj)
    {
        Vector3 slider_pos = obj.transform.position;

        return slider_pos;
    }

    public static void Move_sign_to_pos(GameObject sign, GameObject obj)
    {
        sign.transform.position = Get_slider_pos(obj); 
    }

}

