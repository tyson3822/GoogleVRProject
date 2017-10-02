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
    private string[] _pathFolders;
    private int _pathLength;

    public GameObject _backButtonSample;
    public GameObject _fileButtonSample;
    public GameObject _folderButtonSample;
    public GameObject _obbVideoSample;


    // Use this for initialization
    void Start ()
    {
        _currentPath = Directory.GetCurrentDirectory();
        _pathLength = Directory.GetFiles(_currentPath).Length;
        _pathFiles = new string[_pathLength];

        this.GenerateCurrentPathObject();

        //_obbVideoSample.GetComponent<GvrVideoPlayerTexture>().videoURL = "file://"  + "/storage/MicroSD/111/TEST.mp4";
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Find("CurrentLocalPathText").GetComponent<Text>().text = "Dir = " + _currentPath + ".";
        //transform.Find("Text").GetComponent<Text>().text = "Dir = " + _obbVideoSample.GetComponent<GvrVideoPlayerTexture>().videoURL + ".";
    }

    public void OnBackButtonClick()
    {
        this.DestoryPathObjectList();
        //Debug.Log("parent = " + Directory.GetParent(_currentPath).FullName);
        _currentPath = Directory.GetParent(_currentPath).FullName;
        this.GenerateCurrentPathObject();
    }

    public void OnFolderButtonClick(GameObject buttonObject)
    {
        this.DestoryPathObjectList();
        _currentPath =  Path.Combine(_currentPath, buttonObject.transform.Find("Text").GetComponent<Text>().text);
        //Debug.Log("enter " + _currentPath + " folder!!");
        this.GenerateCurrentPathObject();
    } 

    public void OnFileButtonClick(GameObject buttonObject)
    {
        this.DestoryPathObjectList();
        _currentPath = Path.Combine(_currentPath, buttonObject.transform.Find("Text").GetComponent<Text>().text);
        //Debug.Log("open " + _currentPath + " file!!");
    }

    private void DestoryPathObjectList()
    {
        Transform pathObjectList = this.transform.Find("FilesFoldersList");
        foreach (Transform child in pathObjectList)
            Destroy(child.gameObject);
    }

    //public static string GetAndroidInternalFilesDir()
    //{
    //    string[] potentialDirectories = new string[]
    //    {
    //    "/mnt/sdcard",
    //    "/sdcard",
    //    "/storage/sdcard0",
    //    "/storage/sdcard1"
    //    };

    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        for (int i = 0; i < potentialDirectories.Length; i++)
    //        {
    //            if (Directory.Exists(potentialDirectories[i]))
    //            {
    //                return potentialDirectories[i];
    //            }
    //        }
    //    }
    //    return "";
    //}

    private void GenerateCurrentPathObject()
    {
        _pathFolders = Directory.GetDirectories(_currentPath);

        try
        {
            //Debug.Log("CurrentPath = " + _currentPath);
            //Debug.Log("Directory.GetParent(_currentPath).Exists = " + Directory.GetParent(_currentPath).Exists);
            if (Directory.GetParent(_currentPath).Exists)
            {
                this.GenerateBackButtonUnderFolder();
            }
        }
        catch { }

        foreach (string pathFolder in _pathFolders)
        {
            //Debug.Log("pathFolder = " + pathFolder);
            this.GenerateFolderButtonUnderFolder(Path.GetFileName(pathFolder));
        }

        _pathFiles = Directory.GetFiles(_currentPath);
        foreach (string pathFile in _pathFiles)
        {
            //Debug.Log("pathFiles = " + pathFile);
            this.GenerateFileButtonUnderFolder(Path.GetFileName(pathFile));
        }
    }

    private void GenerateBackButtonUnderFolder()
    {
        GameObject cloneFileButton = Instantiate(_backButtonSample, this.transform.Find("FilesFoldersList"));
        cloneFileButton.transform.Find("Text").GetComponent<Text>().text = "..";
        cloneFileButton.SetActive(true);
    }

    private void GenerateFileButtonUnderFolder(string fileName)
    {
        GameObject cloneFileButton = Instantiate(_fileButtonSample, this.transform.Find("FilesFoldersList"));
        cloneFileButton.transform.Find("Text").GetComponent<Text>().text = fileName;
        cloneFileButton.SetActive(true);
    }

    private void GenerateFolderButtonUnderFolder(string folderName)
    {
        GameObject cloneFileButton = Instantiate(_folderButtonSample, this.transform.Find("FilesFoldersList"));
        cloneFileButton.transform.Find("Text").GetComponent<Text>().text = folderName;
        cloneFileButton.SetActive(true);
    }
}
