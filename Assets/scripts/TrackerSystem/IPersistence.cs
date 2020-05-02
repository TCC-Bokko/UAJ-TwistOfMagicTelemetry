﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPersistence {
    //List of events
    private CircularBuffer<TrackerEvent> eventList;
    private int queueSize;
    private ISerializer serializeObject;
    private bool serializeType = false; // Cambiar a int en caso de agregar mas serializadores
    public IPersistence() {
        //Tamaño máximo de cola circular
        queueSize = 100;
        //Establecer aqui que tipo de serializador usar
        eventList = new CircularBuffer<TrackerEvent>(queueSize);
        if (!serializeType)
        {
            serializeObject = new JSONSerializer();
        }
        else
        {
            serializeObject = new CSVSerializer();
        }
        //serializeObject = new CSVSerializer();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    public void Update()
    {
        //Ver en que momento mandar a serializar
        //ver si serializar evento a evento o toda la lista de eventos
        //adaptar el metodo serialize del serializeObject para recibir evento o lista de eventos

        if (eventList.Count() > 0)
        {
            serializeObject.serialize(eventList.Read());
        }

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

public class CircularBuffer<T>
{
    Queue<T> _queue;
    int _size;
    int _elementCount;

    public CircularBuffer(int size)
    {
        _queue = new Queue<T>(size);
        _size = size;
    }

    public void Add(T obj)
    {
        if (_queue.Count == _size)
        {
            _queue.Dequeue();
            _queue.Enqueue(obj);
        }
        else
            _queue.Enqueue(obj);
    }

    public int Count()
    {
        return _queue.Count;
    }

    public T Read()
    {
        return _queue.Dequeue();
    }

    public T Peek()
    {
        return _queue.Peek();
    }
}