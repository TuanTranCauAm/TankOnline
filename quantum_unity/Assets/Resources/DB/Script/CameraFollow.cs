using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public class CameraFollow : MonoBehaviour
{
    private bool follow;
    private EntityRef mainEntity;
    private Transform mainTransform;
    void Start()
    {
        QuantumEvent.Subscribe<EventDeath>(this, handleDeath);
        QuantumEvent.Subscribe<EventAlive>(this, handleRespawn);
        follow = false;
        mainEntity = EntityRef.None;
    }

    private void handleDeath(EventDeath a)
    {
        follow = false;
    }

    private void handleRespawn(EventAlive a)
    {
        follow = true;
        if (mainEntity == EntityRef.None)
        {
            mainEntity = a.entity;
            //mainTransform = mainEntity.GameObject.GetComponent<Transform>();
        }

    }

    void Update()
    {
        if (!follow)
            return;
        
    }
}
