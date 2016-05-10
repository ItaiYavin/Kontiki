using System.Diagnostics;
using System.Collections.Generic;
using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Goes to the specified destination
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

	public class FindSocialPartner : ActionBase 
	{
        [ApexSerialization, FriendlyName("Search Radius", "Threshold(0-1) for hunger(0-1) where the score will be 0")]
        public float searchRadius = 0;
        
        public override void Execute(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            Character self = ai.character;

            self.wantsToTalk = true;

            Collider[] colliders = Physics.OverlapSphere(ai.transform.position, Settings.scanningRange);

            // Look through all colliders and Look for Characters and put them in a list
            List<Character> charactersInRange = new List<Character>();
            foreach (Collider c in colliders)
            {
                Character foundCharacter = c.GetComponent<Character>();
                if (foundCharacter != null && !foundCharacter.isPlayer)
                {
                    charactersInRange.Add(foundCharacter);
                }
            }

            for(int i = 0; i < charactersInRange.Count; i++)
            {
            	if(charactersInRange[i].socialPartner == null && charactersInRange[i].wantsToTalk)
            	{
            		charactersInRange[i].socialPartner = self;
            		self.socialPartner = charactersInRange[i];
            	}
            }
        }
	}
}