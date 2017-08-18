using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes
{
    /// <summary>
    /// To be used on Unit Test Functions.
    /// When applied to a unit test method, it will drop and recreate the database. 
    /// So in effect, each unit test method will have its own database version 
    /// to execute against.
    /// </summary>
    public class OwnDatabaseContextAttribute: Attribute
    {
    }
}
