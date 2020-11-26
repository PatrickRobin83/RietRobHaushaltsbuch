/*
*----------------------------------------------------------------------------------
*          Filename:	LogHelper.cs
*          Date:        2020.11.26 14:12:53
*          All rights reserved
*
*----------------------------------------------------------------------------------
* @author Patrick Robin <support@rietrob.de>
*/

using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net;
using RietRobHaushaltbuch.Core.Enum;

namespace RietRobHaushaltbuch.Core.Helper

{
    public class LogHelper
    {

        #region Fields

        /// <summary>
        /// Gets the Logger to Log to
        /// </summary>
        private static readonly log4net.ILog Log = LogHelper.GetLogger();

        #endregion

        #region Properties
        /// <summary>
        /// LogFileName to Log to
        /// </summary>
        private static string LogFileName { get; } = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) +
                                                    @"\RietRobHaushaltsbuch\Logs\" + DateTime.Now.ToShortDateString() + @"_log";

        private static string LogDirectory { get; } =
            Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) +
            @"\RietRobHaushaltsbuch\Logs\";

        #endregion

        #region Constructor

        #endregion

        #region Methods
        /// <summary>
        /// Gets the Logger and overgive a filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            return LogManager.GetLogger(filename);
        }

        /// <summary>
        /// Writes the Logfile and fill some stadard information at startup.
        /// </summary>
        public static void WriteLogOnStartup()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
            if (!File.Exists(LogFileName + ".txt"))
            {
                File.Create(LogFileName + "txt");
            }

            using (StreamWriter sw = File.AppendText(LogFileName + ".txt"))
            {
                sw.WriteLine("--------------------------------------------------------------------------------");
                sw.WriteLine($"RietRobHaushaltsbuch Version {typeof(LogHelper).Assembly.GetName().Version}");
                sw.WriteLine($"Installationpath = {Environment.CurrentDirectory}");
                sw.WriteLine($"Computername = {Environment.MachineName}");
                sw.WriteLine($"OS Version = {Environment.OSVersion}");
                sw.WriteLine($"Username LoggedIn = {Environment.UserName}");
                sw.WriteLine($"OS is 64 Bit = {Environment.Is64BitOperatingSystem}");
                sw.WriteLine("--------------------------------------------------------------------------------");
                sw.Close();
            }
        }

        /// <summary>
        /// Writes the given string and LogState to the LogFile 
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logState"></param>
        public static void WriteToLog(string logMessage, LogState logState)
        {
            log4net.GlobalContext.Properties["LogFile"] = LogFileName;
            string s = new Uri(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.
                GetExecutingAssembly().CodeBase), "log4net.config")).LocalPath;
            log4net.Config.XmlConfigurator.Configure();
            switch (logState)
            {
                case LogState.Debug:
                    {
                        Log.Debug(logMessage);
                        break;
                    }
                case LogState.Info:
                    {
                        Log.Info(logMessage);
                        break;
                    }
                case LogState.Warn:
                    {
                        Log.Warn(logMessage);
                        break;
                    }
                case LogState.Error:
                    {
                        Log.Error(logMessage);
                        break;
                    }
                case LogState.Fatal:
                    {
                        Log.Fatal(logMessage);
                        break;
                    }
                default:
                    {
                        Log.Debug(logMessage);
                        break;
                    }
            }
        }
        #endregion

        #region EventHandler

        #endregion


    }
}