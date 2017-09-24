using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LocalPathController : MonoBehaviour
{
    private string PRE_FIX = "file://";
    private string _currentPath = "";
    private string[] _pathFiles;
    private int _pathLength;


    public GameObject _obbVideoSample;


    // Use this for initialization
    void Start ()
    {
        _currentPath = Directory.GetCurrentDirectory();
        _pathLength = Directory.GetFiles(_currentPath).Length;
        _pathFiles = new string[_pathLength];
        
        _pathFiles = Directory.GetFiles(_currentPath);
        foreach(string pathFile in _pathFiles)
            Debug.Log("pathFiles = " + pathFile);
        //_obbVideoSample.GetComponent<GvrVideoPlayerTexture>().videoURL = "file://"  + "/storage/MicroSD/111/TEST.mp4";
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Find("Text").GetComponent<Text>().text = "Dir = " + _currentPath + ".";
        //transform.Find("Text").GetComponent<Text>().text = "Dir = " + _obbVideoSample.GetComponent<GvrVideoPlayerTexture>().videoURL + ".";
    }

    public static string GetAndroidInternalFilesDir()
    {
        string[] potentialDirectories = new string[]
        {
        "/mnt/sdcard",
        "/sdcard",
        "/storage/sdcard0",
        "/storage/sdcard1"
        };

        if (Application.platform == RuntimePlatform.Android)
        {
            for (int i = 0; i < potentialDirectories.Length; i++)
            {
                if (Directory.Exists(potentialDirectories[i]))
                {
                    return potentialDirectories[i];
                }
            }
        }
        return "";
    }
}
