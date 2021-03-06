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
        
        
        private static List<WaterData> waterLog             = new List<WaterData>();
        private static List<CDData> cdLog             = new List<CDData>();
        
        public static float inWaterTime = 0;
        public static float lastInTime = 0;
        
        private static uint uidCount = 0;
        private static uint uidOffset = 256;
        
        public static bool properExit = false;
        private static int randomSeed;
        private static UrgencyLevel urgencyLevel;
        
        public static bool shouldSubmit = true;
        
        public static int pcID;

        //Game Methods
       public static void SendID(){
            pcID = Random.Range(int.MinValue,int.MaxValue);

            string[] values = new string[20];
            values[1] = DataDistributor.id.ToString();
            values[5] = "New Participant";
            UnityDataConnector.Instance.SendLogData(values);
       }
        
        public static void SendLog(){
            if(!shouldSubmit)return;
            Debug.Log("Started Sending Log");
            string[] values;
            
            //Water Sum
            values = new string[20];
            values[1] = DataDistributor.id.ToString();
            values[5] = "Water Sum";
            values[15] = inWaterTime.ToString();
            UnityDataConnector.Instance.SendLogData(values);
           
            //Log Check Sum
            values = new string[20];
            values[1] = DataDistributor.id.ToString();
            values[5] = "Log Check Sum";
            
            UnityDataConnector.Instance.SendLogData(values);
		    UnityDataConnector.Instance.SaveID(DataDistributor.id, Settings.urgencyLevel);
        }
        
        public static void CD(bool wantToContinue, string explaination){
            if(!shouldSubmit)return;
            CDData data = new CDData(wantToContinue, explaination);
            cdLog.Add(data);
            
            string[] values = new string[20];
            values[1] = DataDistributor.id.ToString();
            values[2] = data.gameTime.ToString();
            values[3] = data.realTime.ToString();
            values[5] = data.type.ToString();
            
            values[16] = data.wantToContinue.ToString();
            values[17] = data.explaination;
            UnityDataConnector.Instance.SendLogData(values);
        }
        
        public static void Tutorial(bool tutorialStarted){
            if(!shouldSubmit)return;
            uint uid = uidCount++ * uidOffset;
            Data d = new Data();
            d.type = (tutorialStarted ? Data.Type.Tutorial_Started : Data.Type.Tutorial_Ended);
            logData[uid] = d;
            
            
            
            string[] values = new string[20];
            values[1] = DataDistributor.id.ToString();
            values[2] = d.gameTime.ToString();
            values[3] = d.realTime.ToString();
            values[5] = d.type.ToString();
            
            UnityDataConnector.Instance.SendLogData(values);
        }
        
        //Player methods
        public static void Player_IntoWater(){
            if(!shouldSubmit)return;
            WaterData data = new WaterData(true);
            waterLog.Add(data);
            
            string[] values = new string[20];
            values[1] = DataDistributor.id.ToString();
            values[2] = data.gameTime.ToString();
            values[3] = data.realTime.ToString();
            values[5] = data.type.ToString();
            
            if(data.jumpedIntoWater){
                lastInTime = data.gameTime;
            }else{
                inWaterTime += data.gameTime - lastInTime;
            }
            
            values[15] = data.jumpedIntoWater.ToString();
            UnityDataConnector.Instance.SendLogData(values);
        }
        public static void Player_OutOfWater(){
            if(!shouldSubmit)return;
            WaterData data = new WaterData(false);
            waterLog.Add(data);
            
            string[] values = new string[20];
            values[1] = DataDistributor.id.ToString();
            values[2] = data.gameTime.ToString();
            values[3] = data.realTime.ToString();
            values[5] = data.type.ToString();
            
            if(data.jumpedIntoWater){
                lastInTime = data.gameTime;
            }else{
                inWaterTime += data.gameTime - lastInTime;
            }
            
            values[15] = data.jumpedIntoWater.ToString();
            UnityDataConnector.Instance.SendLogData(values);
        }
       
        //Interaction Methods
        
        public static void Interaction_PlayerAsking(Language.Topic topic, Character interactee){
            if(!shouldSubmit)return;
            Log.InteractionData data = new InteractionData(topic, interactee, true);
            
            PlaceInteractionIntoDictionary(data);
        }
        
        public static void Interaction_PlayerGotReaction(Language.Topic topic, Character interactee){
            Log.InteractionData data = new InteractionData(topic, interactee, false);
            
            PlaceInteractionIntoDictionary(data);
        }
        
        
        //Quest methods
        public static void Quest_Accepted(Quest quest){
            if(!shouldSubmit)return;
            Log.QuestData data = new QuestData(quest, QuestData.Topic.Accepted);
            
            PlaceQuestIntoDictionary(data);
        }
        
        public static void Quest_Completed(Quest quest){
            if(!shouldSubmit)return;
            Log.QuestData data = new QuestData(quest, QuestData.Topic.Completed);
            
            PlaceQuestIntoDictionary(data);
        }
        
        public static void Quest_Objective(Quest quest){
            if(!shouldSubmit)return;
            Log.QuestData data = new QuestData(quest, QuestData.Topic.GotObjective);
            
            PlaceQuestIntoDictionary(data);
        }
        
        public static void Quest_Information(Quest quest, bool hadInfo){
            if(!shouldSubmit)return;
            Log.QuestData data = new QuestData(quest, (hadInfo ? QuestData.Topic.GotInformation : QuestData.Topic.GotNoInformation));
            
            PlaceQuestIntoDictionary(data);
        }
        
        
        //Helpers
        
        private static void PlaceQuestIntoDictionary(Log.QuestData data){
            if(!shouldSubmit)return;
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
            
            string[] values = new string[20];
            values[1] = DataDistributor.id.ToString();
            values[2] = data.gameTime.ToString();
            values[3] = data.realTime.ToString();
            values[4] = uid.ToString();
            values[5] = data.type.ToString();
            
            values[6] = (uid / uidOffset * uidOffset).ToString();
            values[7] = ((QuestData)data).quest.origin.name;
            values[8] = ((QuestData)data).topic.ToString();
                                
          
            UnityDataConnector.Instance.SendLogData(values); 
        }
        
        private static void PlaceInteractionIntoDictionary(Log.InteractionData data){
            if(!shouldSubmit)return;
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
            
             
            string[] values = new string[20];
            values[1] = DataDistributor.id.ToString();
            values[2] = data.gameTime.ToString();
            values[3] = data.realTime.ToString();
            values[4] = uid.ToString();
            values[5] = data.type.ToString();
            
            InteractionData iData = (InteractionData)data;
            values[9] = iData.interactee.name;
            values[10] = iData.languageTopic.ToString();
            values[11] = iData.playerIsAsking.ToString();
            values[12] = iData.energy.ToString();
            values[13] = iData.hunger.ToString();
            values[14] = iData.social.ToString();
           
            UnityDataConnector.Instance.SendLogData(values); 
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
            
            public Type type;
            
            
            public Data(){
                gameTime = Time.time;
                realTime = Time.unscaledTime;
            }
            
            public enum Type{
                CD,
                Water,
                Quest,
                Interaction,
                Tutorial_Started,
                Tutorial_Ended
            }
        }
        
        public class CDData:Data{
            public bool wantToContinue;
            public string explaination;
            
            public CDData(bool wantToContinue, string explaination){
                type = Data.Type.CD;
                this.wantToContinue = wantToContinue;
                this.explaination = explaination;
            }
        }
        
        public class QuestData:Data{
            public Quest quest;
            public Topic topic;
            
            public QuestData(Quest quest, Topic topic)
                :base(){
                    
                type = Data.Type.Quest;
                this.quest = quest;
                this.topic = topic;
            }
            
            public enum Topic{
                Accepted,
                GotInformation,
                GotNoInformation,
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
                type = Data.Type.Interaction;
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
                type = Data.Type.Water;
                this.jumpedIntoWater = jumpedIntoWater;
            }
        }
    }
}
