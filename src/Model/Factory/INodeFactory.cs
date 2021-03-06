﻿namespace RoutesBetweenStations.Model.Factory
{
    /// <summary>
    /// This interface defines the contract for any factory implementation that creates instances of <see cref="Node"/>.
    /// </summary>
    public interface INodeFactory
    {
        Node CreateNode(string name);
    }
}