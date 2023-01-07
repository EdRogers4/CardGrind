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

	[Header("Sounds")] 
	public AudioClip[] SoundDeath;
	public AudioClip[] SoundHit;
	public AudioClip[] SoundAttack;

	[Header("Enemy Panel")]
	[SerializeField] private Image[] _imagesEnemyPanel;
	[SerializeField] private TextMeshProUGUI _textEnemyName;
	[SerializeField] private float _timeFadeInEnemy;
	private Color _maroon = new Color(0.51f, 0.37f, 0.41f);
	private Color _white = new Color(1, 1, 1, 1);
	private Color _blackFaded = new Color(0, 0, 0, 0.68f);
	private Vector2 _cardConnectorLength = new Vector2(415, 10);

	public void EnemyFadeIn()
	{
		_textEnemyName.DOColor(_white, _timeFadeInEnemy);
        
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
				_imagesEnemyPanel[i].DOColor(_white, _timeFadeInEnemy);
			}
		}

		StartCoroutine(ShowEnemyCards());
	}

	private IEnumerator ShowEnemyCards()
	{
		yield return new WaitForSeconds(2.0f);
		_imagesEnemyPanel[1].gameObject.GetComponent<RectTransform>().DOSizeDelta(_cardConnectorLength, 2);
		_animatorEnemyCards.SetBool("isShow", true);
	}
}
