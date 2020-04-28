using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventPlayerDead : TrackerEvent
{
    // Evento invocado por PlayerTracker
    protected float posX;
    protected float posY;
    public EventPlayerDead(float x, float y) : base(DateTime.Now, EventType.DEAD)
    {
        posX = x;
        posY = y;
        session = GM.instance.getSession();
    }

    public override string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + posX.ToString() + "," + posY.ToString() ;

    }
    public override string SerializeToJson()
    {
        return base.SerializeToJson() + " PosX: " + "\"" + posX + "\"" + ",\n" +
            " PosY: " + "\"" + posY + "\"" + ",\n" ;
    }
}