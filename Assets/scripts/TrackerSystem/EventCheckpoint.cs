using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventCheckpoint : TrackerEvent
{
    
    // Evento invocado por LevelTracker
    protected int idCheckpoint;
    protected int level;
    public EventCheckpoint(int n,int nivel) : base(DateTime.Now, EventType.CHECKPOINT)
    {
        level = nivel;
        session = GM.instance.getSession();
        idCheckpoint = n;
    }
    
    public override string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + idCheckpoint.ToString() + ","+ level.ToString();

    }
    public override string SerializeToJson()
    {
        return  base.SerializeToJson() + " IdCheckPoint: " + "\"" + idCheckpoint + "\"" + 
            "NºNivel: "+ "\"" + level+ "\"" + ",\n";
    }
}
