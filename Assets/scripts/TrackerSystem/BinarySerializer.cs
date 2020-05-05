using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
public class BinarySerializer : ISerializer
{
    string folderName = "/PersistentData/session_";
    int sessionIndex;
    string extension = ".dat";
    int iterPlace = 0;
    //Aqui tengo que cerrrar lo que viene a ser el archivo json
    // Update is called once per frame

    [Serializable]
    struct BinnaryEvent {
        public TrackerEvent.EventType type;
        public DateTime timeStamp;
        public long IDSesion;
        public int nivel;
        public int checkPoint;
        public double posX;
        public double posY;
    }
    public BinarySerializer()
    {

    }

    public override string serialize(TrackerEvent tE)
    {
        //string j = JsonUtility.ToJson(tE);
        ////////////////////
        //CAMBIAR
        //Solo pilla el del padre macho k tonto soy

        /////////////////       
        string j = tE.SerializeToJson() + "}" + "\n";
        Debug.Log(j);
        persistance(j, tE);
        return j;
    }
    public override bool persistance(string eventTrace, TrackerEvent tE)
    {
        sessionIndex = GM.instance.getSession();
        BinnaryEvent aux = new BinnaryEvent();
        aux.IDSesion = tE.getSession();
        aux.type = tE.getType();
        aux.timeStamp = tE.getTime();
        switch (tE.getType()) {
            case TrackerEvent.EventType.SESSION_START:
                aux.nivel = -1;
                aux.posX = 0;
                aux.posY = 0;
                aux.checkPoint = -1;
                break;
            case TrackerEvent.EventType.LEVEL_START:
                aux.nivel = (tE as EventLevelStart).getLevel();
                aux.posX = 0;
                aux.posY = 0;
                aux.checkPoint = -1;
                break;
            case TrackerEvent.EventType.LEVEL_END:
                aux.nivel = (tE as EventLevelStart).getLevel();
                aux.posX = 0;
                aux.posY = 0;
                aux.checkPoint = -1;
                break;
            case TrackerEvent.EventType.LEVEL_COMPLETED:
                aux.nivel = (tE as EventLevelStart).getLevel();
                aux.posX = 0;
                aux.posY = 0;
                aux.checkPoint = -1;
                break;
            case TrackerEvent.EventType.PLAYER_POSITION:
                aux.posX = (tE as EventPosition).getX();
                aux.posY = (tE as EventPosition).getY();
                aux.nivel = -1;
                break;
            case TrackerEvent.EventType.SESSION_END:
                aux.nivel = -1;
                aux.posX = 0;
                aux.posY = 0;
                aux.checkPoint = -1;
                break;
            case TrackerEvent.EventType.DEAD:
                aux.posX = (tE as EventPosition).getX();
                aux.posY = (tE as EventPosition).getY();
                aux.nivel = -1;
                aux.checkPoint = -1;
                break;
            case TrackerEvent.EventType.CHECKPOINT:
                aux.posX = 0;
                aux.posY = 0;
                aux.checkPoint = (tE as EventCheckpoint).getCheckPoint();
                aux.nivel = (tE as EventCheckpoint).getLevel();
                break;
            default:
                break;
        }
        bool result = false;
        string fullpath = Application.dataPath + folderName + sessionIndex + extension;
        if (!File.Exists(fullpath))
        {
            File.Create(fullpath).Close();
        }
        FileStream f = File.Open(fullpath, FileMode.Append);
        BinaryFormatter formater = new BinaryFormatter();
        formater.Serialize(f, aux);
        f.Close();
        return result;
    }
}
