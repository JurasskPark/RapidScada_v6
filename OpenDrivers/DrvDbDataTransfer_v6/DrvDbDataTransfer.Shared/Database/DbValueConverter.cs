using System.Data;

namespace Scada.Comm.Drivers.DrvDbDataTransfer
{
    /// <summary>
    /// Converts provider values to stable CLR values before target parameter binding.
    /// <para>Преобразует значения провайдеров к устойчивым CLR-типам перед привязкой параметров.</para>
    /// </summary>
    public static class DbValueConverter
    {
        /// <summary>
        /// Gets a CLR type by a database provider type name.
        /// <para>Возвращает CLR-тип по имени типа провайдера БД.</para>
        /// </summary>
        public static Type GetClrType(string dataTypeName)
        {
            if (string.IsNullOrWhiteSpace(dataTypeName))
            {
                return typeof(object);
            }

            string normalized = dataTypeName.Trim().ToLowerInvariant();

            switch (normalized)
            {
                case "bigint":
                case "int8":
                    return typeof(long);
                case "integer":
                case "int":
                case "int4":
                    return typeof(int);
                case "smallint":
                case "int2":
                    return typeof(short);
                case "numeric":
                case "decimal":
                case "money":
                case "smallmoney":
                    return typeof(decimal);
                case "double precision":
                case "double":
                case "float8":
                    return typeof(double);
                case "real":
                case "float4":
                    return typeof(float);
                case "boolean":
                case "bool":
                case "bit":
                    return typeof(bool);
                case "timestamp":
                case "timestamp without time zone":
                case "datetime":
                case "datetime2":
                case "date":
                case "smalldatetime":
                    return typeof(DateTime);
                case "timestamp with time zone":
                case "timestamptz":
                case "datetimeoffset":
                    return typeof(DateTimeOffset);
                case "uuid":
                case "uniqueidentifier":
                    return typeof(Guid);
                case "text":
                case "string":
                case "varchar":
                case "nvarchar":
                case "char":
                case "nchar":
                case "ntext":
                case "json":
                case "jsonb":
                case "xml":
                    return typeof(string);
                case "bytea":
                case "binary":
                case "varbinary":
                case "image":
                case "timestamp rowversion":
                    return typeof(byte[]);
                case "time":
                    return typeof(TimeSpan);
                default:
                    Type elementType = ElementType.GetElementType(normalized);
                    return elementType ?? typeof(object);
            }
        }

        /// <summary>
        /// Converts a value to a target command parameter value.
        /// <para>Преобразует значение в значение параметра целевой команды.</para>
        /// </summary>
        public static object ConvertForParameter(object value, DataColumn column)
        {
            if (value == null || value == DBNull.Value)
            {
                return DBNull.Value;
            }

            Type targetType = column?.DataType ?? value.GetType();

            if (targetType == typeof(object) || targetType.IsInstanceOfType(value))
            {
                return value;
            }

            if (targetType == typeof(Guid))
            {
                return value is Guid guid ? guid : Guid.Parse(value.ToString());
            }

            if (targetType == typeof(DateTimeOffset))
            {
                if (value is DateTimeOffset dateTimeOffset)
                {
                    return dateTimeOffset;
                }

                if (value is DateTime dateTime)
                {
                    return new DateTimeOffset(dateTime);
                }
            }

            if (targetType == typeof(byte[]))
            {
                return value;
            }

            return Convert.ChangeType(value, targetType);
        }
    }
}
