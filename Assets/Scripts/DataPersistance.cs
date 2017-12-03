using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
public static class DataPersistance {
    /// <summary>
    /// 判断文件是否存在
    /// </summary>
    public static bool IsFileExists(string fileName)
    {
        return File.Exists(fileName);
    }

    /// <summary>
    /// 判断文件夹是否存在
    /// </summary>
    public static bool IsDirectoryExists(string fileName)
    {
        return Directory.Exists(fileName);
    }

    /// <summary>
    /// 创建一个文本文件    
    /// </summary>
    /// <param name="fileName">文件路径</param>
    /// <param name="content">文件内容</param>
    public static void CreateFile(string fileName, string content)
    {
        StreamWriter streamWriter = File.CreateText(fileName);
        streamWriter.Write(content);
        streamWriter.Close();
    }

    /// <summary>
    /// 创建一个文件夹
    /// </summary>
    public static void CreateDirectory(string fileName)
    {
        //文件夹存在则返回
        if (IsDirectoryExists(fileName))
            return;
        Directory.CreateDirectory(fileName);
    }

    public static void SetData(string fileName,int sceneid)
    {
        //将对象序列化为字符串
        StreamWriter streamWriter = File.CreateText(fileName);
        streamWriter.Write(sceneid);
        streamWriter.Close();
    }

    public static int GetData(string fileName)
    {
        StreamReader streamReader = File.OpenText(fileName);
        string data = streamReader.ReadToEnd();
        streamReader.Close();
        return Convert.ToInt32(data);
    }
}
