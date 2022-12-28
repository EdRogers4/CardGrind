using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Button[] _playerCardButton;
    [SerializeField] private Image _highlightedBorder;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void SelectCard(int index)
    {
        _highlightedBorder.transform.position = _playerCardButton[index].transform.position;
    }
}
