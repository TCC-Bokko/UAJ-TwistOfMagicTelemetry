using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventLevelStart : TrackerEvent
{
    // Evento invocado por LevelTracker

    protected int level;
    public EventLevelStart(int nivel):base(DateTime.Now, 1, EventType.LEVEL_START){
        this.level = nivel;
    }
    public string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + this.level.ToString(); ;

    }

}
