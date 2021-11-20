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
        private static string _configFileName { get; } =
            Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) +
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

                        XmlDocument doc = new XmlDocument();
                        XmlDeclaration xmlDec = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                        XmlNode rootNode = doc.CreateElement("root");
                        XmlNode configNode = doc.CreateElement("configsettings");
                        XmlNode languageNode = doc.CreateElement("language");
                        doc.AppendChild(xmlDec);
                        rootNode.AppendChild(configNode);
                        configNode.AppendChild(languageNode);
                        doc.AppendChild(rootNode);
                        fs.Close();
                        fs.Dispose();
                        doc.Save(_configFileName);
                    }
                }

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
            CultureInfo culture = CultureInfo.CurrentCulture;
            string tempCulture = "";
            XmlReader reader = XmlReader.Create(_configFileName);
            while (reader.Read())
            {
                switch (reader.Name.ToString())
                {
                    case "language":
                        tempCulture = reader.ReadElementContentAsString();
                        if (!tempCulture.Equals(culture.ToString()))
                        {
                            culture = new CultureInfo(tempCulture);
                        }

                        break;
                }
            }
            reader.Close();
            reader.Dispose();

            return culture;
        }

        #endregion
    }

}