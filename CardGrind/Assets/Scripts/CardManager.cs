using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Player _scriptPlayer;
    [SerializeField] private Enemies _scriptEnemies;
    [SerializeField] private WeaponManager _scriptWeaponManager;

    [Header("Data")] 
    public int CurrentCharacter;
    
    [Header("Player Cards")]
    [SerializeField] private Button[] _playerCardButton;
    [SerializeField] private TextMeshProUGUI[] _textDamagePlayer;
    [SerializeField] private TextMeshProUGUI[] _textDamageEnemy;
    [SerializeField] private int[] _damagePlayer;
    [SerializeField] private int[] _damageEnemy;
    [SerializeField] private Image _highlightedBorder;
    private Color[] _highlightedColor = {new Color(0.99f, 0.48f, 0.54f), new Color(0.76f, 0.52f, 0.85f), new Color(0.41f, 0.88f, 0.50f)};

    public void GeneratePlayerDamage()
    {
        _highlightedBorder.enabled = true;
        
        for (int i = 0; i < _damagePlayer.Length; i++)
        {
            _damagePlayer[i] = Random.Range(1, 4);
            _textDamagePlayer[i].text = "" + _damagePlayer[i];
        }
    }

    public void GenerateEnemyDamage()
    {
        for (int i = 0; i < _damageEnemy.Length; i++)
        {
            switch (i)
            {
                case 0:
                    _damageEnemy[i] = _scriptEnemies.EnemyCard1[_scriptEnemies.CurrentEnemy];
                    break;
                case 1:
                    _damageEnemy[i] = _scriptEnemies.EnemyCard2[_scriptEnemies.CurrentEnemy];
                    break;
                case 2:
                    _damageEnemy[i] = _scriptEnemies.EnemyCard1[_scriptEnemies.CurrentEnemy];
                    break;
            }
            
            _textDamageEnemy[i].text = "" + _damageEnemy[i];
        }
    }

    public void SelectPlayerCard(int index)
    {
        CurrentCharacter = index;
        _highlightedBorder.transform.position = _playerCardButton[index].transform.position;
        _highlightedBorder.color = _highlightedColor[index];
    }

    public void SelectEnemyCard(int index)
    {
        StartCoroutine(DamageEnemyCard(index));
    }

    private IEnumerator DamageEnemyCard(int index)
    {
        var damage = _damagePlayer[CurrentCharacter];
        
        if (_damagePlayer[CurrentCharacter] > 0)
        {
            for (int i = 0; i < damage; i++)
            {
                _damagePlayer[CurrentCharacter] -= 1;
                _damageEnemy[index] -= 1;
                _textDamagePlayer[CurrentCharacter].text = "" + _damagePlayer[CurrentCharacter];
                _textDamageEnemy[index].text = "" + _damageEnemy[index];
                yield return new WaitForSeconds(0.2f);
            }

            if (_damagePlayer[0] == 0 && _damagePlayer[1] == 0 && _damagePlayer[2] == 0)
            {
                _scriptPlayer.ToggleReloadButtonOn();
            }
        }
    }
}
