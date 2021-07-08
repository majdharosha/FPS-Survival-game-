using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerAttack : MonoBehaviour {


	private WeaponsManager weapon_Manager;

	private float fireRate = 15f;
	private float nextTimetoFire;

	public float damage = 20f;

	private Animator zoomCameraAnim;

	private bool zoomed;
	private Camera mainCam;
	private GameObject crossHair;

	private bool Is_Aiming;

	[SerializeField]
	private GameObject Arrow_Prefab, Spear_Prefab;

	[SerializeField]
	private Transform arrow_bow_Start_position;



	void Awake()
    {
		weapon_Manager = GetComponent<WeaponsManager>();

		zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

		crossHair = GameObject.FindWithTag(Tags.CROSSHAIR);

		mainCam = Camera.main;

    }


	void Start () {
		
	}
	

	void Update () {
		WeaponShoot();
		ZoomInAndOut();
	}



	void WeaponShoot()
    {
		if (weapon_Manager.GetCurrentSelectedWeaponInfo().fireType == WeaponFireType.MULTIPLE)
        {
			if (Input.GetMouseButton(0) && Time.time > nextTimetoFire)
            {
				nextTimetoFire = Time.time + 1f / fireRate;

				weapon_Manager.GetCurrentSelectedWeaponInfo().ShootAnimation();

				BulletFired();

			}

        }
		else
        {
			if (Input.GetMouseButtonDown(0))
            {
				//handle Axe 
				if (weapon_Manager.GetCurrentSelectedWeaponInfo().tag == (Tags.AXE_TAG))
                {
					weapon_Manager.GetCurrentSelectedWeaponInfo().ShootAnimation();
				}

				//handle shoot
				if (weapon_Manager.GetCurrentSelectedWeaponInfo().bulletType == WeaponBulletType.BULLET)
                {
					weapon_Manager.GetCurrentSelectedWeaponInfo().ShootAnimation();
					BulletFired();
				}
				else
                {
					if (Is_Aiming)
                    {
						weapon_Manager.GetCurrentSelectedWeaponInfo().ShootAnimation();

						if (weapon_Manager.GetCurrentSelectedWeaponInfo().bulletType == WeaponBulletType.ARROW)
                        {
							ThrowArrowOrSpear(true);
                        }

						else if (weapon_Manager.GetCurrentSelectedWeaponInfo().bulletType == WeaponBulletType.SPEAR)
                        {
							ThrowArrowOrSpear(false);
						}
                        

					}
                }



            }
        }

    }


	void ZoomInAndOut()
    {
		if (weapon_Manager.GetCurrentSelectedWeaponInfo().weaponaim == WeaponAim.AIM)
        {
			if (Input.GetMouseButtonDown(1))
            {
				zoomCameraAnim.Play(AnimationTags.Zoom_IN_ANIM);

				crossHair.SetActive(false);

            }

			if (Input.GetMouseButtonUp(1))
			{
				zoomCameraAnim.Play(AnimationTags.Zoom_Out_ANIM);

				crossHair.SetActive(true);

			}


		}

		if (weapon_Manager.GetCurrentSelectedWeaponInfo().weaponaim == WeaponAim.SELF_AIM)
        {
			if (Input.GetMouseButtonDown(1))
            {
				weapon_Manager.GetCurrentSelectedWeaponInfo().AIM(true);
				Is_Aiming = true; 


			}

			if (Input.GetMouseButtonUp(1))
			{
				weapon_Manager.GetCurrentSelectedWeaponInfo().AIM(false);
				Is_Aiming = false;


			}

		}
    }

	


	void ThrowArrowOrSpear(bool throwArrow)
    {
		if (throwArrow)
        {
			GameObject arrow = Instantiate(Arrow_Prefab);

			arrow.transform.position = arrow_bow_Start_position.position;

			arrow.GetComponent<ArrowBow>().Launch(mainCam);

        }
        else
        {
			GameObject spear = Instantiate(Spear_Prefab);

			spear.transform.position = arrow_bow_Start_position.position;

			spear.GetComponent<ArrowBow>().Launch(mainCam);

		}
    }



	void BulletFired()
    {

		RaycastHit hit;

		if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
			if (hit.transform.gameObject.tag == Tags.ENEMY_TAG)
            {
				hit.transform.GetComponent<Health>().ApplyDamage(damage);
            }
        }
    }


}
