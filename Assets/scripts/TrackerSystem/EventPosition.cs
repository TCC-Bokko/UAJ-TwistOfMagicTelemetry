using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventPosition : TrackerEvent
{
    // Evento invocado por PlayerTracker
    protected float posX;
    protected float posY;
   
    public EventPosition(float x, float y) : base(DateTime.Now, EventType.PLAYER_POSITION)
    {
        posX = x;
        posY = y;
        session = GM.instance.getSession();
        //causeOf = cause;
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
