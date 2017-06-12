using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// importeaza librariile necesare

public class Game : MonoBehaviour { // clasa Game mosteneste de la clasa MonoBehaviour pentru a putea fi aplicata ca o componenta in Unity

	public Camera mainCamera; // referinta la camera din joc 
	public GameObject startButton; // referinta la butonul de start
	private bool gameIsRunning=false; // variabila booleana care contine informatie referitoare la stdiul in care este jocul
	public GameObject[] things; // vector de obiecte care pot aparea in joc
	public int numberOfThings=0; // numarul current de obiecte din joc
	private GameObject thing; // 
	public GameObject scoreText; // referinta la textul care afiseaza scorul
	public int score=0; // scor
	
	void Start() // functie din clasa MonoBehaviour care este apelata la instantierea obiectului care are aceasta clasa ca componenta
	{
		Button btn = startButton.GetComponent<Button>(); // se gaseste componenta de clasa "Button" din obiectul startButton
		btn.onClick.AddListener(StartGame); // seteaza ca in evenimenul in care se da click pe buton functia StartGame() este apelata
	}

	void StartGame()
	{
		numberOfThings=0;
		score=0;
		Debug.Log("Buton Apasat");
		startButton.SetActive(false); // dezactiveaza butonul de start
		gameIsRunning=true;
		StartCoroutine (spawnThings ()); // porneste o functie apelata de mai multe ori numita spawnThings()
	}

	void doRaycast(Vector3 location) // functie care determina daca un obiect este intr-un aunumit loc din ecran
	{
		int count=0; // variabila folosita pentru a nu da eroare raycastul 
		Ray ray=mainCamera.ScreenPointToRay(location); // este generata o linie viertuala de la pozitia ecranului in spatiul jocului
		RaycastHit hit; // variabila folosita pentru a gasi informatii despre obiectele intersectate cu linia virtuala
		count++; // variabila count este incrementata cu 1
		
		if (Physics.Raycast(ray, out hit)) // in cazul in care un obiect este lovit de linie urmatorul cod va fi executat
		{
	
			Transform objectHit= hit.transform; // objectHit va lua referinta la componenta tranform a obiectului lovit
			if(objectHit.tag=="red") // daca obiectul este categorizat ca fiind rosu
				{
				print("hit red");
				numberOfThings--;
				Destroy(objectHit.gameObject); 
				Scene loadedLevel = SceneManager.GetActiveScene (); // terminare joc 
				SceneManager.LoadScene (loadedLevel.buildIndex); // reluare scenei
				}
			else if(objectHit.tag=="green") // daca obiectul lovit este verde
				{
				print("Hit green");
				numberOfThings--;
				score++; // incrementarea scor
				scoreText.GetComponent<Text>().text="SCOR: "+ score; // afisare scor nou

				Destroy(objectHit.gameObject); // distrugere obiect lovit
				}
			else {print("Nope");return;}
		}
		
	}

	IEnumerator spawnThings ()  // functie de tip Ienumerator, o functie in care se poate astepta 

	{	
		yield return new WaitForSeconds (1); // se asteapta 1 secunda
		while (gameIsRunning)  // cat timp jocul este pornit si nu in meniu
		{	
			if(numberOfThings<30) // daca sunt sub 30 de obiecte pe ecran
			{
				int i;
				for (i = 1; i <= Random.Range(3,6); i++)  // vor aparea intre 3 si 6 obiecte (rosii sau verzi)
				{
					thing = things [Random.Range(0,things.Length)];

					Vector3 spawnPosition = new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (-3.5f, 3.5f), 0);
					// obiectele vor aparea intr-o locaite aleatoare pe ecran intre coordonatele specificate
					Quaternion spawnRotation = new Quaternion ();
					// rotatia este mereu cea implicita
					Instantiate (thing, spawnPosition, spawnRotation);
					// crearea propriu zisa a obiectelor
					numberOfThings++;
					yield return new WaitForSeconds (Random.Range(0.1f,0.5f));
					//se asteapta intre crearea a 2 obiecte consecutive
				}
			}
			yield return new WaitForSeconds (2); // se asteapta 2 secunde si se va relua corutina
		}
	}

	void Update () // funcie care este apelata la fiecare frame de randare a jocului
	{
		if(gameIsRunning)
		{
			if (Input.touchCount > 0 && Input.touchCount < 2) // daca este atins ecranului
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					doRaycast(Input.GetTouch(0).position); // se va apela functia doRaycast la pozitia de pe ecran unde este aatins ecranul
					print("Doing raycast!");
				}
			}





		}



	}


}
