using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventLevelCompleted : TrackerEvent
{
    // Evento invocado por LevelTracker


        //sIMILAR AL Level ENd
    protected int level;
    public EventLevelCompleted(int nivel) : base(DateTime.Now, EventType.LEVEL_COMPLETED)
    {
        this.level = nivel;
        session = GM.instance.getSession();
    }

    public override string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + level.ToString();

    }
    public override string SerializeToJson()
    {
        return base.SerializeToJson() + " Level: " + "\"" + this.level + "\"" + ",\n";
    }
}
