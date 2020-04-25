using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVSerializer : ISerializer
{
    // Start is called before the first frame update



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
        return j;
    }
}
