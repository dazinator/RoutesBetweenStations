using System;
using System.Collections.Generic;

namespace Model.Core
{
    /// <summary>
    /// This defines the contract for any repository implementation that will returns the available <see cref="Node"/>'s.
    /// </summary>
    public interface INodesRepository : IDisposable
    {
        IList<Node> GetNodes();
    }
}