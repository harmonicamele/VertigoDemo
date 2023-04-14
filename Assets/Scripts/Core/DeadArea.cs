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
    private void Awake()
    {
       
    }
    private void OnEnable()
    {
        Giveup.onClick.AddListener(GiveUpAll);
        Revive.onClick.AddListener(KeepIt);
        EventBus.Bomb += Bomb;
    }
    private void OnDisable()
    {
        EventBus.Bomb -= Bomb;
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
        EventBus.GiveUp?.Invoke();
        PanelActive(false);
    }

   
}
