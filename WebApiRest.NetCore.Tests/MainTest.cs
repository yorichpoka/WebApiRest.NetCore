using System;
using System.Collections.Generic;
using System.Xml;
using Xunit;

namespace WebApiRest.NetCore.Tests
{
    public class MainTest
    {
        [Fact]
        public void Method1Test()
        {
            int i = 1;
            object number = (object)i;
        }
    }

    public class LogParser
    {
        public static IEnumerable<int> GetIdsByMessage(string xml, string message)
        {
            var result = new List<int>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            foreach (XmlElement node in xmlDoc.ChildNodes[1].ChildNodes)
            {
                if (node.FirstChild.FirstChild.InnerText == message)
                {
                    result.Add(int.Parse(node.Attributes["id"].Value));
                }
            }

            return result;
        }

        [Fact]
        public void MainTest()
        {
            String xml =
                    "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                    "<log>\n" +
                    "    <entry id=\"1\">\n" +
                    "        <message>Application started</message>\n" +
                    "    </entry>\n" +
                    "    <entry id=\"2\">\n" +
                    "        <message>Application ended</message>\n" +
                    "    </entry>\n" +
                    "</log>";

            foreach (int id in LogParser.GetIdsByMessage(xml, "Application ended"))
                Console.WriteLine(id);
        }
    }
}