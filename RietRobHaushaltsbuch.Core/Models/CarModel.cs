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
        public string Car { get;}
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
            Car = $"{Brand.BrandName} {ModelType.ModelName}";
        }
        #endregion

        #region Methods

        #endregion

        #region EventHandler

        #endregion


    }
}