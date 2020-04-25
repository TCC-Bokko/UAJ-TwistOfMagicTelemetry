﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventRestartLevel : TrackerEvent
{
   
    // Evento invocado por LevelTracker
    protected int nivel;
    public EventRestartLevel(int n) : base(DateTime.Now, EventType.LEVEL_RESTART)
    {       
        nivel = n;
        session = GM.instance.getSession();
    }
    //Añadir override???
    public string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + nivel.ToString();

    }
    public string SerializeToJson()
    {
        return base.SerializeToJson() + " Nivel: " + "\"" + nivel + "\"" + ",\n";
    }
}
