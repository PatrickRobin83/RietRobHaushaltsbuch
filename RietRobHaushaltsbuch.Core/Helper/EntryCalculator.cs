/*
*----------------------------------------------------------------------------------
*          Filename:	EntryCalculator.cs
*          Date:        2020.11.26 16:52:31
*          All rights reserved
*
*----------------------------------------------------------------------------------
* @author Patrick Robin <support@rietrob.de>
*/

using System;

namespace RietRobHaushaltbuch.Core.Helper

{
    public static class EntryCalculator
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods
        /// <summary>
        /// Calculates the average consumption of fuel in l for 100 kilometer
        /// </summary>
        /// <param name="amountOfFuel"></param>
        /// <param name="totalDrivenDistance"></param>
        /// <returns></returns>
        public static string CalculateAverageForHundredKilometer(string amountOfFuel, string totalDrivenDistance)
        {
            double dbAmountOfFuel;
            double dbTotalDrivenDistance;
            double result;

            dbAmountOfFuel = Convert.ToDouble(amountOfFuel);
            dbTotalDrivenDistance = Convert.ToDouble(totalDrivenDistance);

            result = dbAmountOfFuel / dbTotalDrivenDistance * 100;
            result = Math.Round(result, 2);
            
            return result.ToString("N2");
        }
        /// <summary>
        /// Calculates the total fuel costs from the given amount of fuel in liter and price per liter €
        /// </summary>
        /// <param name="amountOfFuel"></param>
        /// <param name="pricePerLiter"></param>
        /// <returns></returns>
        public static string CalculateTotalFuelCosts(string amountOfFuel, string pricePerLiter)
        {
            double tmpAmountOfFuel;
            double tmpPricePerLiter;
            double result;

            tmpAmountOfFuel = Convert.ToDouble(amountOfFuel);
            tmpPricePerLiter = Convert.ToDouble(pricePerLiter);
            result = tmpAmountOfFuel * tmpPricePerLiter;
            return result.ToString("N2");
        }

        #endregion

        #region EventHandler

        #endregion


    }
}