using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventIdleMana1 : TrackerEvent
{
    float t;
    public EventIdleMana1(float time) : base(DateTime.Now, EventType.IDLE_MANA1)
    {
        t = time;
        session = GM.instance.getSession();
    }
    public override string SerializeToCSV()
    {
        return base.SerializeToCSV() + ", " + t.ToString();

    }
    public override string SerializeToJson()
    {
        return base.SerializeToJson() + " Tiempo de recarga: " + "\"" + t + "\"" + ",\n";
    }



}
