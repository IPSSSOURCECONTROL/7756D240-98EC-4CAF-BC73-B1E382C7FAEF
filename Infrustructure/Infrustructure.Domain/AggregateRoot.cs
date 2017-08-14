using System;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Domain
{
    /// <summary>
    /// Base type for all Domain Entities that are identifiable by a 
    /// unique ID.
    /// </summary>
    public abstract class AggregateRoot: IEntity
    {
        private string _id;
        private string _typeName;

        protected AggregateRoot()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Entity ID. The Getter will generate a new Id if the Setter has 
        /// not been called to set the Id.
        /// </summary>
        public string Id
        {
            get { return this._id ?? (this._id = Guid.NewGuid().ToString()); }
            set { this._id = value; }
        }

        /// <summary>
        /// Returns strongly typed name of a domain entity.
        /// </summary>
        public string TypeName
        {
            get
            {
                if(string.IsNullOrWhiteSpace(this._typeName))
                    return this.GetTypeName();

                return this._typeName;
            }
        }

        /// <summary>
        /// Compares two <see cref="AggregateRoot"/>'s by Id.
        /// </summary>
        /// <param name="obj">The <see cref="AggregateRoot"/></param>
        /// <returns>True if they're equal by Id, false if not equal by Id.</returns>
        public override bool Equals(object obj)
        {
            AggregateRoot entity = obj as AggregateRoot;

            if (entity == null)
            {
                return false;
            }

            return entity.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return (this.Id != null ? this.Id.GetHashCode() : 0);
        }

        protected abstract string GetTypeName();

        protected virtual TAggregateRoot BuildNew<TAggregateRoot>() where TAggregateRoot : AggregateRoot
        {
            return default(TAggregateRoot);
        }
    }
}