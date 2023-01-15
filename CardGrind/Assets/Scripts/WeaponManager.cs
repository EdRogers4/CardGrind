using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
	public int[] CurrentWeapon;

	[Header("Weapon Screen")]
	[SerializeField] private RectTransform _equipScreen;

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

	public void OpenWeaponScreen()
    {
		_equipScreen.gameObject.SetActive(true);
    }
}
