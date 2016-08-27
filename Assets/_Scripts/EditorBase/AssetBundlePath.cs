using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundlePath : MonoBehaviour
{
    public const string SCENES_NAME = "Streamed-Scenes.unity3d";
    public const string BACKGROUNDS_NAME = "Backgrounds.unity3d";
    public const string UISCENES_NAME = "Streamed-UIScenes.unity3d";

    public static string texturePath;
    public static string localDataPath;
    public static string assetBundlePath;
    public static string configPath;

    public static string GetStreamingAssetsPath()
    {
        //暂时先写死了
        string path = "";// "file://" + Application.streamingAssetsPath + "/";
       // string path = "file://" + Application.streamingAssetsPath + "/";
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = "file://" + Application.streamingAssetsPath + "/bin/";
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.streamingAssetsPath + "/bin/";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = "file://" + Application.streamingAssetsPath + "/bin/";
        }
        else if (Application.platform==RuntimePlatform.WindowsEditor)
        {
            path = "file://" + Application.streamingAssetsPath + "/bin/";
            
        }
        return path;

    }


    /// <summary>
    /// 获取本地的streaming 文件目录
    /// </summary>
    /// <returns></returns>
    public static string GetStreamingAssetbuddleResourcePath()
    {
        string path = "";
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
			path = GetStreamingAssetsPath() + "Windows/";
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            path = GetStreamingAssetsPath() + "Android/";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXEditor )
        {
            path = GetStreamingAssetsPath() + "IOS/";
        }
        return path;
    }






    /// <summary>
    /// 获取外部包的路径
    /// </summary>
    /// <returns></returns>
    public static string GetOutPackagePath()
    {
        string path = "";
        if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.OSXEditor)
        {
            path = "file://" + Application.dataPath + "/../DownloadData/";
        }

        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = "file://" + Application.dataPath + "/../DownloadData/";
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            path = "file://" + Application.persistentDataPath + "/";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = "file://" + Application.persistentDataPath + "/";
        }
      
        return path;
    }



    public static string GetSettingConfigPath()
    {
        return GetStreamingAssetsPath() + "Setting/";
    }

    

    public static string GetTexturPath()
    {
        texturePath = "";
        //if (Config.COPY_RESOUCE_OUT)
        //{
        //    texturePath = GetOutPackagePath();
        //}
        //else
        //{
        //    if (PlatformManager.IsEditor())
        //    {
        //        texturePath = GetStreamingAssetsPath() + "Texture/RoleShow";
        //    }
        //    else if (Application.platform == RuntimePlatform.WindowsPlayer)
        //    {
        //        texturePath = GetStreamingAssetsPath() + "Texture/RoleShow";
        //    }
        //    else if (Application.platform == RuntimePlatform.Android)
        //    {
        //        texturePath = GetStreamingAssetsPath() + "Texture/RoleShow";
        //    }
        //    else if (Application.platform == RuntimePlatform.IPhonePlayer)
        //    {
        //        texturePath = GetStreamingAssetsPath() + "Texture/RoleShow";
        //    }
        //}
        return texturePath;
    }

}
