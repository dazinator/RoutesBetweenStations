namespace Model.Core
{
    /// <summary>
    /// An abstract base class implementation of <see cref="INodeFactory"/>.
    /// </summary>
    public abstract class NodeFactory : INodeFactory
    {
        /// <summary>
        /// Creates an instance of a node.
        /// </summary>
        /// <param name="name">The name of the node.</param>
        /// <returns>The node.</returns>
        public abstract Node CreateNode(string name);
    }
}