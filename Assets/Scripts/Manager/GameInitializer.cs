using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Core;
using UnityEngine;
namespace Game.Manager
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private Transform Parent;
        [SerializeField] private GameObject CollectArea;
        [SerializeField] private GameObject ZoneArea;
        [SerializeField] private GameObject SpinnerArea;
        [SerializeField] private GameObject DeadArea;
        private void Awake()
        {
            SpriteManager.Instance.Initialize();
            Instantiate(CollectArea, Parent);
            Instantiate(ZoneArea, Parent);
            Instantiate(SpinnerArea, Parent);
            Instantiate(DeadArea, Parent);
        }
    }

}
