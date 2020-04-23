﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPersistence : MonoBehaviour
{
    //List of events
    private List<TrackerEvent> eventList;
    private ISerializer serializeObject;
    // Start is called before the first frame update
    void Start()
    {
        //Establecer aqui que tipo de serializador usar
        serializeObject = new JSONSerializer();
    }

    // Update is called once per frame
    void Update()
    {
        //Ver en que momento mandar a serializar
        //ver si serializar evento a evento o toda la lista de eventos
        //adaptar el metodo serialize del serializeObject para recibir evento o lista de eventos
        /*
        foreach (var t_event in eventList) { 
            serializeObject.serialize(t_event);
        }
        */
        // o bien...
        /*
        serializeObject.serialize(eventList);
        */

        //Esto dentro de la condición de envio de datos
    }

    public void send(List<TrackerEvent> list) {
        foreach (var t_event in list){
            eventList.Add(t_event);
        }
    }
    

}
