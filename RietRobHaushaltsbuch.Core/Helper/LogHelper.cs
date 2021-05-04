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
using NLog;
using RietRobHaushaltbuch.Core.Enum;

namespace RietRobHaushaltbuch.Core.Helper

{
    public class LogHelper
    {
        #region Fields

        /// <summary>
        /// LogFileName to Log to
        /// </summary>
        private static string LogFileName { get; } = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + 
                                                     @"\RietRobHaushaltsbuch\Logs\" + DateTime.Now.ToShortDateString() + @"_log.txt";
        /// <summary>
        /// Path to the LogFile
        /// </summary>
        private static string LogDirectory { get; } =
            Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) +
            @"\RietRobHaushaltsbuch\Logs\";

        #endregion

        #region Properties


        #endregion

        #region Constructor

        #endregion

        #region Methods

        /// <summary>
        /// Writes the Logfile and fill some standard information at startup. The LogFile will be created if it is existent 
        /// </summary>
        public static void WriteLogOnStartup()
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }

                if (!File.Exists(LogFileName))
                {
                    using (FileStream fs = new FileStream(LogFileName, FileMode.Create, FileAccess.ReadWrite))
                    {
                        fs.Close();
                        fs.Dispose();
                    }
                }
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.Message);
            }

            bool notLocked = false;
            while (!notLocked)
            {
                try
                {
                    FileStream fs = File.Open(LogFileName, FileMode.OpenOrCreate,
                        FileAccess.ReadWrite, FileShare.None);
                    fs.Close();

                    notLocked = true;
                }
                catch (IOException ex)
                {
                    notLocked = false;
                    throw new IOException(ex.StackTrace);
                }
            }

            using (StreamWriter sw = File.AppendText(LogFileName))
            {
                    sw.WriteLine("--------------------------------------------------------------------------------");
                    sw.WriteLine(
                        $"Date / Time = {DateTime.Today.ToShortDateString()} / {DateTime.Now.ToShortTimeString()}");
                    sw.WriteLine($"RietRobHaushaltsbuch Version {typeof(LogHelper).Assembly.GetName().Version}");
                    sw.WriteLine($"Installationpath = {Environment.CurrentDirectory}");
                    sw.WriteLine($"Computername = {Environment.MachineName}");
                    sw.WriteLine($"OS Version = {Environment.OSVersion}");
                    sw.WriteLine($"Username LoggedIn = {Environment.UserName}");
                    sw.WriteLine($"OS is 64 Bit = {Environment.Is64BitOperatingSystem}");
                    sw.WriteLine("--------------------------------------------------------------------------------");
                    sw.Close();
                    sw.Dispose();
            }

        }

        /// <summary>
        /// Writes the given string and LogState to the LogFile 
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logState"></param>
        public static void WriteToLog(NLog.Logger logger, string logMessage, LogState logState)
        {

            //ToDO: Configure and Style the LogMessage output in LogFile
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = $"{LogFileName}",
                Layout = "${longdate} || ${level} || ${logger} ||  ${message} ${exception}"
            };

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);


            // Apply config           
            NLog.LogManager.Configuration = config;

            switch (logState)
            {

                case LogState.Debug:
                    {
                        logger.Debug(logMessage);
                        break;
                    }
                case LogState.Info:
                    {
                        logger.Info(logMessage);
                        break;
                    }
                case LogState.Warn:
                    {
                        logger.Warn(logMessage);
                        break;
                    }
                case LogState.Error:
                    {
                        logger.Error(logMessage);
                        break;
                    }
                case LogState.Fatal:
                    {
                        logger.Fatal(logMessage);
                        break;
                    }
                default:
                    {
                        logger.Debug(logMessage);
                        break;
                    }
            }
        }
        #endregion

        #region EventHandler

        #endregion


    }
}