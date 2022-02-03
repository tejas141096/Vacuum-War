using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace VWPrototype
{
    public class VacuumGunRFID : AVacuumGunFunction
    {
        // Start is called before the first frame update
        public SerializableDictionary<string,HeadMode> UIDList;
        public bool AutoResetHead = false;
        public float SecondsToResetHead = 1.5f;
        private Coroutine resetTimer = null;
        new void Start()
        {
            base.Start();

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMessageArrived(byte[]message) {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in message)
                sb.AppendFormat($"{(char)b}");
                //sb.AppendFormat("(#{0}={1})    ", b, (char)b);
            Debug.Log("Received Card UID: " + sb);
            SwitchHeadByUid(sb.ToString());
            if (AutoResetHead) {
                if (resetTimer != null)
                {
                    StopCoroutine(resetTimer);
                }
                resetTimer = StartCoroutine(ResetHead(SecondsToResetHead));
            }
        }

        IEnumerator ResetHead(float time) {
            yield return new WaitForSecondsRealtime(time);
            Debug.Log("Reset Head to Empty");
            gunFrame.SwitchHead(HeadMode.Empty);
            resetTimer = null;
        }

        public void SwitchHeadByUid(string uid) {
            HeadMode newMode;
            if (UIDList.TryGetValue(uid,out newMode)) {
                Debug.Log($"Switch Head to {newMode.ToString()}");
                gunFrame.SwitchHead(newMode);
            }
        }
    }
}