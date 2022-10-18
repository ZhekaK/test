using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class control : MonoBehaviour
{
    public float Vspeed;
    float speed;
    public Camera mainCam;
    string path;
    string mainPath = "";
    public string directroryName = "Screenshots";
    string SName;
    int i;

    void Start()
    {
        speed = Vspeed;
        path = Application.dataPath + "/";
        mainPath = path + directroryName + "/";
        if(!Directory.Exists(mainPath))
        {
            Directory.CreateDirectory(mainPath);
        }
    }

    void Update()
    {
        if(Input.GetKey("g")) 
        {
            speed = 250f;
        }
        else
        {
            speed = Vspeed;
        }
        transform.Translate(0, 0, speed * Time.deltaTime);
        SaveScreenshots(SName);
    }

    void SaveScreenshots(string name)
    {
        i++;
        SName = "" + i;

        int sw = (int)Screen.width;
        int sh = (int)Screen.height;

        RenderTexture rt = new RenderTexture(sw, sh, 0);
        mainCam.targetTexture = rt;
        Texture2D sc = new Texture2D (sw, sh, TextureFormat.RGB24, false);
        mainCam.Render();

        RenderTexture.active = rt;
        sc.ReadPixels(new Rect(0, 0, sw, sh), 0, 0);
        mainCam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] bytes = sc.EncodeToPNG();
        string finalPath = mainPath + name + ".png";
        File.WriteAllBytes(finalPath, bytes);
    }
}
