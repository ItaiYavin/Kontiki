using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Kontiki
{
    public static class Log {
        
        private static Dictionary<uint, Log.Data> logData   = new Dictionary<uint, Log.Data>();
        private static Dictionary<Quest, uint> questLog     = new Dictionary<Quest, uint>();
        private static Dictionary<Character, uint> charLog  = new Dictionary<Character, uint>();
        
        private static uint waterBaseEntry = 0;
        private static uint lastWaterEntry = 0;
        
        private static int randomSeed;
        private static UrgencyLevel urgencyLevel;
        
        private static uint uidCount = 0;
        private static uint uidOffset = 8;
        
        private static uint cdBaseEntry = 0;
        private static uint cdLastEntry = 0;
        
        //Game Methods
        public static void Game_Started(){
            //date?
        }
        
        public static void CD(bool wantToContinue, string explaination){
            CDData data = new CDData(wantToContinue, explaination);
            PlaceCDIntoDictionary(data);
        }
        
        public static void UrgencyLevel(UrgencyLevel level){
            urgencyLevel = level;
        }
        
        //Player methods
        public static void Player_IntoWater(){
            WaterData data = new WaterData(true);
            PlaceWaterIntoDictionary(data);
        }
        public static void Player_OutOfWater(){
            WaterData data = new WaterData(false);
            PlaceWaterIntoDictionary(data);
        }
       
        //Interaction Methods
        
        public static void Interaction_PlayerAsking(Language.Topic topic, Character interactee){
            Log.InteractionData data = new InteractionData(topic, interactee, true);
            
            PlaceInteractionIntoDictionary(data);
        }
        
        public static void Interaction_PlayerGotReaction(Language.Topic topic, Character interactee){
            Log.InteractionData data = new InteractionData(topic, interactee, false);
            
            PlaceInteractionIntoDictionary(data);
        }
        
        
        //Quest methods
        public static void Quest_Accepted(Quest quest){
            Log.QuestData data = new QuestData(quest, QuestData.Topic.Accepted);
            
            PlaceQuestIntoDictionary(data);
        }
        
        public static void Quest_Completed(Quest quest){
            Log.QuestData data = new QuestData(quest, QuestData.Topic.Completed);
            
            PlaceQuestIntoDictionary(data);
        }
        
        public static void Quest_Objective(Quest quest){
            Log.QuestData data = new QuestData(quest, QuestData.Topic.GotObjective);
            
            PlaceQuestIntoDictionary(data);
        }
        
        public static void Quest_Information(Quest quest, bool hadInfo){
            Log.QuestData data = new QuestData(quest, QuestData.Topic.GotInformation);
            
            PlaceQuestIntoDictionary(data);
        }
        
        
        //Helpers
        
        private static void PlaceQuestIntoDictionary(Log.QuestData data){
            Quest quest = data.quest;   
            
            uint uid;
            if(questLog.ContainsKey(quest)){
                uid = questLog[quest];
                
                //find next entry that is empty
                for (uint i = 0; i < uidOffset; i++)
                {
                    if(!logData.ContainsKey(uid + i)){
                        uid = uid + i;
                        break;
                    }
                }
            }else{
                uid = uidCount++ * uidOffset;
                questLog[quest] = uid;
            }
            logData[uid] = data;  
        }
        
        private static void PlaceInteractionIntoDictionary(Log.InteractionData data){
            Character c = data.interactee;   
            
            uint uid;
            if(charLog.ContainsKey(c)){
                uid = charLog[c];
                
                //find next entry that is empty
                for (uint i = 0; i < uidOffset; i++)
                {
                    if(!logData.ContainsKey(uid + i)){
                        uid = uid + i;
                        break;
                    }
                }
            }else{
                uid = uidCount++ * uidOffset;
                charLog[c] = uid;
            }
            logData[uid] = data;  
        }
        
        private static void PlaceWaterIntoDictionary(Log.WaterData data){
            
            uint uid = 1 + lastWaterEntry;
            bool found = false;
            
             if(logData.ContainsKey(waterBaseEntry) && !(logData[waterBaseEntry] is WaterData)){
                    uid = uidCount++ * uidOffset;
                    waterBaseEntry = uid;
            }
            
            while(!found){
                if(uid - waterBaseEntry >= uidOffset){
                    //find next baseOffset
                    uid = uidCount++ * uidOffset;
                    waterBaseEntry = uid;
                }else if(!logData.ContainsKey(uid)){
                    found = true;
                }else{
                    uid++;
                }
                
            }
            lastWaterEntry = uid;
            logData[uid] = data;  
        }    
        private static void PlaceCDIntoDictionary(Log.CDData data){
            
            uint uid = 1 + cdLastEntry;
            bool found = false;
            
            if(logData.ContainsKey(cdBaseEntry) && !(logData[cdBaseEntry] is CDData)){
                    uid = uidCount++ * uidOffset;
                    cdBaseEntry = uid;
            }
            
            while(!found){
                if(uid - cdBaseEntry >= uidOffset){
                    //find next baseOffset
                    uid = uidCount++ * uidOffset;
                    cdBaseEntry = uid;
                }else if(!logData.ContainsKey(uid)){
                    found = true;
                }else{
                    uid++;
                }
                
            }
            cdLastEntry = uid;
            logData[uid] = data;  
        }
        
        
        
        public static string ConvertToBase64(uint i)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            StringBuilder sb = new StringBuilder();

            do
            {
                sb.Insert(0, alphabet[(int)(i % 64)]);
                i = i / 64;
            } while (i != 0);
            return sb.ToString();
        }
        
        //Data Types 
      
        public class Data{
            public float gameTime;
            public float realTime;
            
            
            public Data(){
                gameTime = Time.time;
                realTime = Time.unscaledTime;
            }
        }
        
        public class CDData:Data{
            public bool wantToContinue;
            public string explaination;
            
            public CDData(bool wantToContinue, string explaination){
                this.wantToContinue = wantToContinue;
                this.explaination = explaination;
            }
        }
        
        public class QuestData:Data{
            public Quest quest;
            public Topic topic;
            
            public QuestData(Quest quest, Topic topic)
                :base(){
                    
                this.quest = quest;
                this.topic = topic;
            }
            
            public enum Topic{
                Accepted,
                GotInformation,
                Completed,
                GotObjective
            }
        }
        public class InteractionData:Data{
            
            public Language.Topic languageTopic;
            public bool playerIsAsking = false;
            public Character interactee;
            
            public float hunger;
            public float social;
            public float energy;
            
            public InteractionData(Language.Topic languageTopic, Character interactee, bool playerIsAsking)
                :base(){
                this.interactee = interactee;
                this.playerIsAsking = playerIsAsking;
                this.languageTopic = languageTopic;
                this.hunger = interactee.hunger;
                this.social = interactee.social;
                this.energy = interactee.energy;
            }
        }
        public class WaterData:Data{
            public bool jumpedIntoWater = false;
            
            public WaterData(bool jumpedIntoWater){
                this.jumpedIntoWater = jumpedIntoWater;
            }
        }
    }
}
