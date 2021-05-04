/*
*----------------------------------------------------------------------------------
*          Filename:	CreateYearEntrysForComboBoxSelection.cs
*          Date:        2021.04.28 21:54:21
*          All rights reserved
*
*----------------------------------------------------------------------------------
* @author Patrick Robin <support@rietrob.de>
*/

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RietRobHaushaltbuch.Core.Helper

{
    public static class CreateYearEntrysForComboBoxSelection
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods
        /// <summary>
        /// Creates a list of strings with the Year from now back to 100 Years
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<string> AddYearsToComboBox()
        {
            ObservableCollection<string> YearsToSelect = new ObservableCollection<string>();
            YearsToSelect.Add("Show All");
            for (int i = 0; i <= 100; i++)
            {
                YearsToSelect.Add((DateTime.Now.Year - i).ToString());
            }

            return YearsToSelect;
        }

        #endregion

        #region EventHandler

        #endregion


    }
}