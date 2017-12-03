using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {

    public int sceneID = 0;

    void Awake()
    {
         //定义存档路径
        string dirpath = Application.persistentDataPath + "/Save";
        //创建存档文件夹
        DataPersistance.CreateDirectory(dirpath);
        //定义存档文件路径
        string filename = dirpath + "/GameData.txt";

        //读取数据
        int t1 = 0;
        if (DataPersistance.IsFileExists(filename))
        {  
            t1 = DataPersistance.GetData(filename);//存档id
        }

        int t2 = Application.loadedLevel;

        if (t2 == 0 && (t1 == 0 || t1 == 4))
        {
            if (t1 == 4)
            {
                GameObject.Find("GameManager").GetComponent<Manager2>().ifFinished = true;
            }
            DataPersistance.SetData(filename, t2);
            t1 = 0;
            
        }
        if (t1 <= t2)
        { //保存数据
            DataPersistance.SetData(filename, t2);
            sceneID = t2;
        }
        else
        {
            sceneID = t1;
        }
        
    }
   


}
