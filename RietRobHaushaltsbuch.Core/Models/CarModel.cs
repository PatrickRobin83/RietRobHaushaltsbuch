/*
*----------------------------------------------------------------------------------
*          Filename:	CarModel.cs
*          Date:        2020.11.23 20:44:45
*          All rights reserved
*
*----------------------------------------------------------------------------------
* @author Patrick Robin <support@rietrob.de>
*/

using System.Collections.ObjectModel;

namespace RietRobHaushaltbuch.Core.Models

{
    public class CarModel
    {

        #region Fields



        #endregion

        #region Properties 
        /// <summary>
        /// unique identifier 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Indicates if the CarModel is active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Unique identifier for the BrandModel
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>
        /// Unique identifier for the ModelTypeModel 
        /// </summary>
        public int ModelId { get; set; }
        /// <summary>
        /// unique identifier for the FuelTypeModel
        /// </summary>
        public int TypeoffuelId { get; set; }
        /// <summary>
        /// ModelTypeModel for the CarModel
        /// </summary>
        public ModelTypeModel ModelType { get; set; }
        /// <summary>
        /// FuelTypeModel for the CarModel
        /// </summary>
        public FuelTypeModel FuelType { get; set; }
        /// <summary>
        /// BrandModel for the CarModel
        /// </summary>
        public BrandModel Brand { get; set; }
        /// <summary>
        /// List of all Entries for the CarModel
        /// </summary>
        public ObservableCollection<EntryModel> Entries { get; set; }
        /// <summary>
        /// Name of Brand and ModelType for Example= "Toyota Yaris"
        /// </summary>
        public string CarName { get; set; }
        /// <summary>
        /// The average price price in € per liter
        /// </summary>
        public string AveragePricePerLiter { get; set; }
        /// <summary>
        /// The total fuel amount in liter 
        /// </summary>
        public string TotalFuelAmount { get; set; }
        /// <summary>
        /// Total costs for the refueling
        /// </summary>
        public string TotalRefuelCosts { get; set; }
        /// <summary>
        /// The total driven distance in km
        /// </summary>
        public string TotalDrivenDistance { get; set; }
        /// <summary>
        /// The average consumption per 100km
        /// </summary>
        public string AverageConsumption { get; set; }
        /// <summary>
        /// The average costs per 100km
        /// </summary>
        public string AverageCostsOfHundredKilometer { get; set; }


        #endregion

        #region Constructor

        public CarModel()
        {
            Brand = new BrandModel();
            ModelType = new ModelTypeModel();
            FuelType = new FuelTypeModel();
            Brand.Id = BrandId;
            ModelType.Id = ModelId;
            FuelType.Id = TypeoffuelId;
        }
        #endregion

        #region Methods

        #endregion

        #region EventHandler

        #endregion


    }
}