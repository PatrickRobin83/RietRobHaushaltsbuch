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

        public static string CalculateAverage(string dividend, string divisor)
        {
            double tmpDividend;
            double tmpDivisor;
            double result;

            tmpDividend = Convert.ToDouble(dividend);
            tmpDivisor = Convert.ToDouble(divisor);

            result = tmpDividend / tmpDivisor * 100;
            result = Math.Round(result, 2);
            
            return result.ToString("N2");
        }

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