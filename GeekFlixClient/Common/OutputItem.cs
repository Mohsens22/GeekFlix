using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class OutputItem : RealmObject
    {
        //Properties------------------------------------------------------------------

        //StartTime and EndTime times are in milliseconds
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Lines { get; set; }





        // Methods --------------------------------------------------------------------------

        public override string ToString()
        {
            var startTs = new TimeSpan(0, 0, 0, 0, StartTime);
            var endTs = new TimeSpan(0, 0, 0, 0, EndTime);

            var res = string.Format("{0} --> {1}: {2}", startTs.ToString("G"), endTs.ToString("G"), string.Join(Environment.NewLine, Lines));
            return res;
        }

        public int BeforePause { get; set; }
        public int AfterPause { get; set; }
        public int SpeakingTime { get; set; }
        public double SynthRate { get; set; }
        public string OutputPath { get; set; }
        public string Name { get; set; }
    }
}
