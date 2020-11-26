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

using System.Windows.Controls;
using Prism.Events;

namespace RietRobHaushaltbuch.Core.Interfaces
{
    public interface IViewModelHelper
    {
        void  RegisterCommands();
        
    }
}