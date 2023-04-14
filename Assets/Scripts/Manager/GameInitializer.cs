using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] private GameObject collectArea;
    [SerializeField] private GameObject ZoneArea;
    [SerializeField] private GameObject SpinnerArea;
    [SerializeField] private GameObject DeadArea;
    //[SerializeField] private InputHandler _inputHandler;
    //[SerializeField] private MatchObjectCreator _matchObjectCreator;
    private void Awake()
    {
        Instantiate(collectArea,parent);
        Instantiate(ZoneArea, parent);
        Instantiate(SpinnerArea, parent);
        Instantiate(DeadArea, parent);
        // _camera.DOColor(currentcolor, 1).SetEase(Ease.InSine).OnComplete(() => );
    }
}
