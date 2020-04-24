using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventPlayerDead : TrackerEvent
{
    // Evento invocado por PlayerTracker
    protected float posX;
    protected float posY;
    protected causeOfDeath causeOf;
    public EventPlayerDead(float x,float y, causeOfDeath cause): base(DateTime.Now, 1, EventType.SESSION_START)
    {
        posX = x;
        posY = y;
        causeOf = cause;
    }
    //Ovverride?
    public string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + posX.ToString() + "," + posY.ToString() + "," + causeOf.ToString();

    }
    public string SerializeToJson()
    {
        return base.SerializeToJson() + " PosX: " + "\"" + posX + "\"" + ",\n" +
            " PosY: " + "\"" + posY + "\"" + ",\n" +
            " CauseOfDeath: " + "\"" + causeOf.ToString() + "\"" + ",\n";
    }


}
