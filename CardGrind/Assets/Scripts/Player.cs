using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [Header("Scripts")] 
    [SerializeField] private Enemies _scriptEnemies;
    [SerializeField] private CardManager _scriptCardManager;
    [SerializeField] private WeaponManager _scriptWeaponManager;
    
    [Header("UI")] 
    [SerializeField] private GameObject _buttonFight;
    [SerializeField] private Button _buttonTravel;
    [SerializeField] private Image[] _blood1;
    [SerializeField] private Image[] _blood2;
    [SerializeField] private Image[] _blood3;
    [SerializeField] private Image[] _imageBlot;
    [SerializeField] private Image[] _imageX;

    [Header("Audio")] 
    private AudioSource _audioSource;

    [Header("Health")] 
    public int[] PlayerHealth;
    [SerializeField] private int[] _playerHealthMax;
    [SerializeField] private RectTransform[] _healthBar;
    [SerializeField] private TextMeshProUGUI[] _textHealth;
    private int _widthHealthBar = 110;
    private float _fillAmount;

    [Header("Experience")] 
    public int[] PlayerLevel;
    [SerializeField] private int[] _playerExperience;
    [SerializeField] private int[] _toNextLevel;
    [SerializeField] private RectTransform[] _experienceBar;
    [SerializeField] private TextMeshProUGUI[] _textLevel;

    [Header("Story")] 
    [SerializeField] private Animator _animatorStoryPanel;
    [SerializeField] private TextMeshProUGUI _textStory;
    [SerializeField] private string[] _storyBlipsForest;
    [SerializeField] private float _speedAnimateText;
    private int _currentStoryBlip;

    [Header("Guns")]
    [SerializeField] private Button[] _buttonEquip;

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();

        for (int i = 0; i < PlayerHealth.Length; i++)
        {
            PlayerHealth[i] = _playerHealthMax[i];
            _textHealth[i].text = "" + _playerHealthMax[i];
        }
    }

    public void Fight()
    {
        _animatorStoryPanel.SetBool("isOn", false);
        _buttonTravel.interactable = false;
        _scriptEnemies.EnemyFadeIn();
        _scriptCardManager.GenerateEnemyDamage();
        _scriptCardManager.GeneratePlayerDamage();
        _buttonFight.GetComponent<Button>().interactable = false;
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundReload[0], 1);
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundReload[1], 1);
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundReload[2], 1);

        for (int i = 0; i < _buttonEquip.Length; i++)
        {
            _buttonEquip[i].interactable = false;
        }
    }

    public void Aim()
    {
        _scriptCardManager.GeneratePlayerDamage();
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundDraw[0], 1);
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundDraw[1], 1);
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundDraw[2], 1);
    }

    public void PlayStory()
    {
        _animatorStoryPanel.SetBool("isOn", true);
        StartCoroutine(AnimateStoryText());
    }

    public void ClearStory()
    {
        _textStory.text = "";
        StopCoroutine(AnimateStoryText());
    }

    private IEnumerator AnimateStoryText()
    {
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < _storyBlipsForest[_currentStoryBlip].Length; i++)
        {
            _textStory.text += _storyBlipsForest[_currentStoryBlip][i];
            yield return new WaitForSeconds(0.00001f);
        }
    }

    public void ReduceHealthBar(int index)
    {
        if (PlayerHealth[index] < 0)
        {
            PlayerHealth[index] = 0;
            _imageBlot[index].DOColor(Color.black, 1.5f);
            _imageX[index].DOColor(Color.white, 1.5f);
        }

        _textHealth[index].text = "" + PlayerHealth[index];
        var barWidth = _widthHealthBar / _playerHealthMax[index];
        barWidth = barWidth * PlayerHealth[index];
        _healthBar[index].DOSizeDelta(new Vector2(barWidth, _healthBar[index].sizeDelta.y), 0.2f);
    }

    public IEnumerator IncreaseExperience(int index, int amount)
    {
        _playerExperience[index] += amount;
        var thisCharacter = index;

        if (_playerExperience[index] >= _toNextLevel[PlayerLevel[index]])
        {
            _playerExperience[index] = _playerExperience[index] - _toNextLevel[PlayerLevel[index]];
            PlayerLevel[index] += 1;
            _textLevel[index].text = "" + (PlayerLevel[index] + 1);
            var maxWidth = new Vector2(110f, 10.0f);
            _experienceBar[index].DOSizeDelta(maxWidth, 0.5f);
            NewLevel(index);
            yield return new WaitForSeconds(0.5f);
        }
        
        _fillAmount = (float)_playerExperience[thisCharacter] / (float)_toNextLevel[PlayerLevel[thisCharacter]];
        var newX = 110f * _fillAmount;
        var newWidth = new Vector2(newX, 10.0f);
        _experienceBar[index].DOSizeDelta(newWidth, 1.0f);
    }

    public void NewLevel(int index)
    {
        _playerHealthMax[index] += 1;
        PlayerHealth[index] = _playerHealthMax[index];
        _textHealth[index].text = "" + _playerHealthMax[index];
        _healthBar[index].DOSizeDelta(new Vector2(110, _healthBar[index].sizeDelta.y), 0.2f);
    }
    
    public void BloodFadeIn(int character)
    {
        var bloodIndex = Random.Range(0, 3);
        _audioSource.PlayOneShot(_scriptEnemies.SoundAttack[Random.Range(0, _scriptEnemies.SoundAttack.Length)]);

        switch (character)
        {
            case 0:
                _blood1[bloodIndex].DOColor(Color.white, 0.2f);
                StartCoroutine(BloodFadeOut(_blood1[bloodIndex]));
                break;
            case 1:
                _blood2[bloodIndex].DOColor(Color.white, 0.2f);
                StartCoroutine(BloodFadeOut(_blood2[bloodIndex]));
                break;
            case 2:
                _blood3[bloodIndex].DOColor(Color.white, 0.2f);
                StartCoroutine(BloodFadeOut(_blood3[bloodIndex]));
                break;
        }
    }

    private IEnumerator BloodFadeOut(Image blood)
    {
        yield return new WaitForSeconds(1.5f);
        blood.DOColor(Color.clear, 0.5f);
    }

    public IEnumerator StoryBlip()
    {
        _currentStoryBlip += 1;
        _textStory.text = "";
        yield return new WaitForSeconds(3.0f);
        _animatorStoryPanel.SetBool("isOn", true);
        _buttonFight.GetComponent<Button>().interactable = true;
        _buttonTravel.interactable = true;

        for (int i = 0; i < _buttonEquip.Length; i++)
        {
            _buttonEquip[i].interactable = true;
        }
    }

}
