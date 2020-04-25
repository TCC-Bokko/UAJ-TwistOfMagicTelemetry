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
    public EventPlayerDead(float x,float y): base(DateTime.Now, EventType.DEAD)
    {
        posX = x;
        posY = y;
        session = GM.instance.getSession();
        //causeOf = cause;
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
