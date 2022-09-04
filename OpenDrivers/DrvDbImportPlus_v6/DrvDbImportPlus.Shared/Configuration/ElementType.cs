using System;
using System.Collections.Generic;
using System.Text;

namespace DrvDbImportPlus.Common.Configuration
{
    public class ElementType
    {
        /// <summary>
        /// Decodes by the name of the object into its object type.
        /// </summary>
        public static Type GetElementType(string inputString)
        {

            Type type;
            switch (inputString)
            {
                case "string": type = typeof(string); break;
                case "nvarchar": type = typeof(string); break;
                case "varchar": type = typeof(string); break;
                case "int": type = typeof(int); break;
                case "bigint": type = typeof(long); break;
                case "decimal": type = typeof(decimal); break;
                case "double": type = typeof(double); break;
                case "datetime": type = typeof(DateTime); break;
                case "binary": type = typeof(byte[]); break;
                case "bit": type = typeof(bool); break;
                case "char": type = typeof(string); break;
                case "date": type = typeof(DateTime); break;
                case "datetime2": type = typeof(DateTime); break;
                case "datetimeoffset": type = typeof(DateTimeOffset); break;
                case "float": type = typeof(float); break;
                case "Runtime.sys.geography": type = typeof(object); break;
                case "Runtime.sys.geometry": type = typeof(object); break;
                case "Runtime.sys.hierarchyid": type = typeof(object); break;
                case "image": type = typeof(byte[]); break;
                case "money": type = typeof(decimal); break;
                case "nchar": type = typeof(string); break;
                case "ntext": type = typeof(string); break;
                case "real": type = typeof(float); break;
                case "smalldatetime": type = typeof(DateTime); break;
                case "smallint": type = typeof(short); break;
                case "smallmoney": type = typeof(decimal); break;
                case "sql_variant": type = typeof(object); break;
                case "text": type = typeof(string); break;
                case "time": type = typeof(TimeSpan); break;
                case "timestamp": type = typeof(byte[]); break;
                case "tinyint": type = typeof(byte); break;
                case "uniqueidentifier": type = typeof(Guid); break;
                case "varbinary": type = typeof(byte[]); break;
                case "xml": type = typeof(object); break;
                default: return null;
            }

            return Type.GetType(type.FullName, false, true);
        }
    }
}
