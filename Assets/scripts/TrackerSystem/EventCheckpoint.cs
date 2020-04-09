using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventCheckpoint : TrackerEvent
{
    int nM;
    // Evento invocado por LevelTracker
    protected int idCheckpoint;
    public EventCheckpoint(int n) : base(DateTime.Now, 1, EventType.CHECKPOINT)
    {
        idCheckpoint = n;
    }
    //Añadir override???
    public string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + idCheckpoint.ToString();

    }
}
