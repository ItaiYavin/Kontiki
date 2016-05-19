using UnityEngine;

namespace Kontiki {
	public class LogTester : MonoBehaviour {
        
        public bool uploadLog = false;
        
        void Update(){
            if(uploadLog){
                uploadLog = false;
                
                Log.SendLog();
            }
        }
    }
}