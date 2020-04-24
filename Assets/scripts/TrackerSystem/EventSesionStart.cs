using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class EventSesionStart : TrackerEvent
{                                                //No se como llamar a session
    public EventSesionStart() :base(DateTime.Now, 1, EventType.SESSION_START)
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


