/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   IViewModel.cs
 *   Date			:   2020-11-25 11:13:36
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

namespace RietRobHaushaltbuch.Core.Interfaces
{
    /// <summary>
    /// Interface every ViewModel should implement to get the RegisterCommands method
    /// </summary>
    public interface IViewModelHelper
    {
        void  RegisterCommands();
    }
}