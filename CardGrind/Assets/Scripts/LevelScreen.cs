using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreen : MonoBehaviour
{
    [Header("Area")]
    public int CurrentArea;
    public int CurrentFrame;
    [SerializeField] private Image _environment;
    [SerializeField] private AudioClip[] _areaTrack;
    private AudioSource _audioSource;

    [Header("Scripts")]
    [SerializeField] private Player _scriptPlayer;

    [Header("Level Select")]
    [SerializeField] private Image[] _levelIcon;
    [SerializeField] private Image _fadeOutScreen;
    [SerializeField] private Image _background;
    [SerializeField] private RectTransform _levelSelector;
    [SerializeField] private GameObject _levelScreenElements;
    [SerializeField] private float _speedTransitionScreen;
    private Color _colorNotSelected = new Color(1f, 1f, 1f, 0.44f);
    private Color _colorBackground = new Color(0.14f, 0.14f, 0.14f, 1.0f);

    [Header("Level Frames")]
    [SerializeField] private Sprite[] _frameForest;
    [SerializeField] private Sprite[] _frameCity;
    [SerializeField] private Sprite[] _frameFactory;
    [SerializeField] private Sprite[] _frameLake;


    private void Start()
    {
        _fadeOutScreen.DOColor(Color.clear, 3.5f);
        _audioSource = gameObject.GetComponent<AudioSource>();
        MusicFadeIn();
    }

    public void SelectLevel(int index)
    {
        CurrentArea = index;
        _levelSelector.transform.position = _levelIcon[index].transform.position;
        _levelIcon[index].color = Color.white;

        for (int i = 0; i < _levelIcon.Length; i++)
        {
            if (i != index)
            {
                _levelIcon[i].color = _colorNotSelected;
            }
        }
    }

    public void ButtonGo()
    {
        _fadeOutScreen.DOColor(Color.black, _speedTransitionScreen);
        EnvironmentFrame();
        StartCoroutine(DelayPlayStory());
        MusicFadeOut();
    }
    
    private IEnumerator DelayPlayStory()
    {
        yield return new WaitForSeconds(0.5f);
        MusicFadeIn();
        _audioSource.clip = _areaTrack[CurrentArea + 1];
        _audioSource.Play();
        yield return new WaitForSeconds(_speedTransitionScreen);
        _levelScreenElements.SetActive(false);
        _background.DOColor(Color.clear, 0f);
        _fadeOutScreen.DOColor(Color.clear, _speedTransitionScreen);
        yield return new WaitForSeconds(_speedTransitionScreen);
        _scriptPlayer.PlayStory();
    }

    public void ButtonTravel()
    {
        _scriptPlayer.ClearStory();
        _fadeOutScreen.DOColor(Color.black, _speedTransitionScreen);
        StartCoroutine(DelayLevelSelect());
        MusicFadeOut();
    }

    private IEnumerator DelayLevelSelect()
    {
        yield return new WaitForSeconds(_speedTransitionScreen);
        _levelScreenElements.SetActive(true);
        _background.DOColor(_colorBackground, 0f);
        _fadeOutScreen.DOColor(Color.clear, _speedTransitionScreen);
        MusicFadeIn();
        _audioSource.clip = _areaTrack[0];
        _audioSource.Play();
        yield return new WaitForSeconds(_speedTransitionScreen);
    }

    private void MusicFadeOut()
    {
        for (int i = 0; i < 1000; i++)
        {
            if (_audioSource.volume > 0)
            {
                _audioSource.volume -= 0.001f;
            }
        }
    }

    private void MusicFadeIn()
    {
        for (int i = 0; i < 1000; i++)
        {
            if (_audioSource.volume < 1)
            {
                _audioSource.volume += 0.001f;
            }
        }
    }

    public void EnvironmentFrame()
    {
        switch (CurrentArea)
        {
            case 0:
                _environment.sprite = _frameForest[CurrentFrame];
                break;
            case 1:
                _environment.sprite = _frameCity[CurrentFrame];
                break;
            case 2:
                _environment.sprite = _frameFactory[CurrentFrame];
                break;
            case 3:
                _environment.sprite = _frameLake[CurrentFrame];
                break;
        }
    }
}
