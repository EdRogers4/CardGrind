using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Button[] _playerCardButton;
    [SerializeField] private TextMeshProUGUI[] _textDamagePlayer;
    [SerializeField] private TextMeshProUGUI[] _textDamageEnemy;
    [SerializeField] private int[] _damagePlayer;
    [SerializeField] private int[] _damageEnemy;
    
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
