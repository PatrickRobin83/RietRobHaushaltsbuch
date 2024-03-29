﻿/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   AverageCalculator.cs
 *   Date			:   2020-11-25 14:40:23
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using RietRobHaushaltbuch.Core.Models;

namespace RietRobHaushaltbuch.Core.Helper
{
    /// <summary>
    /// Calculates averages
    /// </summary>
    public static class AverageCalculator
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods

        /// <summary>
        /// Calculates the average price per liter for the given entryModels Collection
        /// </summary>
        /// <param name="entryModels"></param>
        /// <returns></returns>
        public static string CalculateAveragePricePerLiter(ObservableCollection<EntryModel> entryModels)
        {
            string _averagePricePerLiter = "";

            float tmpPricePerLiter = 0;
            foreach (EntryModel entryModel in entryModels)
            {
                string tmp = entryModel.PricePerLiter;
                float tmpPrice = (float)Convert.ToDouble(tmp, CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                tmpPricePerLiter += tmpPrice;
            }
            if (!double.IsNaN(Math.Round(tmpPricePerLiter / entryModels.Count, 3)))
            {
                _averagePricePerLiter = Convert.ToString(Math.Round(tmpPricePerLiter / entryModels.Count, 3) + " €");
            }
            else
            {
                _averagePricePerLiter = "0 €";
            }

            _averagePricePerLiter = _averagePricePerLiter.Replace(".", ",");

            return _averagePricePerLiter;
        }

        /// <summary>
        /// Calculates the total amount of fuel for the given entrymodels collection
        /// </summary>
        /// <param name="entryModels"></param>
        /// <returns></returns>
        public static string CalculateTotalFuelAmount(ObservableCollection<EntryModel> entryModels)
        {
            string totalFuelAmount = "";
            
            double tmpTotalFuelAmount = 0;
            foreach (EntryModel entryModel in entryModels)
            {
                string tmp = entryModel.AmountOffuel;
                double tmpAmount = Convert.ToDouble(tmp, CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                tmpTotalFuelAmount += tmpAmount;
            }
            if (!double.IsNaN(Math.Round(tmpTotalFuelAmount, 2)))
            {
                totalFuelAmount = Convert.ToString(Math.Round(tmpTotalFuelAmount, 2) +" l");
            }
            else
            {
                totalFuelAmount = "0 l";
            }

            totalFuelAmount = totalFuelAmount.Replace(".", ",");

            return totalFuelAmount;
        }
        /// <summary>
        /// Calculates the total refuel costs for the given entryModels Collection
        /// </summary>
        /// <param name="entryModels"></param>
        /// <returns></returns>
        public static string CalculateTotalRefuelCosts(ObservableCollection<EntryModel> entryModels)
        {
            double tmpRefuelCosts = 0;
            string totalRefuelCosts = "";

            foreach (EntryModel entryModel in entryModels)
            {
                tmpRefuelCosts += Convert.ToDouble(entryModel.TotalAmount, CultureInfo.GetCultureInfo("de-DE").NumberFormat);
            }
            if (!double.IsNaN(Math.Round(tmpRefuelCosts, 2)))
            {
                totalRefuelCosts = Convert.ToString(Math.Round(tmpRefuelCosts, 2) + " €");
            }
            else
            {
                totalRefuelCosts = "0 €";
            }

            totalRefuelCosts = totalRefuelCosts.Replace(".", ",");

            return totalRefuelCosts;
        }

        /// <summary>
        /// Calculates the total driven distance for the given entrys collection from selected car 
        /// </summary>
        /// <param name="allEntrysForSelectedCar"></param>
        /// <returns></returns>
        public static string CalculateTotalDrivenDistance(ObservableCollection<EntryModel> allEntrysForSelectedCar)
        {
            double tmpDrivenDistance = 0;
            string totalDrivenDistance = "";

            foreach (EntryModel entryModel in allEntrysForSelectedCar)
            {
                tmpDrivenDistance += Convert.ToDouble(entryModel.DrivenDistance, CultureInfo.GetCultureInfo("de-DE").NumberFormat);
            }
            if (!double.IsNaN(Math.Round(tmpDrivenDistance, 2)))
            {
                totalDrivenDistance = Convert.ToString(Math.Round(tmpDrivenDistance, 2) + " km");
            }
            else
            {
                totalDrivenDistance = "0 km";
            }

            totalDrivenDistance = totalDrivenDistance.Replace(".", ",");

            return totalDrivenDistance;
        }

        /// <summary>
        /// Calculates the total average consumption from the entrys collection for the selected car
        /// </summary>
        /// <param name="allEntrysForSelectedCar"></param>
        /// <returns></returns>
        public static string CalculateTotalAverageConsumption(ObservableCollection<EntryModel> allEntrysForSelectedCar)
        {
            double tmpConsumption = 0;
            string averageConsumption = "";

            foreach (EntryModel entryModel in allEntrysForSelectedCar)
            {
                tmpConsumption += Convert.ToDouble(entryModel.ConsumptationPerHundredKilometer, CultureInfo.GetCultureInfo("de-DE").NumberFormat);
            }
            if (!double.IsNaN(Math.Round(tmpConsumption / allEntrysForSelectedCar.Count, 2)))
            {
                averageConsumption = Convert.ToString(Math.Round(tmpConsumption / allEntrysForSelectedCar.Count, 1) + " l");
            }
            else
            {
                averageConsumption = "0 l";
            }

            averageConsumption = averageConsumption.Replace(".", ",");

            return averageConsumption;
        }

        /// <summary>
        /// Calculate the total average costs of hundred kilometer for the entrys collection for the selected car
        /// </summary>
        /// <param name="allEntrysForSelectedCar"></param>
        /// <returns></returns>
        public static string CalculateTotalAverageCostsOfHundredKilometer(ObservableCollection<EntryModel> allEntrysForSelectedCar)
        {
            double tmpCostsPerHundredKilometer = 0;
            string averageCostsOfHundredKilometer = "";
            foreach (EntryModel entryModel in allEntrysForSelectedCar)
            {
                tmpCostsPerHundredKilometer += Convert.ToDouble(entryModel.CostPerHundredKilometer, CultureInfo.GetCultureInfo("de-DE").NumberFormat);
            }
            if (!double.IsNaN(Math.Round(tmpCostsPerHundredKilometer / allEntrysForSelectedCar.Count, 2)))
            {
                averageCostsOfHundredKilometer = Convert.ToString(Math.Round(tmpCostsPerHundredKilometer / allEntrysForSelectedCar.Count, 2).ToString("N2") + " €");

            }
            else
            {
                averageCostsOfHundredKilometer = "0 €";
            }

            averageCostsOfHundredKilometer = averageCostsOfHundredKilometer.Replace(".", ",");
            return averageCostsOfHundredKilometer;
        }
        #endregion

    }
}