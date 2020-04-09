using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventIdleMana1 : TrackerEvent
{
    // Evento invocado por PlayerTracker

        //Creo que con esta creo que no es necesaior 4 clases   
        //Habria que pensar otra manera
    public EventIdleMana1() : base(DateTime.Now, 1, EventType.IDLE_MANA1)
    {


    }




}
