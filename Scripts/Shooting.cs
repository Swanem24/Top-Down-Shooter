using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
	//	Gun and bullet prefabs	//
	public Transform firePoint;
	public GameObject bulletPrefab;
	public GameObject ar_bulletPrefab;
	public float bulletForce = 20f;
	
	//	Sound Effects	//
	public GameObject hg_sound_effect;
	public GameObject ar_sound_effect;
	
	//	Ammo	//
	int ammo = 20;
	int hand_gun_ammo = 20;
	int hg_t_ammo = 20;
	public Text ammo_text;
	int rifle_ammo = 100;
	int ar_t_ammo = 100;
	
	//	Animation	//
	private Animator anim;
	
	//	Shop	//
	public GameObject shop_ui;	
	int shop_clicked = 0;
	bool shopping = false;
	public Player_Currency shop_gun;
	
	//	Reloading	//
	public GameObject reloading_UI;
	bool isReloading = false;
	
	//	FlameThrower	//
	public GameObject flamethrower_prefab;
	
	//	Current Weapon	//
	string currently_equipped;
	public Text weapon_text;
	public GameObject HG_image;
	public GameObject AR_image;
	bool bought_assault_rifle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		swap_to_handgun();
    }

    // Update is called once per frame
    void Update()
    {
		//	Checks if user bought Assault Rifle	//
		bought_assault_rifle = shop_gun.bought_ar;
	
	
		//	Weapon Swap	//
		if(Input.GetKeyDown(KeyCode.F1))
		{
			swap_to_handgun();
		}
		else if(Input.GetKeyDown(KeyCode.F2) && bought_assault_rifle == true)
		{
			swap_to_rifle();
		}
		//	Reload	//
		else if(Input.GetKeyDown(KeyCode.R))
		{
			reloading_UI.SetActive(true);
			isReloading = true;
			StartCoroutine(Reloading());
		}
	
		//	Shooting	Hand Gun	//
		if (Input.GetMouseButtonDown(0) && currently_equipped == "Hand Gun")
		{
			Shoot_Hand_Gun();
		} 
		//	Shooting	Assault Rifle	//
		else if (Input.GetMouseButtonDown(0) && currently_equipped == "Assault Rifle")
		{
			Shoot_Assault_Rifle();
		}
		else
		{
			anim.SetBool("isShooting", false);
		}
		
		//	FlameThrower	//
		if (Input.GetMouseButtonDown(1))
		{
			flamethrower_prefab.SetActive(true);
		}
		else if (Input.GetMouseButtonUp(1))
		{
			flamethrower_prefab.SetActive(false);
		}
    }
	
	void Shoot_Hand_Gun()
	{
		if(hand_gun_ammo > 0 && isReloading == false && shopping == false)
		{
			hand_gun_ammo -= 1;
			ammo_text.text = "Ammo: " + hand_gun_ammo + "/20";
			anim.SetBool("isShooting", true);
		
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			Instantiate(hg_sound_effect, firePoint.position, firePoint.rotation);
			Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
			rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
		}
		else
		{
			//	No ammo	//
			anim.SetBool("isShooting", false);
		}
	}
	
	void Shoot_Assault_Rifle()
	{
		if(rifle_ammo > 0 && isReloading == false && shopping == false)
		{
			rifle_ammo -= 1;
			ammo_text.text = "Ammo: " + rifle_ammo + "/100";
			anim.SetBool("isShooting", true);
		
			GameObject ar_bullet = Instantiate(ar_bulletPrefab, firePoint.position, firePoint.rotation);
			Instantiate(ar_sound_effect, firePoint.position, firePoint.rotation);
			Rigidbody2D rb = ar_bullet.GetComponent<Rigidbody2D>();
			rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
		}
		else
		{
			//	No ammo	//
			anim.SetBool("isShooting", false);
		}
	}
	
	IEnumerator Reloading()
	{
		ammo_text.text = "Reloading....";
		anim.SetBool("isReloading", true);
		
		switch (currently_equipped)
		{
			case ("Hand Gun"):
				hand_gun_ammo = 20;
				yield return new WaitForSeconds(2f);
				ammo_text.text = "Ammo: " + hand_gun_ammo + "/20";
				reloading_UI.SetActive(false);
				anim.SetBool("isReloading", false);
				isReloading = false;
				break;
				
			case ("Assault Rifle"):
				rifle_ammo = 100;
				yield return new WaitForSeconds(2f);
				ammo_text.text = "Ammo: " + rifle_ammo + "/100";
				reloading_UI.SetActive(false);
				anim.SetBool("isReloading", false);
				isReloading = false;
				break;
		}
	}
	
	//	SHOP	//
	public void shop()
	{
		switch (shop_clicked)
		{
			case 0:
				shop_ui.SetActive(true);
				shopping = true;
				shop_clicked += 1;
				break;
			case 1:
				shop_ui.SetActive(false);
				shopping = false;
				shop_clicked -= 1;
				break;
			default:
				break;
		}
	}
		
	public void swap_to_handgun()
	{
		anim.Play("Hand_Gun_idle");
		Debug.Log("Hand Gun");
		
		switch (currently_equipped)
		{
			case "Hand Gun":
				//	Already selected hand gun	//
				break;
			default:
				currently_equipped = "Hand Gun";
				ammo_text.text = "Ammo: " + hand_gun_ammo + "/20";
				weapon_text.text = "Hand Gun";
				HG_image.SetActive(true);
				AR_image.SetActive(false);
				break;
		}
	}
	
	public void swap_to_rifle()
	{
		anim.Play("Assault_Rifle_idle");
		Debug.Log("Assault Rifle");

		switch (currently_equipped)
		{
			case "Assault Rifle":
				//	Already selected rifle	//
				break;
			default:
				currently_equipped = "Assault Rifle";
				ammo_text.text = "Ammo: " + rifle_ammo + "/100";
				weapon_text.text = "Assault Rifle";
				HG_image.SetActive(false);
				AR_image.SetActive(true);
				break;
		}
	}
}
