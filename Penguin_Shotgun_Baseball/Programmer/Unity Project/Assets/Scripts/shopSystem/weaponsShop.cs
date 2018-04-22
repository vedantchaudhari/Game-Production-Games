


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponsShop : MonoBehaviour {

	GameObject player;

	public List<weapons> weaponList = new List<weapons>();

	private List<GameObject> itemHolderList = new List<GameObject> ();

	public List<GameObject> buyButtonList = new List<GameObject>();

	public static weaponsShop WeaponShop; 

	public GameObject itemHolderPrefab;
	public Scrollbar scroller;
	GameObject moneyUI;
	GameObject equippedUI;
	public AudioClip errorSound;

	public Transform grid;
	weapons weaponSelected;
	GameObject itemHeld;
	int maxItem;
	int minItem;
	int currentItem;
	//public bool first;
	bool goBack = true;

	// checking input
	float timeOfFirstButton;
	bool pressed = false;
	int pressNum = 0;
	float timeDelay = 0.20f;
	float heldDelay = 0.35f;
	float heldTime = 0.0f;
	bool heldPress = false;
	void Awake()
	{
		//first = true;
	}

	// Use this for initialization
	void Start () {
		equippedUI = GameObject.Find("Canvas/shopPanel/equippedUI");
		moneyUI = GameObject.Find ("Canvas/shopPanel/moneyUI");
		player = GameObject.Find ("Player");
		WeaponShop = this;
		fillList ();
		gameManager.gameManagerr.GetComponent<saveLoad> ().Load ();
		gameObject.SetActive (false);
		//gameManager.gameManagerr.GetComponent<saveLoad> ().Load ();
		weaponSelected = weaponList [0];
		itemHeld = itemHolderList [0];
		//itemHeld.GetComponent<buyButton>().buttonText.text = "Back";
		if (weaponSelected.weaponID == 1) {
			itemHeld.transform.Find ("buyButton").gameObject.GetComponent<buyButton> ().buttonText.text = "Back";
			itemHeld.transform.Find ("buyButton").gameObject.GetComponent<buyButton> ().GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
		}
	}

	// Update is called once per frame
	void Update () {
		checkItemSelected ();
		highlightSelected ();
	}

	void fillList()
	{
		for (int i = 0; i < weaponList.Count; i++) {
			GameObject holder = Instantiate (itemHolderPrefab, grid,false);
			itemHolder holderScript = holder.GetComponent<itemHolder>();

			holderScript.itemName.text = weaponList [i].weaponName;
			holderScript.itemPrice.text = "$ " + weaponList [i].weaponPrice.ToString("N2");
			holderScript.itemID = weaponList [i].weaponID;

			// BUY BUTTON
			holderScript.buyButton.GetComponent<buyButton>().weaponID = weaponList[i].weaponID;

			// HANDLE LIST
			itemHolderList.Add(holder);
			buyButtonList.Add (holderScript.buyButton);


			if (weaponList [i].bought) {
				//holderScript.itemImage.sprite = weaponList [i].boughtSprite;
				holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + weaponList[i].boughtSprite); 

				// need to have a solid naming convetion.. would use weaponList[i].boughtSprite for bought and
				// weaponList[i].boughtSprite + "_black" or whatever makes it unbought in the sprite name
				// everything else is the same
				// except change bought/unbought to spriteName
			}  else {
				//holderScript.itemImage.sprite = weaponList [i].unboughtSprite;
				holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + weaponList[i].unboughtSprite);
			}

		}
	}

	public void updateSprite(int weaponID)
	{
		for (int i = 0; i < itemHolderList.Count; i++) {
			itemHolder holderScript = itemHolderList [i].GetComponent<itemHolder> ();
			if (holderScript.itemID == weaponID) {
				for (int j = 0; j < weaponList.Count; j++) {
					if (weaponList [j].weaponID == weaponID) {
						if (weaponList [j].bought) {
							if (weaponList [j].bought) {
								//holderScript.itemImage.sprite = weaponList [i].boughtSprite;
								holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + weaponList[j].boughtSprite); 
								holderScript.itemPrice.text = "Sold out!";
								// need to have a solid naming convetion.. would use weaponList[i].boughtSprite for bought and
								// weaponList[i].boughtSprite + "_black" or whatever makes it unbought in the sprite name
								// everything else is the same
								// except change bought/unbought to spriteName
							}  else {
								//holderScript.itemImage.sprite = weaponList [i].unboughtSprite;
								holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + weaponList[j].unboughtSprite);
							}
						}
					}
				}
			}
		}
	}

	public void updateBuyButtons()
	{
		int currentWeaponID = gameManager.gameManagerr.currentWeaponID;
		Debug.Log (gameManager.gameManagerr.currentWeaponID);
		string currentWeaponType = gameManager.gameManagerr.currentWeaponType;
		Debug.Log (gameManager.gameManagerr.currentWeaponType);
		for (int i = 0; i < buyButtonList.Count; i++) {
			buyButton buyButtonScript = buyButtonList [i].GetComponent<buyButton> ();
			for (int j = 0; j < weaponList.Count; j++) {
				if (weaponList [j].weaponID == buyButtonScript.weaponID && weaponList [j].bought && weaponList [j].weaponID != currentWeaponID) {
					if (string.Equals (weaponList [j].itemType, buyButtonScript.weaponType) && string.Equals (weaponList [j].itemType, currentWeaponType)) {
						//Debug.Log (weaponList [j].itemType);
						//Debug.Log (buyButtonScript.weaponType);
						if (weaponList [j].weaponID == 1) {
							buyButtonScript.buttonText.text = "Back";
							buyButtonScript.GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
						}
						else {
							buyButtonScript.buttonText.text = "Use";
							buyButtonScript.GetComponent<Image> ().color = new Color (0f, 0f, 1f, 1f);
							weaponList [j].isBackButton = 0;
						}
					}
				}  else if (weaponList [j].weaponID == buyButtonScript.weaponID && weaponList [j].bought && weaponList [j].weaponID == currentWeaponID) {
					if (string.Equals (weaponList [j].itemType, buyButtonScript.weaponType)&& string.Equals (weaponList [j].itemType, currentWeaponType)) {
						// Bring up UI that sa ys equipped for 1 sec over the shop
						//if (weaponList [i].isBackButton == 0 && weaponList[i].isChanged == false) {

						/*for (int z = 0; z < weaponList.Count; z++) {
							buyButton buyButtonScript2 = buyButtonList [z].GetComponent<buyButton> ();
							if (weaponList [z].weaponID == buyButtonScript.weaponID && weaponList [z].bought && weaponList [z].weaponID == currentWeaponID && weaponList [z].isBackButton == 0) {
									weaponList [z].isBackButton = 0;
									buyButtonScript2.buttonText.text = "Use";
						
							} else if (weaponList [z].weaponID == buyButtonScript.weaponID && weaponList [z].bought && weaponList [z].weaponID != currentWeaponID && weaponList [z].isBackButton == 1) {
								
									weaponList [z].isBackButton = 0;
									buyButtonScript2.buttonText.text = "Use";
							}
						}*/

						for (int z = 0; z < weaponList.Count; z++) {
							buyButton buyButtonScript2 = buyButtonList [z].GetComponent<buyButton> ();
							if (string.Equals (weaponList [z].itemType, buyButtonScript.weaponType) && weaponList[z].bought) {
								weaponList [z].isBackButton = 0;
								buyButtonScript2.buttonText.text = "Use";
								buyButtonScript2.GetComponent<Image> ().color = new Color (0f, 0f, 1f, 1f);
							}
						}
						/*for (int q = 0; q < weaponList.Count; q++) {
							buyButton buyButtonScript2 = buyButtonList [q].GetComponent<buyButton> ();
							if (weaponList [q].weaponID != weaponList [j].weaponID && weaponList[q].bought && string.Equals (weaponList [j].itemType, buyButtonScript.weaponType)) {
								if (weaponList [q].weaponID == 1) {
									weaponList [q].isBackButton = 1;
									buyButtonScript2.buttonText.text = "Back";
								} else{
									weaponList [q].isBackButton = 0;
									buyButtonScript2.buttonText.text = "Use";
								}
							}
						}*/
						weaponList[j].isBackButton = 1;
						buyButtonScript.buttonText.text = "Back";
						buyButtonScript.GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
									
							gameManager.gameManagerr.GetComponent<saveLoad> ().Saving ();
							//StartCoroutine (changeToBack (buyButtonScript, weaponList [j]));
						//}
						if (currentWeaponType == "Shotgun") {
							player.GetComponent<test> ().startingVelocity = weaponList [j].forceToAdd;
						}  else if (currentWeaponType == "Ammo") {
							player.GetComponent<fireGun> ().bulletAmount = weaponList [j].bulletCount;
						}  else if(currentWeaponType == "Bullet")
						{
							player.GetComponent<fireGun> ().force = weaponList [j].forceToAdd;
						}
					}
				}
			}
		}
	}

	public void loadBuyButtons()
	{
		int currentWeaponID = gameManager.gameManagerr.currentWeaponID;
		string currentWeaponType = gameManager.gameManagerr.currentWeaponType;
		for (int j = 0; j < weaponList.Count; j++) {
			buyButton buyButtonScript = buyButtonList [j].GetComponent<buyButton> ();
			if (weaponList [j].bought && weaponList [j].weaponID != currentWeaponID) {
				if (string.Equals (weaponList [j].itemType, currentWeaponType)) {
					if (weaponList [j].isBackButton == 1) {
						buyButtonScript.buttonText.text = "Back";
						buyButtonScript.GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
						if (weaponList [j].itemType == "Shotgun") {
							player.GetComponent<test> ().startingVelocity = weaponList [j].forceToAdd;
						}else if (weaponList [j].itemType == "Ammo") {
							player.GetComponent<fireGun> ().bulletAmount = weaponList [j].bulletCount;
						}  else if(weaponList [j].itemType == "Bullet")
						{
							player.GetComponent<fireGun> ().force = weaponList [j].forceToAdd;
						}
					} else {
						buyButtonScript.buttonText.text = "Use";
						buyButtonScript.GetComponent<Image> ().color = new Color (0f, 0f, 1f, 1f);
					}
				}
			}
		}
	}

	void highlightSelected()
	{
		itemHeld.GetComponent<Image> ().color =  new Color(itemHeld.GetComponent<Image>().color.r, itemHeld.GetComponent<Image>().color.g, itemHeld.GetComponent<Image>().color.b, .85f);
		//Debug.Log (weaponSelected.);
	}

	void checkItemSelected()
	{
		if (Input.GetKey (KeyCode.Space) /*&& !first*/) {
			heldTime += Time.deltaTime;
			heldPress = true;
			if (heldTime > heldDelay && goBack) {
				goBack = false;
				itemHeld.transform.Find ("buyButton").gameObject.GetComponent<buyButton> ().buyWeapon ();
				//Debug.Log ("Held");
			}
		}  else if (heldTime < heldDelay && heldPress /*&& !first*/) {
			heldPress = false;
			pressNum++;
			if (pressed) {
				return;
			}
			pressed = true;
			Invoke ("singleOrDouble", timeDelay);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			if (equippedUI.activeSelf == true) {
				equippedUI.SetActive (false);
			} else if (moneyUI.activeSelf == true) {
				moneyUI.SetActive (false);
			}
			goBack = true;
			heldPress = false;
			heldTime = 0.0f;
			//first = false;
			//Debug.Log ("Unheld");
		}
	}

	void singleOrDouble()
	{

		maxItem = weaponList.Count;
		minItem = 1;
		currentItem = weaponSelected.weaponID - 1;
		if (pressNum > 1) {
			// up one
			//Debug.Log("before:" + currentItem);
			goBack = true;
			if (currentItem - minItem < 0) {
				SoundManager.instance.playSingle(errorSound);
				// play sound that shop can't go up anymore
			}  else {
				//Debug.Log (scroller.GetComponent<Scrollbar> ().value);
				itemHeld.GetComponent<Image>().color = new Color(itemHeld.GetComponent<Image>().color.r, itemHeld.GetComponent<Image>().color.g, itemHeld.GetComponent<Image>().color.b, 1f);
				currentItem--;
				if (currentItem == 0 || currentItem == 1) {
					scroller.GetComponent<Scrollbar> ().value = 1.0f;
				}  else if (currentItem == maxItem - 2) {
					scroller.GetComponent<Scrollbar> ().value = 0.0f;
				}  else {
					scroller.GetComponent<Scrollbar> ().value = 1.0f - (currentItem / (float)maxItem);
				}
				itemHeld = itemHolderList [currentItem];
				weaponSelected = weaponList [currentItem];
				//Debug.Log ("after:" + currentItem);
			}
			//Debug.Log ("Double");
		}  else {
			goBack = true;
			// down one
			if (currentItem + minItem == maxItem) {
				// play sound that shop can't go up anymore
				SoundManager.instance.playSingle(errorSound);
			}  else {
				//Debug.Log ("Size:" + weaponList.Count);
				//Debug.Log (scroller.GetComponent<Scrollbar> ().value);
				currentItem++;
				itemHeld.GetComponent<Image>().color =  new Color(itemHeld.GetComponent<Image>().color.r, itemHeld.GetComponent<Image>().color.g, itemHeld.GetComponent<Image>().color.b, 1f);
				if (currentItem == maxItem-1 || currentItem == maxItem-2) {
					scroller.GetComponent<Scrollbar> ().value = 0.0f;
				}  else {
					scroller.GetComponent<Scrollbar> ().value = 1.0f - (currentItem / (float)maxItem);
				}
				itemHeld = itemHolderList [currentItem];
				weaponSelected = weaponList [currentItem];
				//Debug.Log ("after:" + currentItem);
			}
			//Debug.Log ("Single");
		}
		pressNum = 0;
		pressed = false;
	}

	// old implementation 
	/*IEnumerator changeToBack(buyButton buyButtonScript, weapons weapon)
	{
		if (weapon.isBackButton == 0) {
			weapon.isChanged = true;
			yield return new WaitForSeconds (2f);
			weapon.isBackButton = 1;
			buyButtonScript.buttonText.text = "Back?";
		}
	}*/
}	