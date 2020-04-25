﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JSONSerializer : ISerializer {
    // Start is called before the first frame update


    string folderName = "/PersistentData/session_";
    int sessionIndex;
    string extension = ".json";
    int iterPlace = 0;
    //Aqui tengo que cerrrar lo que viene a ser el archivo json
    // Update is called once per frame

     public JSONSerializer() {

    }

    public override string serialize(TrackerEvent tE) {
        //string j = JsonUtility.ToJson(tE);
 ////////////////////
 //CAMBIAR
 //Solo pilla el del padre macho k tonto soy

/////////////////       
        string j = tE.SerializeToJson() +  "}"  + "\n";
        persistance(j);
        return j;
    }
    public override bool persistance(string eventTrace)
    {
        sessionIndex = GM.instance.getSession();
        bool result = false;
        string fullpath = Application.dataPath+folderName+sessionIndex+extension;
        if (!File.Exists(fullpath)) {
            File.Create(fullpath).Close();
        }
        using (StreamWriter w = File.AppendText(fullpath)) {
            w.Write(eventTrace);
            result = true;
        }
        return result;
    }
}
