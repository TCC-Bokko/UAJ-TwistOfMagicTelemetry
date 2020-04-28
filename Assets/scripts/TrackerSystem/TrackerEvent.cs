using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class TrackerEvent {
    // Clase Abstracta
    // Define variables y métodos compartidos por todos los eventos.
    // Se utiliza como clase padre para implementar todos los eventos descritos en el documento de diseño. 
    public enum EventType
    {
        SESSION_START,
        SESSION_END,
        LEVEL_START,
        LEVEL_END,
        LEVEL_COMPLETED,
        LEVEL_RESTART,
        PLAYER_POSITION,
        DEAD,
        CHECKPOINT,
        IDLE_MANA1,
        IDLE_MANA2,
        IDLE_MANA3,
        IDLE_MANA4
    }
    public enum causeOfDeath
    {
        BY_MANA,
        NORMAL//Por Enemigo, por aplastamiento, por ahogamiento
    }
   
    protected DateTime time;
    protected long session;
    protected EventType eventType;

    public TrackerEvent() { }//EVITAR ERRORS

     //Todos los eventos tienen esto en comun
    public TrackerEvent(DateTime time_, EventType evenType_)
    {
        this.time = time_;
       
        this.eventType = evenType_;
    }
    
    public void setSession(int s)
    {
        this.session = s;
    }
    public virtual string SerializeToCSV()
    {
        return time.ToString() + "," + session.ToString() + "," + eventType.ToString();
    }
    public virtual string SerializeToJson()
    {
        return "User: { \n" + " IDSesion: " + "\"" + this.session + "\"" + ",\n" +
             " Time: " + "\"" + time.ToString() + "\"" + ",\n" +
             " Event: " + "\"" +eventType.ToString()+ "\"" + ",\n";
    }
    //serialize to csv & to json or xml or something


    public EventType getType() {
        return this.eventType;
    }

}