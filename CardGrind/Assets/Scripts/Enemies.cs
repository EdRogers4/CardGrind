using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Enemies : MonoBehaviour
{
	[Header("Scripts")] 
	[SerializeField] private Player _scriptPlayer;

	[Header("Enemy Cards")] 
	public int CurrentEnemy;
	public int[] CurrentEnemyCards;
	public int[] EnemyCard1;
	public int[] EnemyCard2;
	public int[] EnemyCard3;
	[SerializeField] private Animator _animatorEnemyCards;
	[SerializeField] private Sprite[] _enemyPortrait;
	[SerializeField] private Image _imagePortrait;
	public int[] EnemyKillExperience;
	public int[] EnemyCardExperience;

	[Header("Effects")]
	[SerializeField] private Image[] _blood;

	[Header("Sounds")] 
	private AudioSource _audioSource;
	public AudioClip[] SoundDeath;
	public AudioClip[] SoundHit;
	public AudioClip[] SoundAttack;

	[Header("Enemy Panel")]
	[SerializeField] private Image[] _imagesEnemyPanel;
	[SerializeField] private TextMeshProUGUI _textEnemyName;
	[SerializeField] private float _timeFadeInEnemy;
	[SerializeField] private float _timeFadeOutEnemy;
	private Color _maroon = new Color(0.51f, 0.37f, 0.41f);
	private Color _blackFaded = new Color(0, 0, 0, 0.68f);
	private Vector2 _cardConnectorLength = new Vector2(415, 10);

	private void Start()
	{
		_audioSource = gameObject.GetComponent<AudioSource>();
	}

	public void EnemyFadeIn()
	{
		_textEnemyName.DOColor(Color.white, _timeFadeInEnemy);
		_imagePortrait.sprite = _enemyPortrait[CurrentEnemy];
		StartCoroutine(ShowEnemyCards());
        
		for (int i = 0; i < _imagesEnemyPanel.Length; i++)
		{
			if (i == 1)
			{
				_imagesEnemyPanel[i].DOColor(_blackFaded, _timeFadeInEnemy);
			}
			else if (i == 3)
			{
				_imagesEnemyPanel[i].DOColor(_blackFaded, _timeFadeInEnemy);
			}
			else
			{
				_imagesEnemyPanel[i].DOColor(Color.white, _timeFadeInEnemy);
			}
		}
	}

	public IEnumerator EnemyFadeOut()
	{
		_audioSource.PlayOneShot(SoundDeath[Random.Range(0, SoundDeath.Length)], 1.5f);
		yield return new WaitForSeconds(2.0f);
		_textEnemyName.DOColor(Color.clear, _timeFadeInEnemy);
		_animatorEnemyCards.SetBool("isShow", false);
		
		for (int i = 0; i < _imagesEnemyPanel.Length; i++)
		{
			_imagesEnemyPanel[i].DOColor(Color.clear, _timeFadeOutEnemy);
		}
	}

	private IEnumerator ShowEnemyCards()
	{
		yield return new WaitForSeconds(0.5f);
		_imagesEnemyPanel[1].gameObject.GetComponent<RectTransform>().DOSizeDelta(_cardConnectorLength, 2);
		_animatorEnemyCards.SetBool("isShow", true);
	}
	
	public void BloodFadeIn()
	{
		var bloodIndex = Random.Range(0, 3);
		_blood[bloodIndex].DOColor(Color.white, 0);
		StartCoroutine(BloodFadeOut(_blood[bloodIndex]));
	}

	private IEnumerator BloodFadeOut(Image blood)
	{
		yield return new WaitForSeconds(1.5f);
		blood.DOColor(Color.clear, 0.5f);
	}
}
