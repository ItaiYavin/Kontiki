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
        
        
        private static uint uidCount = 0;
        private static uint uidOffset = 256;
        
        
        private static int randomSeed;
        private static UrgencyLevel urgencyLevel;

        //Game Methods
        public static void Game_Started(){
            //date?
        }
        
        public static void SendLog(){
            Debug.Log("Started Sending Log");
            string[] values;
            
            foreach(var data in logData){
                values = new string[20];
                values[1] = DataDistributor.id.ToString();
                values[2] = data.Value.gameTime.ToString();
                values[3] = data.Value.realTime.ToString();
                values[4] = data.Key.ToString();
                values[5] = data.Value.type.ToString();
                
                if(data.Value is QuestData){
                    values[6] = (data.Key / uidOffset * uidOffset).ToString();
                    values[7] = ((QuestData)data.Value).quest.origin.name;
                    values[8] = ((QuestData)data.Value).topic.ToString();
                                        
                }
                if(data.Value is InteractionData){
                    InteractionData iData = (InteractionData)data.Value;
                    values[9] = iData.interactee.name;
                    values[10] = iData.languageTopic.ToString();
                    values[11] = iData.playerIsAsking.ToString();
                    values[12] = iData.energy.ToString();
                    values[13] = iData.hunger.ToString();
                    values[14] = iData.social.ToString();
                }
                
                UnityDataConnector.Instance.SendLogData(values);
            }
            
            for (int i = 0; i < waterLog.Count; i++)
            {
                values = new string[20];
                values[1] = DataDistributor.id.ToString();
                values[2] = waterLog[i].gameTime.ToString();
                values[3] = waterLog[i].realTime.ToString();
                values[5] = waterLog[i].type.ToString();
                
                values[15] = waterLog[i].jumpedIntoWater.ToString();
                UnityDataConnector.Instance.SendLogData(values);
            }
            
            for (int i = 0; i < cdLog.Count; i++)
            {
                values = new string[20];
                values[1] = DataDistributor.id.ToString();
                values[2] = cdLog[i].gameTime.ToString();
                values[3] = cdLog[i].realTime.ToString();
                values[5] = cdLog[i].type.ToString();
                
                values[16] = cdLog[i].wantToContinue.ToString();
                values[17] = cdLog[i].explaination;
                UnityDataConnector.Instance.SendLogData(values);
            }
            values = new string[20];
            values[1] = DataDistributor.id.ToString();
            values[5] = "Log Check Sum";
            values[18] = (UnityDataConnector.Instance.logsSend + 1).ToString();
            
            UnityDataConnector.Instance.SendLogData(values);
        }
        
        public static void CD(bool wantToContinue, string explaination){
            CDData data = new CDData(wantToContinue, explaination);
            cdLog.Add(data);
        }
        
        public static void UrgencyLevel(UrgencyLevel level){
            urgencyLevel = level;
        }
        
        public static void Tutorial(bool tutorialStarted){
            uint uid = uidCount++ * uidOffset;
            Data d = new Data();
            d.type = (tutorialStarted ? Data.Type.Tutorial_Started : Data.Type.Tutorial_Ended);
            logData[uid] = d;
        }
        
        //Player methods
        public static void Player_IntoWater(){
            WaterData data = new WaterData(true);
            waterLog.Add(data);
        }
        public static void Player_OutOfWater(){
            WaterData data = new WaterData(false);
            waterLog.Add(data);
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
            Log.QuestData data = new QuestData(quest, (hadInfo ? QuestData.Topic.GotInformation : QuestData.Topic.GotNoInformation));
            
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
