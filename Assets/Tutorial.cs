using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Kontiki
{
	public class Tutorial : MonoBehaviour {
		private TutorialSteps step;
		private bool newStep = false;
		private bool endTutorial = false;

		public KeyCode skipKey;
		public Text skipKeyText;
		public float delay;
		private float timeStamp;


		public GameObject[] tutorialImages;
		public Character player;

		[Range(0, 1)]
		public float deadZone = 0.2f;

		// Tutorial Bools //
		private bool W,A,S,D,Q,E = false;

		// Use this for initialization
		void Start () {
			step = TutorialSteps.Movement;
			timeStamp = delay + Time.time;
			Log.Tutorial(true);
		}
		
		// Update is called once per frame
		void Update () {
			if((int)step < tutorialImages.Length && !endTutorial)
				if(!newStep && timeStamp < Time.time){
					newStep = true;
				}


			if(newStep)
				switch(step){
					case TutorialSteps.Movement: // ASK PLAYER TO MOVE
					{
						tutorialImages[(int)step].SetActive(true);

						if(Input.GetKey(KeyCode.W)) W = true;
						if(Input.GetKey(KeyCode.A)) A = true;
						if(Input.GetKey(KeyCode.D)) D = true;
						if(Input.GetKey(KeyCode.S)) S = true;

						if(W && A && S && D || Input.GetKey(skipKey))
						{
							tutorialImages[(int)step].SetActive(false);
							newStep = false;
							step = TutorialSteps.CameraControl;
							timeStamp = delay + Time.time;
						}
					}
					break;

					case TutorialSteps.CameraControl: // ASK PLAYER TO MOVE CAMERA
					{
						tutorialImages[(int)step].SetActive(true);

						if(Input.GetAxis("Mouse X") < -deadZone || Input.GetAxis("Mouse X") > deadZone || Input.GetAxis("Mouse Y") < -deadZone || Input.GetAxis("Mouse Y") > deadZone || Input.GetKey(skipKey))
						{
							tutorialImages[(int)step].SetActive(false);
							newStep = false;
							step = TutorialSteps.Interact;
							timeStamp = delay + Time.time;
						}
					}
					break;

					case TutorialSteps.Interact: // ASK PLAYER TO PRESS INTERACT BUTTON
					{
						tutorialImages[(int)step].SetActive(true);

						if(Input.GetKey(KeyCode.V) || Input.GetKey(skipKey))
						{
							tutorialImages[(int)step].SetActive(false);
							newStep = false;
							step = TutorialSteps.Talk;
							timeStamp = delay + Time.time;
						}
					}
					break;

					case TutorialSteps.Talk: // ASK PLAYER TO TALK WITH AN NPC
					{
						tutorialImages[(int)step].SetActive(true);

						if(player.languageExchanger.speakingTo != null || Input.GetKey(skipKey))
						{
							tutorialImages[(int)step].SetActive(false);
							newStep = false;
							step = TutorialSteps.Quest;
							timeStamp = delay + Time.time;
						}
					}
					break;

					case TutorialSteps.Quest: // ASK PLAYER TO GET A QUEST
					{
						tutorialImages[(int)step].SetActive(true);

					    if (Input.GetKey(skipKey))
                        {
                            tutorialImages[(int)step].SetActive(false);
                            newStep = false;
                            step = TutorialSteps.Response;
                            timeStamp = delay + Time.time;
                        }

						if(QuestSystem.Instance.acceptedQuests.Count != 0 && QuestSystem.Instance.acceptedQuests[0] != null)
						{
							tutorialImages[(int)step].SetActive(false);
							newStep = false;
							step = TutorialSteps.Response;
							timeStamp = delay + Time.time;
						}
					}
					break;

					case TutorialSteps.Response: // EXPLAIN RESPONSES
					{
						tutorialImages[(int)step].SetActive(true);

						if(Input.GetKey(skipKey))
						{
							tutorialImages[(int)step].SetActive(false);
							newStep = false;
							step = TutorialSteps.Information;
							timeStamp = delay + Time.time;
						}
					}
					break;

					case TutorialSteps.Information: // ASK PLAYER TO GET INFORMATION
					{
						tutorialImages[(int)step].SetActive(true);
						
					    if (Input.GetKey(skipKey))
                        {
							tutorialImages[(int)step].SetActive(false);
							step = TutorialSteps.QuestDescription;
							newStep = false;
                            timeStamp = delay + Time.time;
                        }
                        
						if(QuestSystem.Instance.acceptedQuests.Count != 0 && QuestSystem.Instance.acceptedQuests[0].askedPeople.Count > 0)
						{
							tutorialImages[(int)step].SetActive(false);
							step = TutorialSteps.QuestDescription;
							newStep = false;
                            timeStamp = delay + Time.time;
						}
					}
					break;

					case TutorialSteps.QuestDescription: // EXPLAIN QUEST MARKERS
					{
						tutorialImages[(int)step].SetActive(true);

						if(Input.GetKey(skipKey))
						{
							tutorialImages[(int)step].SetActive(false);
							newStep = false;
							step = TutorialSteps.QuestCylinder;
							timeStamp = delay + Time.time;
						}
					}
					break;

					case TutorialSteps.QuestCylinder: // Explain AOI
					{
						tutorialImages[(int)step].SetActive(true);

						if(Input.GetKey(skipKey))
						{
							tutorialImages[(int)step].SetActive(false);
							newStep = false;
							step = TutorialSteps.Swimming;
							timeStamp = delay + Time.time;
						}
					}
					break;

					case TutorialSteps.Swimming: // EXPLAIN SWIMMING
					{
						tutorialImages[(int)step].SetActive(true);

						if(player.animationController.lastWaterFrame && Input.GetKey(KeyCode.Q)) Q = true;
						if(player.animationController.lastWaterFrame && Input.GetKey(KeyCode.E)) E = true;
						
					    if (Input.GetKey(skipKey))
                        {
							tutorialImages[(int)step].SetActive(false);
							newStep = false;
							endTutorial = true;
							
							Log.Tutorial(false);
                        }

						if(E && Q)
						{
							tutorialImages[(int)step].SetActive(false);
							newStep = false;
							endTutorial = true;
							
							Log.Tutorial(false);
						}
					}
					break;

					default:
						Debug.Log("Tutorial: Index out of range");
						newStep = false;
						break;
				}

		}
	}
}