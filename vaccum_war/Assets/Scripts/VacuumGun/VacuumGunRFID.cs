using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        public SerialControllerCustomDelimiter serialController;
        new void Start()
        {
            base.Start();
            Open();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Open(string arguments = null)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            //设置.net的程序路径
            p.StartInfo.FileName = Application.streamingAssetsPath + "/GetSerialPorts.exe";
            p.StartInfo.Arguments = arguments;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.StandardOutputEncoding = Encoding.Default;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.Start();
            StreamReader s = p.StandardOutput;
            p.WaitForExit();
            Manager(s.ReadToEnd());
            s.Close();
        }
        private void Manager(string content)
        {
            //处理接收到的内容
            Debug.Log(content);
            serialController.portName = content;
            serialController.gameObject.SetActive(true);
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