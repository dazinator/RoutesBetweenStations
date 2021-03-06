﻿using System;
using System.Collections.Generic;
using RoutesBetweenStations.Model;

namespace RoutesBetweenStations.DataAccess
{
    /// <summary>
    /// This defines the contract for any repository implementation that will returns the available <see cref="Node"/>'s.
    /// </summary>
    public interface INodesRepository
    {
        IList<Node> GetNodes();
    }
}