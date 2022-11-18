using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;
using System;
using UnityEngine.UI;

public class QuantumDeadScreen : QuantumCallbacks
{
    [SerializeField] private GameObject deadScene;
    [SerializeField] private ParticleSystem m_ExplosionAnimation;
    [SerializeField] private EntityView entityView;
    [SerializeField] private Slider health;
    [SerializeField] private GameObject healthUI;
    [SerializeField] private Image curHealthUI;
    [SerializeField] private float mainPlayerHealthColor;
    [SerializeField] private Text curPoint;

    private void Start()
    {
        QuantumEvent.Subscribe<EventDeath>(this, handleDeath);
        QuantumEvent.Subscribe<EventAlive>(this, handleRespawn);
        QuantumEvent.Subscribe<EventUpdateHealth>(this, handleUpdateHeath);
        QuantumEvent.Subscribe<EventUpdateColorHealth>(this, handleUpdateColorHealth);
        QuantumEvent.Subscribe<EventUpdatePoint>(this, handleUpdatePoint);
        QuantumEvent.Subscribe<EventShellExplored>(this, handle7);
        deadScene.SetActive(false);
    }

    private void handleDeath(EventDeath a)
    {
        deadScene.SetActive(true);
        healthUI.SetActive(false);
    }

    private void handleRespawn(EventAlive a)
    {
        deadScene.SetActive(false);
        healthUI.SetActive(true);
    }

    private void handleUpdateHeath(EventUpdateHealth a)
    {
        if (entityView.EntityRef != a.entity)
            return;

        health.value = (float)(a.health / a.maxHealth);
    }

    private void handleUpdateColorHealth(EventUpdateColorHealth a)
    {
        if (entityView.EntityRef != a.entity)
            return;

        curHealthUI.color = new Color(0, 255, 0, 255);
    }

    private void handleUpdatePoint(EventUpdatePoint a)
    {
        Debug.Log("Cập nhật điểm");
        if (entityView.EntityRef != a.entity)
            return;

        int curP = Int32.Parse(curPoint.text);
        curPoint.text = (curP++).ToString();
    }

    private void handle7(EventShellExplored a)
    {
        var x = Instantiate(m_ExplosionAnimation);
        x.transform.position = a.exploredPosition.ToUnityVector3();
        x.Play();
    }
}
