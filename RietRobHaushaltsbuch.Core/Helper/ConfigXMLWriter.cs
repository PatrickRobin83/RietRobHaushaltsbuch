/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ConfigXMLWriter.cs
 *   Date			:   2021-05-10 14:32:12
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace RietRobHaushaltbuch.Core.Helper
{
    public static class ConfigXMLWriter
    {
        #region Fields

        /// <summary>
        /// ConfigFileName
        /// </summary>
        private static string _configFileName { get; } = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) +
                                                     @"\RietRobHaushaltsbuch\config.xml";
        /// <summary>
        /// Path to the LogFile
        /// </summary>
        private static string _configDirectory { get; } =
            Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) +
            @"\RietRobHaushaltsbuch\";

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public static void CreateXMLFile()
        {
            try
            {
                if (!Directory.Exists(_configDirectory))
                {
                    Directory.CreateDirectory(_configDirectory);
                }

                if (!File.Exists(_configFileName))
                {
                    using (FileStream fs = new FileStream(_configFileName, FileMode.Create, FileAccess.ReadWrite))
                    {
                        fs.Close();
                        fs.Dispose();
                    }
                }

                XmlDocument doc = new XmlDocument();
                XmlNode rootNode = doc.CreateElement("root");
                XmlNode configNode = doc.CreateElement("configsettings");
                XmlNode languageNode = doc.CreateElement("language");
                languageNode.InnerText = CultureInfo.CurrentCulture.Name;
                rootNode.AppendChild(configNode);
                configNode.AppendChild(languageNode);
                doc.AppendChild(rootNode);
                doc.Save(_configFileName);



                //XmlTextWriter xmlWriter = new XmlTextWriter(_configFileName, Encoding.UTF8);
                //xmlWriter.WriteStartDocument(true);
                //xmlWriter.WriteStartElement("root");
                //xmlWriter.WriteStartElement("configsection");
                //xmlWriter.WriteElementString("language", "de-DE");
                //xmlWriter.WriteEndElement();
                //xmlWriter.WriteEndElement();
                //xmlWriter.WriteEndDocument();
                //xmlWriter.Dispose();
                //xmlWriter.Close();
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.Message);
            }
        }

        public static void WriteToXmlFile()
        {

        }

        #endregion

    }
}