using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventLevelStart : TrackerEvent
{
    // Evento invocado por LevelTracker

    protected int level;
    public EventLevelStart(int nivel):base(DateTime.Now, EventType.LEVEL_START){
        this.level = nivel;
        session = GM.instance.getSession();
    }
    public override string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + this.level.ToString(); ;

    }
    public override string SerializeToJson()
    {
        return base.SerializeToJson() + " Level: " + "\"" + this.level + "\"" + ",\n";
    }
    public int getLevel() { return level; }
}
