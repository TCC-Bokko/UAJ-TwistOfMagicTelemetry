using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVSerializer : ISerializer
{
    // Start is called before the first frame update


    private string extension = ".csv";
    private string filename = "session_";
    private string folderName = "/PersistentData/";
    private int sessionID = 0;
    //Aqui tengo que cerrrar lo que viene a ser el archivo json
    // Update is called once per frame

    public CSVSerializer()
    {

    }
   
    public override string serialize(TrackerEvent tE)
    {
        //string j = JsonUtility.ToJson(tE);
        string j = tE.SerializeToCSV() + "\n";
        Debug.Log(j);
        persistance(j, tE);
        return j;
    }
    public override bool persistance(string eventTrace, TrackerEvent tE)
    {
        sessionID = GM.instance.getSession();
        bool result = false;
        string fullpath = Application.dataPath + folderName +filename+ sessionID + extension;
        if (!File.Exists(fullpath))
        {
            File.Create(fullpath).Close();
        }
        using (StreamWriter w = File.AppendText(fullpath))
        {
            w.Write(eventTrace);
            result = true;
        }
        return result;
    }
}
