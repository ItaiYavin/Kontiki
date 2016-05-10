using System.Diagnostics;
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

	        if(!stop){ // Start talking
	        	self.isTalking = true;
	        	self.transform.LookAt(self.socialPartner.transform, Vector3.up);
	        } else {
	        	self.isTalking = false;
	        }

		}
	}
}