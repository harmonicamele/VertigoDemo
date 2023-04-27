using System;
using System.Collections;
using System.Collections.Generic;
using Game.EventSystem;
using UnityEngine;
using UnityEngine.UI;

public class DeadArea : MonoBehaviour
{
    [SerializeField] private Button Giveup, Revive;
    [SerializeField] private GameObject DeadPanel;

    private EventBus eventBus;
    private void Awake()
    {
        eventBus = EventBus.Instance;
    }
    private void OnEnable()
    {
        Giveup.onClick.AddListener(GiveUpAll);
        Revive.onClick.AddListener(KeepIt);
        eventBus.Subscribe<GameEvents.Bomb>(Bomb);
    }
    private void OnDisable()
    {
        Giveup.onClick.RemoveListener(GiveUpAll);
        Revive.onClick.RemoveListener(KeepIt);
        eventBus.Unsubscribe<GameEvents.Bomb>(Bomb);
    }


    private void Bomb()
    {
        PanelActive(true);
    }

    private void KeepIt()
    {
        PanelActive(false);
    }
    public void PanelActive(bool active)
    {
        DeadPanel.SetActive(active);
    }
    private void GiveUpAll()
    {
        eventBus.Fire(new GameEvents.GiveUp());
        PanelActive(false);
    }

   
}
