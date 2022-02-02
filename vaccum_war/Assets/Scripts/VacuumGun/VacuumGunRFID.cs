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
        }

        public void SwitchHeadByUid(string uid) {
            HeadMode newMode;
            if (UIDList.TryGetValue(uid,out newMode)) {
                gunFrame.SwitchHead(newMode);
            }
        }
    }
}