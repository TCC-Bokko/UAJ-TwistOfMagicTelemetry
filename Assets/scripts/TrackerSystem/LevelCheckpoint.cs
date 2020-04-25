using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCheckpoint : MonoBehaviour
{
    public GameObject jugador;
    bool triggered;
    public int checkPoint_num;

    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.name == jugador.name && !triggered)
        {
            TrackerEvent checkpoint = new EventCheckpoint(checkPoint_num,GM.instance.numeroNivel);
            GM.TrackerInstance.TrackEvent(checkpoint);
            triggered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
