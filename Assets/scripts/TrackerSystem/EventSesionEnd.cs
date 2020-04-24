using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventSesionEnd : TrackerEvent
{
    // Evento invocado por GeneralTracker

    public EventSesionEnd() : base(DateTime.Now, 1, EventType.SESSION_END)
    {


    }
    public string SerializeToCSV()
    {
        return base.SerializeToCSV() + ",\n";

    }
    public string SerializeToJson()
    {
        return base.SerializeToJson() + ",\n";
    }


}

