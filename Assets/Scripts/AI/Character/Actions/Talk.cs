using System.Collections.Generic;
using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI {
	public class Talk : ActionBase {
	    [ApexSerialization, FriendlyName("Stop", "Stop talking")]
	    public bool stop = false;
	    
	    public override void Execute(IAIContext context)
	    {
	        AIContext ai = ((AIContext)context);
	        Character self = ai.character;

	       	LanguageExchanger languageExchanger = self.languageExchanger;
	       	languageExchanger.speakingTo = self.socialPartner.languageExchanger;

	       	Character partner = languageExchanger.speakingTo.transform.GetComponent<Character>();

	        if(!stop){ // Start talking
	        	Language.RandomTalk(languageExchanger, languageExchanger.speakingTo);
	        	self.isTalking = true;
	        	partner.isTalking = true;

		        if(ai.debugAI_Character){
					Debug.Log("Started talking");		        	
		        }

	        } else {
	        	self.wantsToTalk = false;
	        	self.isTalking = false;
	        	partner.isTalking = false;

	        	partner.socialPartner = null;
	        	self.socialPartner = null;


		        if(ai.debugAI_Character){
		     		Debug.Log("Stopped Conversation");   	
		        }
	        }
		}
	}
}