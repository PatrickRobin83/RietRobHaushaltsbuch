/*
*----------------------------------------------------------------------------------
*          Filename:	FuelTypeModel.cs
*          Date:        2020.11.23 21:13:10
*          All rights reserved
*
*----------------------------------------------------------------------------------
* @author Patrick Robin <support@rietrob.de>
*/

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.Models

{
    public class FuelTypeModel
    {

        #region Fields

        #endregion

        #region Properties
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the FuelType
        /// </summary>
        public string TypeOfFuel { get; set; }

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public override string ToString()
        {
            return TypeOfFuel;
        }

        #endregion

    }
}