using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// compnonenta aplicata obiectelor verzi si rosii care le face sa dispara dupa o perioada aleatoare de timp
public class despawn : MonoBehaviour {
	
	public int lifeSpan=5;
	public GameObject gameManager;
	
	
	void Start () // funcite apelata la instantiera unui obiect rosu sau verde
	{
		gameManager= GameObject.Find("GameManager"); // referinta a obiectului gameManager care are ca componenta clsas "Game"
		StartCoroutine (die ()); // dupa un anumit timp de secunde obiectul va disparea din joc chiar daca nu este atins de jucator
	}
	
	IEnumerator die () 

		{
		yield return new WaitForSeconds (Random.Range (lifeSpan -1.5f,lifeSpan + 3.5f)); // se asteapta intre  5-1.5 si 5+3.5 secunde
		Destroy(this.gameObject); // obiectul este distrus
		gameManager.GetComponent<Game>().numberOfThings--; // numarul de obiecte este redus cu 1 
		}

	}

