using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ExcelLibrary;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Editor
{
    public class ExportXlsData
    {
        private static string FILE_SUFFIX = ".xls";
        private static string FILE_BIN_SUFFIX = ".bin";
        [MenuItem("PPL/Excle生成.bin文件")]
        public static void Config_MakeBinData()
        {
            var path = Application.dataPath + "/Config/";
            
            var files = Directory.GetFiles(path);
            var count = files.Length;
            
            List<string> file_name_list = new List<string>();

            if (count > 0)
            {
                var progress = 0.0f;
                var step = 1.0f/count;
                Debug.Log("*****************************");
                for (int i = 0; i < count; i++)
                {
                    progress += step;
                    if (files[i].EndsWith(FILE_SUFFIX))
                    {
                        var xlsfileName = files[i].Replace(FILE_SUFFIX, "");
                        var pathFiled = xlsfileName.Split('/');
                        if (pathFiled.Length > 0)
                        {
                            xlsfileName = pathFiled[pathFiled.Length - 1];
                            file_name_list.Add(xlsfileName + FILE_BIN_SUFFIX);

                            try
                            {
                                Debug.LogFormat("{0}{1} --->>> {2}.bin", xlsfileName, FILE_SUFFIX, xlsfileName);
                                ExportBin(path, xlsfileName);
                                EditorUtility.DisplayCancelableProgressBar("Excle --->>> .bin", xlsfileName, progress);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogFormat("fileName: {0}, Error: {1}", xlsfileName, ex);
                                throw;
                            }
                        }
                    }
                }

                PlayerPrefs.SetString(StaticTag.PLAYERPREFS_CONFIG, string.Join("|", file_name_list.ToArray()));

                EditorUtility.ClearProgressBar();
                Debug.Log("*****************************");
            }
        }

        private static void ExportBin(string filePath, string filename)
        {
            ByteArray ba = new ByteArray();
            
            Assembly asm = Assembly.LoadFile(Application.dataPath + "/../Library/ScriptAssemblies/Assembly-CSharp.dll");

            DataSet dataSet = DataSetHelper.CreateDataSet(filePath + filename + FILE_SUFFIX);

            if (dataSet != null)
            {
                //int sheetNum = dataSet.Tables.Count;

                int canreadSheet = 0;
                foreach (DataTable sheet in dataSet.Tables)
                {
                    string className = sheet.TableName;
                    object obj = asm.CreateInstance(className);

                    //Debug.Log("当前表名是： " + sheet.TableName);
                    if (obj == null)
                    {
                        continue;
                    }
                    ++canreadSheet;
                }
                ba.writeInt(canreadSheet);
                //Converter.WriteInt(writer, sheetNum);
                foreach (DataTable sheet in dataSet.Tables)
                {
                    string className = sheet.TableName;
                    object obj = asm.CreateInstance(className);
                    if (obj == null)
                    {
                        //MUtils.ShowNotice("Load data error: "+className+" in "+filePath+" has no data.");
                        continue;
                    }
                    FieldInfo[] fis = obj.GetType().GetFields();

                    ba.writeUTF(sheet.TableName);
                    //Converter.WriteJavaString(writer,sheet.TableName);

                    int rows = sheet.Rows.Count;
                    int rowCount = 0;
                    for (int r = 0; r != rows; ++r)
                    {
                        if (string.IsNullOrEmpty(sheet.Rows[r].ItemArray[0].ToString()))
                        {
                            break;
                        }

                        rowCount++;
                    }
                    ba.writeInt(rowCount);
                    if (rows > 0)
                        ba.writeInt(sheet.Rows[0].ItemArray.Length);
                    else
                        ba.writeInt(0);

                    foreach (DataRow row in sheet.Rows)
                    {
                        int i = 0;
                        foreach (object item in row.ItemArray)
                        {
                            if (i >= fis.Length)
                                break;

                            if (fis[i].FieldType == typeof(int))
                            {
                                int val = 0;
                                int.TryParse(item.ToString(), out val);
                                ba.writeInt(val);
                            }
                            else if (fis[i].FieldType == typeof(short))
                            {
                                short val = 0;
                                short.TryParse(item.ToString(), out val);
                                ba.writeShort(val);
                            }
                            else if (fis[i].FieldType == typeof(long))
                            {
                                long val = 0;
                                long.TryParse(item.ToString(), out val);
                                ba.writeLong(val);
                            }
                            else if (fis[i].FieldType == typeof(float) || fis[i].FieldType == typeof(double) ||
                                     fis[i].FieldType == typeof(string))
                                ba.writeUTF(item.ToString());

                            ++i;
                        }
                    }
                }
            }

            //string binpath = AssetBundlePath.GetStreamingAssetsPath() + "bin/".Replace("file://","");
            string binpath = AssetBundlePath.GetStreamingAssetsPath().Replace("file://", "");
            binpath = binpath.Replace("file://", "");

            var file = new FileStream(binpath + filename + FILE_BIN_SUFFIX, FileMode.Create, System.IO.FileAccess.Write);

            byte[] bytes = ba.data;
            file.Write(bytes, 0, bytes.Length);
            file.Close();
        }

    }
}
