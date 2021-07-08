using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum WeaponAim
{
	NONE,
	SELF_AIM,
	AIM
}

public enum WeaponFireType
{
	SINGLE,
	MULTIPLE
}
public enum WeaponBulletType
{
	BULLET,
	ARROW,
	SPEAR,
	NONE
}


public class WeaponHandler : MonoBehaviour {

	private Animator anim;

	public WeaponAim weaponaim;

	[SerializeField]
	private GameObject muzzleflash;

	[SerializeField]
	private AudioSource Shootsound , ReloadSound;

	public WeaponBulletType bulletType;

	public WeaponFireType fireType;

	public GameObject AttackPoint; 

	void Awake()
    {
		anim = GetComponent<Animator>();
    }

	public void ShootAnimation()
    {
		anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

	public void AIM(bool canAim)
    {
		anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);

    }

	public void Turn_ON_MuzzleFlash()
    {
		muzzleflash.SetActive(true);
    }
	public void Turn_OFF_MuzzleFlash()
	{
		muzzleflash.SetActive(false);
	}

	void PlayShootSound()
    {
		Shootsound.Play();
    }
	void PlayReloadSound()
	{
		ReloadSound.Play();
	}

	void Turn_ON_AttackPoint()
    {
		AttackPoint.SetActive(true);
    }
	void Turn_OFF_AttackPoint()
	{
		if (AttackPoint.activeInHierarchy)
        {
			AttackPoint.SetActive(false);
		}
		
	}



	void Start () {
		
	}
	
	void Update () {
		
	}


}
