/*
*----------------------------------------------------------------------------------
*          Filename:	BaseSqLiteDataAccess.cs
*          Date:        2020.11.26 14:08:26
*          All rights reserved
*
*----------------------------------------------------------------------------------
* @author Patrick Robin <support@rietrob.de>
*/

using System.Configuration;
using NLog;

namespace RietRobHaushaltbuch.Core.DataAccess.Base

{
    public class BaseSqLiteDataAccess
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructor

        #endregion

        #region Methods
        /// <summary>
        /// Reads the Connection String from the App.config
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Connection String from App.config</returns>
        public static string LoadConnectionString(string id = "CarRefuelTracker")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        #endregion

        #region EventHandler

        #endregion


    }
}