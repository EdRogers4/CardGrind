using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Stats")] 
    public string CurrentArea;
    
    [Header("Scripts")] 
    [SerializeField] private Enemies _scriptEnemies;
    [SerializeField] private CardManager _scriptCardManager;
    [SerializeField] private WeaponManager _scriptWeaponManager;
    
    [Header("UI")] 
    [SerializeField] private GameObject _buttonFight;
    [SerializeField] private GameObject _buttonReload;

    [Header("Audio")] 
    private AudioSource _audioSource;

    [Header("Weapons")] 
    [SerializeField] private int[] _currentWeapon;
    
    [Header("Story")] 
    [SerializeField] private Animator _animatorStoryPanel;
    [SerializeField] private TextMeshProUGUI _textStory;
    [SerializeField] private string[] _storyBlipsForest;
    [SerializeField] private float _speedAnimateText;
    private int _currentStoryBlip;

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _animatorStoryPanel.SetBool("isOn", true);
        StartCoroutine(AnimateStoryText());
    }

    public void Fight()
    {
        _animatorStoryPanel.SetBool("isOn", false);
        _scriptEnemies.EnemyFadeIn();
        _scriptCardManager.GenerateEnemyDamage();
        _scriptCardManager.GeneratePlayerDamage();
        _buttonFight.GetComponent<Button>().interactable = false;
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundReload[0], 1);
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundReload[1], 1);
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundReload[2], 1);
    }

    public void Aim()
    {
        _scriptCardManager.GeneratePlayerDamage();
        _buttonReload.GetComponent<Button>().interactable = false;
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundDraw[0], 1);
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundDraw[1], 1);
        _audioSource.PlayOneShot(_scriptWeaponManager.SoundDraw[2], 1);
    }

    public void ToggleReloadButtonOn()
    {
        _buttonFight.SetActive(false);
        _buttonReload.SetActive(true);
        _buttonReload.GetComponent<Button>().interactable = true;
        
    }

    private IEnumerator AnimateStoryText()
    {
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < _storyBlipsForest[_currentStoryBlip].Length; i++)
        {
            _textStory.text += _storyBlipsForest[_currentStoryBlip][i];
            yield return new WaitForSeconds(_speedAnimateText);
        }
    }
}
