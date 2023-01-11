using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreen : MonoBehaviour
{
    [SerializeField] private Player _scriptPlayer;
    [SerializeField] private Image[] _levelIcon;
    [SerializeField] private Image _fadeOutScreen;
    [SerializeField] private Image _background;
    [SerializeField] private RectTransform _levelSelector;
    [SerializeField] private GameObject _levelScreenElements;
    [SerializeField] private float _speedTransitionScreen;
    private Color _colorNotSelected = new Color(1f, 1f, 1f, 0.44f);
    private Color _colorBackground = new Color(0.14f, 0.14f, 0.14f, 1.0f);

    public void SelectLevel(int index)
    {
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
        StartCoroutine(DelayPlayStory());
    }
    
    private IEnumerator DelayPlayStory()
    {
        yield return new WaitForSeconds(_speedTransitionScreen + 0.5f);
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
    }

    private IEnumerator DelayLevelSelect()
    {
        yield return new WaitForSeconds(_speedTransitionScreen);
        _levelScreenElements.SetActive(true);
        _background.DOColor(_colorBackground, 0f);
        _fadeOutScreen.DOColor(Color.clear, _speedTransitionScreen);
        yield return new WaitForSeconds(_speedTransitionScreen);
    }
}
