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
using System.Xml.Linq;

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
                //TODO: XDocument instead of XmlDocument;

                XmlDocument doc = new XmlDocument();
                XmlNode rootNode = doc.CreateElement("root");
                XmlNode configNode = doc.CreateElement("configsettings");
                XmlNode languageNode = doc.CreateElement("language");
                languageNode.InnerText = SaveLanguageSettings(CultureInfo.CurrentCulture);
                rootNode.AppendChild(configNode);
                configNode.AppendChild(languageNode);
                doc.AppendChild(rootNode);
                doc.Save(_configFileName);

            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.Message);
            }
        }

        public static string SaveLanguageSettings(CultureInfo cutlure)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_configFileName);
            XmlNode languageNode = doc.SelectSingleNode("root/configsettings/language");
            string language = languageNode.InnerText;

            
            if (language != null && !language.Equals(cutlure.ToString()))
            {
                language = cutlure.ToString();
                languageNode.InnerText = language;
                doc.Save(_configFileName);
            }

            return language;
        }


        public static CultureInfo GetCulture()
        {
            CultureInfo culture = null;
            using (XmlReader reader = XmlReader.Create(_configFileName))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "language":
                                culture = new CultureInfo(reader.ReadElementContentAsString());
                                break;
                        }
                    }
                }
            }

            return culture;
        }
        #endregion

    }
}