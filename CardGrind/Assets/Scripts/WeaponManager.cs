using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using LBG.UI.Radial;
using TMPro;

public class WeaponManager : MonoBehaviour
{
	public int[] CurrentWeapon;
	public int CurrentCharacterEquip;
	public bool[] isWeaponUnlocked;

	[Header("Weapon Screen")]
	[SerializeField] private CardManager _scriptCardManager;

	[Header("Weapon Screen")]
	[SerializeField] private RectTransform _equipScreen;
	[SerializeField] private RadialLayerButtons _weaponLayer;
	[SerializeField] private RadialMenuButton[] _weaponButton;
	[SerializeField] private Sprite[] spriteGun;
	[SerializeField] private Sprite[] spriteGunBlack;

	[Header("Equip UI")]
	[SerializeField] private Image[] _characterWeapon;
	[SerializeField] private Image _weaponDisplay;
	[SerializeField] private Image _ammoDisplay;
	[SerializeField] private TextMeshProUGUI _textWeaponName;
	[SerializeField] private TextMeshProUGUI _textAmmo;
	[SerializeField] private TextMeshProUGUI _textDamage;

	[Header("Ammo")]
	[SerializeField] private int[] _ammoIndex;
	[SerializeField] private Sprite[] _ammoSprite;
	public int[] AmmoCount;

	[Header("Damage")]
	public int[] DamageMin;
	public int[] DamageMax;

	[Header("Bullet Effects")]
	public AudioClip[] SoundGunshot;
	public AudioClip[] SoundReload;
	public AudioClip[] SoundDraw;
	public List<Image> BulletHoles1;
	public List<Image> BulletHoles2;
	public List<Image> BulletHoles3;

	public void BulletHoleFadeIn(int index, Image bulletHole)
	{
		switch (index)
		{
			case 0:
				BulletHoles1.Remove(bulletHole);
				break;
			case 1:
				BulletHoles2.Remove(bulletHole);
				break;
			case 2:
				BulletHoles3.Remove(bulletHole);
				break;
		}
		
		bulletHole.DOColor(Color.white, 0);
		StartCoroutine(BulletHoleFadeOut(index, bulletHole));
	}

	private IEnumerator BulletHoleFadeOut(int index, Image bulletHole)
	{
		yield return new WaitForSeconds(1.5f);
		bulletHole.DOColor(Color.clear, 0.5f);

		switch (index)
		{
			case 0:
				BulletHoles1.Add(bulletHole);
				break;
			case 1:
				BulletHoles2.Add(bulletHole);
				break;
			case 2:
				BulletHoles3.Add(bulletHole);
				break;
		}
	}

	public void OpenEquipScreen(int CurrentCharacter)
    {
		CurrentCharacterEquip = CurrentCharacter;
		_equipScreen.gameObject.SetActive(true);
		_weaponDisplay.sprite = spriteGun[CurrentWeapon[CurrentCharacterEquip]];
		_ammoDisplay.sprite = _ammoSprite[_ammoIndex[CurrentWeapon[CurrentCharacterEquip]]];
		_textWeaponName.text = _weaponButton[CurrentWeapon[CurrentCharacterEquip]].m_ElementName;
		_textAmmo.text = "" + AmmoCount[_ammoIndex[CurrentWeapon[CurrentCharacterEquip]]];
		_textDamage.text = "Damage: " + DamageMin[CurrentWeapon[CurrentCharacterEquip]] + "-" + DamageMax[CurrentWeapon[CurrentCharacterEquip]];
	}

	public void CloseEquipScreen()
    {
		_equipScreen.gameObject.SetActive(false);
    }

	public void EquipWeapon(int index)
    {
		if (isWeaponUnlocked[index])
        {
			_characterWeapon[CurrentCharacterEquip].sprite = spriteGun[index];
			_weaponDisplay.sprite = spriteGun[index];
			_ammoDisplay.sprite = _ammoSprite[_ammoIndex[index]];
			_textWeaponName.text = _weaponButton[index].m_ElementName;
			_textAmmo.text = "" + AmmoCount[_ammoIndex[index]];
			_textDamage.text = "Damage: " + DamageMin[index] + "-" + DamageMax[index];
			_scriptCardManager.SetPlayerDamage(CurrentCharacterEquip, 0);
        }
    }
}
