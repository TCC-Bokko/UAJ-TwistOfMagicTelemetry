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
    public override string SerializeToCSV()
    {
        return base.SerializeToCSV() + ",\n";

    }
    public override string SerializeToJson()
    {
        return base.SerializeToJson() + "\""+ "\n";
    }



}


