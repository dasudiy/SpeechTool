using System.Collections.Generic;
using System.Xml.Linq;

namespace TTSTool.Classes
{
    public class SSMLHelper
    {
        public static XElement BuildBookmark(string mark, params object[] parameters) => new XElement("bookmark", new List<object>(parameters) { new XAttribute("mark", mark) });
        public static XElement BuildVoice(string name, params object[] parameters) => new XElement("voice", new List<object>(parameters) { new XAttribute("name", name) });
        public static XElement BuildSpeak(params object[] parameters) => new XElement((XNamespace)"http://www.w3.org/2001/10/synthesis" + "speak", new List<object>(parameters) { new XAttribute("version", "1.0"), new XAttribute(XNamespace.Xml + "lang", "zh-CN") });
        public static XElement BuildExpressAs(string style, float? styledegree = 1, string role = null, params object[] parameters) => new XElement("mstts:express-as", new List<object>(parameters) {
            new XAttribute("style", style),
            new XAttribute("styledegree", styledegree),
            new XAttribute("role", role)
        });
    }
}
