using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventCheckpoint : TrackerEvent
{
    
    // Evento invocado por LevelTracker
    protected int idCheckpoint;
    public EventCheckpoint(int n) : base(DateTime.Now, EventType.CHECKPOINT)
    {
        session = GM.instance.getSession();
        idCheckpoint = n;
    }
    //Añadir override???
    public string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + idCheckpoint.ToString();

    }
    public  string SerializeToJson()
    {
        return  base.SerializeToJson() + " IdCheckPoint: " + "\"" + idCheckpoint + "\"" + ",\n";
    }
}
