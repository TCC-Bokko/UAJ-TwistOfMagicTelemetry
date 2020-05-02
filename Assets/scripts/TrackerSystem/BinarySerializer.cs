using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class BinarySerializer : ISerializer
{
    string folderName = "/PersistentData/session_";
    int sessionIndex;
    string extension = ".dat";
    int iterPlace = 0;
    //Aqui tengo que cerrrar lo que viene a ser el archivo json
    // Update is called once per frame

    public BinarySerializer()
    {

    }

    public override string serialize(TrackerEvent tE)
    {
        //string j = JsonUtility.ToJson(tE);
        ////////////////////
        //CAMBIAR
        //Solo pilla el del padre macho k tonto soy

        /////////////////       
        string j = tE.SerializeToJson() + "}" + "\n";
        Debug.Log(j);
        persistance(j, tE);
        return j;
    }
    public override bool persistance(string eventTrace, TrackerEvent tE)
    {
        sessionIndex = GM.instance.getSession();
        bool result = false;
        string fullpath = Application.dataPath + folderName + sessionIndex + extension;
        if (!File.Exists(fullpath))
        {
            File.Create(fullpath).Close();
        }
        FileStream f = File.Open(fullpath, FileMode.Append);
        BinaryFormatter formater = new BinaryFormatter();
        formater.Serialize(f, tE);
        f.Close();
        return result;
    }
}
