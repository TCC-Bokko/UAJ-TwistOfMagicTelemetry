using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventSesionEnd : TrackerEvent
{
    // Evento invocado por GeneralTracker

    public EventSesionEnd() : base(DateTime.Now, EventType.SESSION_END)
    {
        session = GM.instance.getSession();
    }
    public override string SerializeToCSV()
    {
        return base.SerializeToCSV() ;

    }
    public override string SerializeToJson()
    {
        return base.SerializeToJson();
    }


}

