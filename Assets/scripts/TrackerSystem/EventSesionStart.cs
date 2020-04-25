using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class EventSesionStart : TrackerEvent
{                                                
    public EventSesionStart() : base(DateTime.Now, EventType.SESSION_START)
    {
        session = GM.instance.getSession();
    }
    public string SerializeToCSV()
    {
        return base.SerializeToCSV() + ",\n";

    }
    public string SerializeToJson()
    {
        return base.SerializeToJson() + "\""+ "\n";
    }



}


