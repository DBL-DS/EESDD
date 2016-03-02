using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.SceneCheck
{
    class SceneCheck
    {
        public SceneCheck(string sceneName, string normal, string distractA, string distractB, string distractC, string distractD)
        {
            场景名称 = sceneName;
            正常模式 = normal;
            微信语音 = distractA;
            微信短信 = distractB;
            调谐收音机 = distractC;
            行车导航 = distractD;
        }

        public string 场景名称 { get; set; }
        public string 正常模式 { get; set; }
        public string 微信语音 { get; set; }
        public string 微信短信 { get; set; }
        public string 调谐收音机 { get; set; }
        public string 行车导航 { get; set; }
    }
}
