using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class LoadConfigManager
{

    public static bool allConfigLoaded = false;
   
    public static IEnumerator LoadConfigData()
    {
//        Debug.Log("表的路径是： " + AssetBundlePath.GetStreamingAssetsPath());

        string path = AssetBundlePath.GetStreamingAssetsPath();
        string[] fileNames = PlayerPrefs.GetString(StaticTag.PLAYERPREFS_CONFIG).Split('|');

        int count = fileNames.Length;
        float startTime = Time.realtimeSinceStartup;

        for (int i = 0; i < count; ++i)
        {
            WWW www = new WWW(path + fileNames[i]);
            yield return www;

            ParseFile(www.bytes, www.url);
            Debug.LogFormat("###Load {0} Success!###", fileNames[i]);

        }
        Debug.LogFormat("load all data time: {0}" , (Time.realtimeSinceStartup - startTime));

        allConfigLoaded = true;
    }

    private static void ParseFile(Byte[] bytes, string filePath)
    {
        Assembly asm = Assembly.GetExecutingAssembly();

        ByteArray ba = new ByteArray(bytes);
        int sheetCount = ba.readInt();

        for (int i = 0; i != sheetCount; ++i)
        {
            string className = ba.readUTF();
            int rowCount = ba.readInt();
            int colCount = ba.readInt();

            // 2. read values
            for (int r = 0; r != rowCount; ++r)
            {
                object obj = asm.CreateInstance(className);
                if (obj == null)
                {
                    Debug.Log("Load data error: " + className + " in " + filePath + " has no data.");
                    break;
                }
                FieldInfo[] fis = obj.GetType().GetFields();

                int id = 0;
                for (int c = 0; c != colCount; ++c)
                {
                    //string val = ba.readUTF();
                    if (c < fis.Length)
                    {
                        if (fis[c].FieldType == typeof(int))
                        {
                            int nVal = ba.readInt();
                            if (c == 0)
                                id = nVal;
                            fis[c].SetValue(obj, nVal);
                        }
                        else if (fis[c].FieldType == typeof(string))
                        {
                            fis[c].SetValue(obj, ba.readUTF());
                        }
                        else if (fis[c].FieldType == typeof(float))
                        {
                            string val = ba.readUTF();
                            fis[c].SetValue(obj, float.Parse(val));
                        }
                        else if (fis[c].FieldType == typeof(double))
                        {
                            string val = ba.readUTF();
                            fis[c].SetValue(obj, double.Parse(val));
                        }
                        else if (fis[c].FieldType == typeof(long))
                        {
                            fis[c].SetValue(obj, ba.readLong());
                        }
                    }
                }
                try
                {
                    ConfigDataManager.GetInstance().AddToDictionary(id, obj, rowCount);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex.ToString() + " @Id=" + id + " of " + className + " in " + filePath);
                }
            }
        }
    }
}
