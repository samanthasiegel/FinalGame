﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExitScript : MonoBehaviour {
	
	/** GLOBAL VARIABLES ***************************/

	// Handle changes to UI
	public GameObject DialogueContainer, SingleDialogue;
	public Font QuoteFont;

	// Text to display
	public string Room;
	public string WarningMessage;

	// Check required objects player must interact with before advancing
	public string[] RequiredInteractions;
	public bool ActiveExit = false;
	private HashSet<string> VisitedSet;

	public int NextScene;

	/** FUNCTIONS **********************************/

	// Initialize visited global variable
	void Start(){
		VisitedSet = GameObject.FindGameObjectWithTag ("Hero").GetComponent<HeroScript> ().Visited;
	}

	// Check if player interacted with all required objects before moving to next level
	public void CheckConditionsToExit(){
		if (!ActiveExit) { 
			// Impossible to move to this room in the scene
			DisplayMessage ();
			return;
		} else if (!RequiredInteractionsComplete ()) {
			// Still need to interact with objects in scene
			DisplayMessage ();
			return;
		}
		// Move onto next level
		else {
			SceneManager.LoadScene (NextScene);
		}
		
	}

	// Return true if player interacted with all required objects, false otherwise
	public bool RequiredInteractionsComplete(){
		foreach (string tag in RequiredInteractions) {
			if (!VisitedSet.Contains (tag)) {
				return false;
			}
		}
		return true;
	}

	// Change UI to display relevant message
	void DisplayMessage(){
		DialogueContainer.SetActive (true);
		SingleDialogue.SetActive (true);
		Text SingleText = SingleDialogue.GetComponentInChildren<Text> ();
		SingleText.font = QuoteFont;
		SingleText.text = WarningMessage;
		return;
	}
}
