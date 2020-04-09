using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventLevelEnd : TrackerEvent
{
    // Evento invocado por LevelTracker

    protected int level;
    public EventLevelEnd(int nivel) : base(DateTime.Now, 1, EventType.LEVEL_START)
    {
        this.level = nivel;
    }

}
