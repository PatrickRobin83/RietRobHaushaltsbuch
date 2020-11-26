/*
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
using RietRobHaushaltbuch.Core.Models;

namespace RietRobHaushaltbuch.Core.Helper
{
    public static class AverageCalculator
    {
        

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public static string CalculateAveragePricePerLiter(ObservableCollection<EntryModel> entryModels)
        {
            string _averagePricePerLiter = "";

            double tmpPricePerLiter = 0;
            foreach (EntryModel entryModel in entryModels)
            {
                tmpPricePerLiter += Convert.ToDouble(entryModel.PricePerLiter);
            }
            if (!double.IsNaN(Math.Round(tmpPricePerLiter / entryModels.Count, 3)))
            {
                _averagePricePerLiter = Convert.ToString(Math.Round(tmpPricePerLiter / entryModels.Count, 3));
            }
            else
            {
                _averagePricePerLiter = "0";
            }
            return _averagePricePerLiter;
        }
        public static string CalculateTotalFuelAmount(ObservableCollection<EntryModel> entryModels)
        {
            string totalFuelAmount = "";

            double tmpTotalFuelAmount = 0;
            foreach (EntryModel entryModel in entryModels)
            {
                tmpTotalFuelAmount += Convert.ToDouble(entryModel.AmountOffuel);
            }
            if (!double.IsNaN(Math.Round(tmpTotalFuelAmount, 2)))
            {
                totalFuelAmount = Convert.ToString(Math.Round(tmpTotalFuelAmount, 2));
            }
            else
            {
                totalFuelAmount = "0";
            }

            return totalFuelAmount;
        }
        public static string CalculateTotalRefuelCosts(ObservableCollection<EntryModel> entryModels)
        {
            double tmpRefuelCosts = 0;
            string totalRefuelCosts = "";

            foreach (EntryModel entryModel in entryModels)
            {
                tmpRefuelCosts += Convert.ToDouble(entryModel.TotalAmount);
            }
            if (!double.IsNaN(Math.Round(tmpRefuelCosts, 2)))
            {
                totalRefuelCosts = Convert.ToString(Math.Round(tmpRefuelCosts, 2));
            }
            else
            {
                totalRefuelCosts = "0";
            }

            return totalRefuelCosts;
        }
        public static string CalculateTotalDrivenDistance(ObservableCollection<EntryModel> allEntrysForSelectedCar)
        {
            double tmpDrivenDistance = 0;
            string totalDrivenDistance = "";

            foreach (EntryModel entryModel in allEntrysForSelectedCar)
            {
                tmpDrivenDistance += Convert.ToDouble(entryModel.DrivenDistance);
            }
            if (!double.IsNaN(Math.Round(tmpDrivenDistance, 2)))
            {
                totalDrivenDistance = Convert.ToString(Math.Round(tmpDrivenDistance, 0));
            }
            else
            {
                totalDrivenDistance = "0";
            }

            return totalDrivenDistance;
        }
        public static string CalculateTotalAverageConsumption(ObservableCollection<EntryModel> allEntrysForSelectedCar)
        {
            double tmpConsumption = 0;
            string averageConsumption = "";

            foreach (EntryModel entryModel in allEntrysForSelectedCar)
            {
                tmpConsumption += Convert.ToDouble(entryModel.ConsumptationPerHundredKilometer);
            }
            if (!double.IsNaN(Math.Round(tmpConsumption / allEntrysForSelectedCar.Count, 2)))
            {
                averageConsumption = Convert.ToString(Math.Round(tmpConsumption / allEntrysForSelectedCar.Count, 1));
            }
            else
            {
                averageConsumption = "0";
            }

            return averageConsumption;
        }
        public static string CalculateTotalAverageCostsOfHundredKilometer(ObservableCollection<EntryModel> allEntrysForSelectedCar)
        {
            double tmpCostsPerHundredKilometer = 0;
            string averageCostsOfHundredKilometer = "";
            foreach (EntryModel entryModel in allEntrysForSelectedCar)
            {
                tmpCostsPerHundredKilometer += Convert.ToDouble(entryModel.CostPerHundredKilometer);
            }
            if (!double.IsNaN(Math.Round(tmpCostsPerHundredKilometer / allEntrysForSelectedCar.Count, 2)))
            {
                averageCostsOfHundredKilometer = Convert.ToString(Math.Round(tmpCostsPerHundredKilometer / allEntrysForSelectedCar.Count, 2).ToString("N2"));

            }
            else
            {
                averageCostsOfHundredKilometer = "0";
            }
            return averageCostsOfHundredKilometer;
        }
        #endregion

    }
}