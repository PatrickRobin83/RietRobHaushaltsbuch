/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ICloseWindows.cs
 *   Date			:   2020-11-24 14:24:10
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;

namespace RietRobHaushaltbuch.Core.Interfaces
{
    public interface ICloseWindows
    {
        Action Close { get; set; }
    }
}