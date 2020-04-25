﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventLevelEnd : TrackerEvent
{
    // Evento invocado por LevelTracker

    protected int level;
    public EventLevelEnd(int nivel) : base(DateTime.Now, EventType.LEVEL_END)
    {
        this.level = nivel;
        session = GM.instance.getSession();
    }
    public string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + this.level.ToString();

    }
    public string SerializeToJson()
    {
        return base.SerializeToJson() + " Level: " + "\"" + this.level + "\"" + ",\n";
    }

}