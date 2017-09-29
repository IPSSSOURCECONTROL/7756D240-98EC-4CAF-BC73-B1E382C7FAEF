using System;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
            this.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        }

        /// <summary>
        /// Entity ID. The Getter will generate a new Id if the Setter has 
        /// not been called to set the Id.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id
        {
            get
            {
                return this._id ?? (this._id = MongoDB.Bson.ObjectId.GenerateNewId().ToString());
            }
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

        public string ApplyGrammerToTypeName<TDomainType>() where TDomainType: class
        {
            string input = typeof(TDomainType).Name;

            if (input.Contains("_"))
            {
                return input.Replace('_', ' ');
            }
            else
            {
                StringBuilder newString = new StringBuilder();
                foreach (Char char1 in input)
                {
                    if (char.IsUpper(char1))
                        newString.Append(new char[] { ' ', char1 });
                    else
                        newString.Append(char1);
                }

                newString.Remove(0, 1);
                return newString.ToString();
            }
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