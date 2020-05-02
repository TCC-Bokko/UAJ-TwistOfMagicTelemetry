using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class EventSesionStart : TrackerEvent
{                                                
    public EventSesionStart() : base(DateTime.Now, EventType.SESSION_START)
    {
        session = GM.instance.getSession();
        //session = GM.getSession()
    }
    public override string SerializeToCSV()
    {
        return base.SerializeToCSV();

    }
    public override string SerializeToJson()
    {
        return base.SerializeToJson();
    }



}


