using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Player _scriptPlayer;
    [SerializeField] private Enemies _scriptEnemies;
    [SerializeField] private WeaponManager _scriptWeaponManager;

    [Header("Audio")] 
    private AudioSource _audioSource;

    [Header("Data")] 
    public int CurrentCharacter;
    
    [Header("Cards")]
    [SerializeField] private Button[] _playerCardButton;
    [SerializeField] private RectTransform[] _enemyCardButton;
    [SerializeField] private TextMeshProUGUI[] _textDamagePlayer;
    [SerializeField] private TextMeshProUGUI[] _textDamageEnemy;
    [SerializeField] private int[] _damagePlayer;
    [SerializeField] private int[] _damageEnemy;
    [SerializeField] private Image _highlightedBorder;
    private Color[] _highlightedColor = {new Color(0.99f, 0.48f, 0.54f), new Color(0.76f, 0.52f, 0.85f), new Color(0.41f, 0.88f, 0.50f)};
    private Color _maroon = new Color(0.51f, 0.37f, 0.41f);

    private void Start()
    {
        _audioSource = _scriptPlayer.gameObject.GetComponent<AudioSource>();
    }

    public void GeneratePlayerDamage()
    {
        _highlightedBorder.enabled = true;
        
        for (int i = 0; i < _damagePlayer.Length; i++)
        {
            if (_damagePlayer[i] <= 0)
            {
                _damagePlayer[i] = Random.Range(1, 4);
                _textDamagePlayer[i].text = "" + _damagePlayer[i];
            }
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
                    _scriptEnemies.CurrentEnemyCards[i] = _scriptEnemies.EnemyCard1[_scriptEnemies.CurrentEnemy];
                    break;
                case 1:
                    _damageEnemy[i] = _scriptEnemies.EnemyCard2[_scriptEnemies.CurrentEnemy];
                    _scriptEnemies.CurrentEnemyCards[i] = _scriptEnemies.EnemyCard2[_scriptEnemies.CurrentEnemy];
                    break;
                case 2:
                    _damageEnemy[i] = _scriptEnemies.EnemyCard1[_scriptEnemies.CurrentEnemy];
                    _scriptEnemies.CurrentEnemyCards[i] = _scriptEnemies.EnemyCard3[_scriptEnemies.CurrentEnemy];
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
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundDraw[_scriptWeaponManager.CurrentWeapon[index]]);
    }

    public void SelectEnemyCard(int index)
    {
        StartCoroutine(DamageEnemyCard(index));
    }

    private IEnumerator DamageEnemyCard(int index)
    {
        var damage = _damagePlayer[CurrentCharacter];
        var thisEnemy = index;
        var thisCharacter = CurrentCharacter;
        
        if (_damagePlayer[CurrentCharacter] > 0)
        {
            for (int i = 0; i < damage; i++)
            {
                _damagePlayer[thisCharacter] -= 1;
                _textDamagePlayer[thisCharacter].text = "" + _damagePlayer[thisCharacter];
                _audioSource.PlayOneShot(_scriptWeaponManager.SoundGunshot[_scriptWeaponManager.CurrentWeapon[thisCharacter]], 0.7F);
                _audioSource.PlayOneShot(_scriptEnemies.SoundHit[Random.Range(0, _scriptEnemies.SoundHit.Length)]);

                switch (thisEnemy)
                {
                    case 0:
                        _scriptWeaponManager.BulletHoleFadeIn(index, _scriptWeaponManager.BulletHoles1[Random.Range(0, _scriptWeaponManager.BulletHoles1.Count)]);
                        break;
                    case 1:
                        _scriptWeaponManager.BulletHoleFadeIn(index, _scriptWeaponManager.BulletHoles2[Random.Range(0, _scriptWeaponManager.BulletHoles2.Count)]);
                        break;
                    case 2:
                        _scriptWeaponManager.BulletHoleFadeIn(index, _scriptWeaponManager.BulletHoles3[Random.Range(0, _scriptWeaponManager.BulletHoles3.Count)]);
                        break;
                }

                if (_damageEnemy[thisEnemy] > 0)
                {
                    _damageEnemy[thisEnemy] -= 1;
                    _scriptEnemies.CurrentEnemyCards[thisEnemy] -= 1;
                    _textDamageEnemy[thisEnemy].text = "" + _damageEnemy[thisEnemy];
                
                    if (_scriptEnemies.CurrentEnemyCards[thisEnemy] <= 0)
                    {
                        _scriptEnemies.BloodFadeIn();
                    }
                }

                yield return new WaitForSeconds(0.2f);
            }
            
            //Resolve card elimination
            if (_scriptEnemies.CurrentEnemyCards[thisEnemy] <= 0)
            {
                StartCoroutine(EliminateCard(thisEnemy, thisCharacter));
            }

            if (_damagePlayer[0] == 0 && _damagePlayer[1] == 0 && _damagePlayer[2] == 0)
            {
                StartCoroutine(TogglePlayerAim());

                if (_damageEnemy[0] > 0)
                {
                    StartCoroutine(EnemyAttack(0));
                }
                
                if (_damageEnemy[1] > 0)
                {
                    StartCoroutine(EnemyAttack(1));
                }
                
                if (_damageEnemy[2] > 0)
                {
                    StartCoroutine(EnemyAttack(2));
                }
            }

            if (_damageEnemy[0] == 0 && _damageEnemy[1] == 0 && _damageEnemy[2] == 0)
            {
                StartCoroutine(_scriptEnemies.EnemyFadeOut());
                StartCoroutine(_scriptPlayer.StoryBlip());
                StartCoroutine(ResetEnemyCards());
            }
        }
    }

    private IEnumerator TogglePlayerAim()
    {
        yield return new WaitForSeconds(2.0f);
        _scriptPlayer.Aim();
    }

    private IEnumerator EnemyAttack(int index)
    {
        yield return new WaitForSeconds(0.5f);
        _scriptPlayer.BloodFadeIn(index);
        _scriptPlayer.PlayerHealth[index] -= _damageEnemy[index];
        _scriptPlayer.ReduceHealthBar(index);
        
    }

    private IEnumerator EliminateCard(int thisEnemy, int thisCharacter)
    {
        yield return new WaitForSeconds(0.5f);
        _enemyCardButton[thisEnemy].GetComponent<Image>().DOColor(Color.clear, 2);
        _textDamageEnemy[thisEnemy].DOFade(0, 2);
        StartCoroutine(_scriptPlayer.IncreaseExperience(thisCharacter, _scriptEnemies.EnemyCardExperience[_scriptEnemies.CurrentEnemy]));
    }

    private IEnumerator ResetEnemyCards()
    {
        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < _enemyCardButton.Length; i++)
        {
            _enemyCardButton[i].GetComponent<Image>().DOColor(_maroon, 0);
            _textDamageEnemy[i].DOFade(1, 0);
        }
    }
}
