﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventLevelCompleted : TrackerEvent
{
    // Evento invocado por LevelTracker


        //sIMILAR AL Level ENd
    protected int level;
    public EventLevelCompleted(int nivel) : base(DateTime.Now, 1, EventType.LEVEL_COMPLETED)
    {
        this.level = nivel;
    }

}
