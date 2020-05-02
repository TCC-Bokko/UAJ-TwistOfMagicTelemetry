using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tracker {
    // VARIABLES ///////////////////////////////////////////////////////////////////////
    // Proceso del singleton
    private static Tracker instance = null;

    // Objetos de persistencia
    private IPersistence persistenceObject; // IPersistence se comunica con ISerializer

    // Lista de trackers activos (progression, resource, etc.)
    private List<ITrackerAsset> activeTrackers;
    //Aqui dentro irán:
    //Tracker trackerNivel
    //Tracker trackerJugador
    //Tracker trackerGeneral

    // Lista de eventos
    // en esta implementación pasamos todos los eventos validos a la misma lista
    // sin embargo sería muy facil dividirlos por cada tracker (general, jugador y nivel) 
    // en el momento de validarlos.
    private List<TrackerEvent> eventList;

    Tracker() {
        activeTrackers = new List<ITrackerAsset>();
        eventList = new List<TrackerEvent>();

        // Enlazado de otras clases segun tipo
        persistenceObject = new IPersistence();
        // Llenar lista de trackers
        ITrackerAsset trackerNivel = new LevelTracker();
        activeTrackers.Add(trackerNivel);
        ITrackerAsset trackerJugador = new PlayerTracker();
        activeTrackers.Add(trackerJugador);
        ITrackerAsset trackerGeneral = new GeneralTracker();
        activeTrackers.Add(trackerGeneral);
    }

    public static Tracker getInstance() {
        if (instance == null) {
            instance = new Tracker();
        }
        return instance;
    }

    // METODOS ///////////////////////////////////////////////////////////////////////
    //Para que no se destruya entre escenas
    void Awake() {
        //DontDestroyOnLoad(transform.gameObject);
    }

    // Init con instanciado
    void Start() { 
        
    }

    public void TrackEvent(TrackerEvent t_event) {
        bool accepted = false;
        foreach (var tracker in activeTrackers) {
            if (tracker.checkValidity(t_event))
                accepted = true;
        }

        if (accepted) {
            eventList.Add(t_event);
        }
    }

    // Update is called once per frame
    public void Update() {
        //Cada X tiempo o cantidad de eventos...
        if (eventList.Count >= 1) {
            persistenceObject.send(eventList);
            eventList.Clear();
            persistenceObject.Update();
        }
    }
    public IPersistence GetPersistence() {
        return persistenceObject;
    }
}
