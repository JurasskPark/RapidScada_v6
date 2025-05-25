using Scada;
using Scada.Comm.Drivers.DrvFreeDiskSpaceJP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using static Scada.Comm.Drivers.DrvFreeDiskSpaceJP.Tag;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    #region Class ParserTextGroupTag
    public class DriverGroupTag
    {
        public DriverGroupTag()
        {
            Group = new List<DriverTag>();
        }

        #region Группа тегов
        public List<DriverTag> Group { get; set; }
        #endregion Группа тегов
    }
    #endregion Class ParserTextGroupTag

    #region Class DriverTag
    public class DriverTag
    {
        public DriverTag()
        {
            TagID = Guid.NewGuid();
            TagName = string.Empty;
            TagCode = string.Empty;
            TagAddressNumberBlock = string.Empty;
            TagAddressNumberLine = string.Empty;
            TagAddressNumberParameter = string.Empty;
            TagDescription = string.Empty;
            TagEnabled = true;
            TagDataValue = new object();
            TagDateTime = DateTime.MinValue;
            TagNumberDecimalPlaces = 3;

            DeviceTagsBasedRequestedTableColumns = false;
            ColumnNames = string.Empty;
            ColumnNameTag = string.Empty;
            ColumnNameValue = string.Empty;
            ColumnNameValueFormat = Tag.FormatTag.Float;
            ColumnNameValueNumberDecimalPlaces = 3;
            ColumnNameDatetime = string.Empty;
        }

        #region Тег
        private Guid tagID;
        public Guid TagID
        {
            get { return tagID; }
            set { tagID = value; }
        }

        private string tagAddressNumberBlock;
        public string TagAddressNumberBlock
        {
            get { return tagAddressNumberBlock; }
            set { tagAddressNumberBlock = value; }
        }

        private string tagAddressNumberLine;
        public string TagAddressNumberLine
        {
            get { return tagAddressNumberLine; }
            set { tagAddressNumberLine = value; }
        }

        private string tagAddressNumberParameter;
        public string TagAddressNumberParameter
        {
            get { return tagAddressNumberParameter; }
            set { tagAddressNumberParameter = value; }
        }

        private string tagName;
        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
        }

        private string tagCode;
        public string TagCode
        {
            get { return tagCode; }
            set { tagCode = value; }
        }

        private string tagDescription;
        public string TagDescription
        {
            set { tagDescription = value; }
            get { return tagDescription; }
        }

        private bool tagEnabled;
        public bool TagEnabled
        {
            set { tagEnabled = value; }
            get { return tagEnabled; }
        }

        private object tagDataValue;
        public object TagDataValue
        {
            set { tagDataValue = value; }
            get { return tagDataValue; }
        }

        private DateTime tagDateTime;
        public DateTime TagDateTime
        {
            set { tagDateTime = value; }
            get { return tagDateTime; }
        }


        private FormatData tagFormatData;
        public FormatData TagFormatData
        {
            set { tagFormatData = value; }
            get { return tagFormatData; }
        }

        public enum FormatData
        {
            Float = 0,
            DateTime = 1,
            String = 2,
            Integer = 3,
            Boolean = 4,
            Table = 5,
            TableSQL = 6,
        }

        private int tagNumberDecimalPlaces;
        public int TagNumberDecimalPlaces
        {
            set { tagNumberDecimalPlaces = value; }
            get { return tagNumberDecimalPlaces; }
        }


        private bool deviceTagsBasedRequestedTableColumns;
        public bool DeviceTagsBasedRequestedTableColumns
        {
            set { deviceTagsBasedRequestedTableColumns = value; }
            get { return deviceTagsBasedRequestedTableColumns; }
        }

        private string columnNames;
        public string ColumnNames
        {
            set { columnNames = value; }
            get { return columnNames; }
        }

        private string columnNameTag;
        public string ColumnNameTag
        {
            set { columnNameTag = value; }
            get { return columnNameTag; }
        }

        private string columnNameValue;
        public string ColumnNameValue
        {
            set { columnNameValue = value; }
            get { return columnNameValue; }
        }

        private FormatTag columnNameValueFormat;
        public FormatTag ColumnNameValueFormat
        {
            set { columnNameValueFormat = value; }
            get { return columnNameValueFormat; }
        }

        private int columnNameValueNumberDecimalPlaces;
        public int ColumnNameValueNumberDecimalPlaces
        {
            set { columnNameValueNumberDecimalPlaces = value; }
            get { return columnNameValueNumberDecimalPlaces; }
        }

        private string columnNameDatetime;
        public string ColumnNameDatetime
        {
            set { columnNameDatetime = value; }
            get { return columnNameDatetime; }
        }

        #endregion Тег

        #region Xml
        /// <summary>
        /// Loads the settings from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
                throw new ArgumentNullException("xmlNode");

            TagID = DriverUtils.StringToGuid(xmlNode.GetChildAsString("TagID"));
            TagName = xmlNode.GetChildAsString("TagName");
            TagCode = xmlNode.GetChildAsString("TagCode");
            try
            {
                TagFormatData = (DriverTag.FormatData)Enum.Parse(typeof(DriverTag.FormatData), xmlNode.GetChildAsString("TagFormatData"));
            }
            catch { TagFormatData = FormatData.String; }


            TagNumberDecimalPlaces = xmlNode.GetChildAsInt("TagNumberDecimalPlaces");
            TagAddressNumberBlock = xmlNode.GetChildAsString("TagAddressNumberBlock");
            TagAddressNumberLine = xmlNode.GetChildAsString("TagAddressNumberLine");
            TagAddressNumberParameter = xmlNode.GetChildAsString("TagAddressNumberParametr");
            TagDescription = xmlNode.GetChildAsString("TagDescription");
            TagEnabled = xmlNode.GetChildAsBool("TagEnabled");
            TagDataValue = new object();
            TagDateTime = DateTime.MinValue;

            DeviceTagsBasedRequestedTableColumns = xmlNode.GetChildAsBool("DeviceTagsBasedRequestedTableColumns");
            ColumnNames = xmlNode.GetChildAsString("ColumnNames");
            ColumnNameTag = xmlNode.GetChildAsString("ColumnNameTag");
            ColumnNameValue = xmlNode.GetChildAsString("ColumnNameValue");

            try
            {
                ColumnNameValueFormat = (FormatTag)Enum.Parse(typeof(FormatTag), xmlNode.GetChildAsString("ColumnNameValueFormat"));
            }
            catch { ColumnNameValueFormat = FormatTag.Float; }

            ColumnNameValueNumberDecimalPlaces = xmlNode.GetChildAsInt("ColumnNameValueNumberDecimalPlaces");
            ColumnNameDatetime = xmlNode.GetChildAsString("ColumnNameDatetime");
        }


        /// <summary>
        /// Saves the settings into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
                throw new ArgumentNullException("xmlElem");

            xmlElem.AppendElem("TagID", TagID.ToString());
            xmlElem.AppendElem("TagName", TagName);
            xmlElem.AppendElem("TagCode", TagCode);
            xmlElem.AppendElem("TagFormatData", TagFormatData.ToString());
            xmlElem.AppendElem("TagNumberDecimalPlaces", TagNumberDecimalPlaces.ToString());
            xmlElem.AppendElem("TagAddressNumberBlock", TagAddressNumberBlock);
            xmlElem.AppendElem("TagAddressNumberLine", TagAddressNumberLine);
            xmlElem.AppendElem("TagAddressNumberParametr", TagAddressNumberParameter);
            xmlElem.AppendElem("TagDescription", TagDescription);
            xmlElem.AppendElem("TagEnabled", TagEnabled.ToString());

            xmlElem.AppendElem("DeviceTagsBasedRequestedTableColumns", DeviceTagsBasedRequestedTableColumns);
            xmlElem.AppendElem("ColumnNames", ColumnNames);
            xmlElem.AppendElem("ColumnNameTag", ColumnNameTag);
            xmlElem.AppendElem("ColumnNameValue", ColumnNameValue);
            xmlElem.AppendElem("ColumnNameValueFormat", ColumnNameValueFormat.ToString());
            xmlElem.AppendElem("ColumnNameValueNumberDecimalPlaces", ColumnNameValueNumberDecimalPlaces.ToString());
            xmlElem.AppendElem("ColumnNameDatetime", ColumnNameDatetime);
        }
        #endregion Xml

        #region Получение значения тега по типу данных

        //public static void GetValue(ParserListBlocks blocks, ref DriverTag tag)
        //{
        //    if (tag == null)
        //    {
        //        return;
        //    }

        //    if (tag.TagEnabled == true) // eсли тег активен
        //    {
        //        if (tag.TagID != null) // если у самого тега есть свой ID
        //        {
        //            #region Расшифровка адреса тега
        //            int addressBlock = 0;
        //            int addressBlockStart = 0;
        //            int addressBlockEnd = 0;
        //            int addressBlockCount = 0;

        //            int addressLine = 0;
        //            int addressLineStart = 0;
        //            int addressLineEnd = 0;
        //            int addressLineCount = 0;

        //            int addressParameter = 0;
        //            int addressParameterStart = 0;
        //            int addressParameterEnd = 0;
        //            int addressParameterCount = 0;

        //            try
        //            {
        //                addressBlock = ParsingAddress(tag.tagAddressNumberBlock, out addressBlock, out addressBlockStart, out addressBlockEnd, out addressBlockCount);
        //            }
        //            catch { }
        //            try
        //            {
        //                addressLine = ParsingAddress(tag.tagAddressNumberLine, out addressLine, out addressLineStart, out addressLineEnd, out addressLineCount);
        //            }
        //            catch { }
        //            try
        //            {
        //                addressParameter = ParsingAddress(tag.tagAddressNumberParameter, out addressParameter, out addressParameterStart, out addressParameterEnd, out addressParameterCount);
        //            }
        //            catch { }

        //            #endregion Расшифровка адреса тега

        //            #region Преобразования 

        //            try
        //            {
        //                if (blocks != null)
        //                {
        //                    // формат данных
        //                    switch ((FormatData)Enum.Parse(typeof(FormatData), tag.TagFormatData.ToString()))
        //                    {
        //                        //case FormatData.Boolean:
        //                        //    tag.TagDataValue = Convert.ToBoolean(parserText.GetStringValue(blocks, addressBlock, addressLine, addressParameter).TrimEnd(new char[] { ',', '.', ';', ':', '?', '!' }));
        //                        //    break;
        //                        //case FormatData.DateTime:
        //                        //    tag.TagDataValue = DateTime.Parse(parserText.GetStringValue(blocks, addressBlock, addressLine, addressParameter).TrimEnd(new char[] { ',', '.', ';', ':', '?', '!' }));
        //                        //    break;
        //                        //case FormatData.Float:
        //                        //    tag.TagDataValue = DriverUtils.FloatAsFloat(parserText.GetStringValue(blocks, addressBlock, addressLine, addressParameter).TrimEnd(new char[] { ',', '.', ';', ':', '?', '!' }));
        //                        //    break;
        //                        //case FormatData.String:
        //                        //    tag.TagDataValue = parserText.GetStringValue(blocks, addressBlock, addressLine, addressParameter).TrimEnd(new char[] { ',', '.', ';', ':', '?', '!' });
        //                        //    break;
        //                        //case FormatData.Integer:
        //                        //    tag.TagDataValue = Convert.ToInt32(parserText.GetStringValue(blocks, addressBlock, addressLine, addressParameter).TrimEnd(new char[] { ',', '.', ';', ':', '?', '!' }));
        //                        //    break;
        //                    }
        //                }
        //            }
        //            catch { }

        //            #endregion Преобразования
        //        }
        //        else
        //        {
        //            tag.TagDataValue = "<Error!>";
        //        }
        //    }
        //}

        #endregion Получение значения тега по типу данных

        #region Конвертирование в строку

        #region IsNullString
        /// <summary>
        /// Сonverting an object to a string, if the object is empty, it returns an empty string.
        /// </summary>
        /// <param name="Value">object Value</param>
        /// <returns>string Value</returns>
        public static string TagToString(object Value, FormatData formatData = FormatData.String)
        {
            try
            {
                // check for null
                if (Value == null) return "";

                var type = Value.GetType();

                // check for invalid values in date times.
                if (type == typeof(DateTime))
                {
                    if (((DateTime)Value) == DateTime.MinValue)
                    {
                        return string.Empty;
                    }

                    var date = (DateTime)Value;

                    if (date.Millisecond > 0)
                    {
                        return date.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                    }
                    else
                    {
                        return date.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                // use only the local name for qualified names.
                if (type == typeof(XmlQualifiedName))
                {
                    return ((XmlQualifiedName)Value).Name;
                }

                // use only the name for system types.
                if (type.FullName == "System.RuntimeType")
                {
                    return ((Type)Value).FullName;
                }

                // treat byte arrays as a special case.
                if (type == typeof(byte[]))
                {
                    var bytes = (byte[])Value;

                    var buffer = new StringBuilder(bytes.Length * 3);

                    foreach (var character in bytes)
                    {
                        buffer.Append(character.ToString("X2"));
                        buffer.Append(".");
                    }

                    return buffer.ToString();
                }

                // show the element type and length for arrays.
                if (type.IsArray)
                {
                    string result = string.Empty;
                    int index = 0;
                    foreach (object element in (Array)Value)
                    {
                        result += String.Format("[{0}]", index++) + element.ToString() + Environment.NewLine;
                    }
                    return $"{type.GetElementType()?.Name}[{((Array)Value).Length}]{result}";
                }

                // instances of array are always treated as arrays of objects.
                if (type == typeof(Array))
                {
                    string result = string.Empty;
                    int index = 0;
                    foreach (object element in (Array)Value)
                    {
                        result += String.Format("[{0}]", index++) + element.ToString() + Environment.NewLine;
                    }
                    return $"Object[{((Array)Value).Length}]{result}";
                }

                // default behavior.
                return Value.ToString().Replace("\"", "");
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        #endregion IsNullString


        #endregion Конвертирование в строку

        /// <summary>
        /// Returns the value of the required number of bits from the string float.
        /// </summary>
        public static int ParsingAddress(string s, out int address, out int startAddress, out int endAddress, out int countAddress)
        {
            address = 0;
            startAddress = 0;
            endAddress = 0;
            countAddress = 0;

            string[] parts = FloatPuttingInOrder(s).Split('.');
            if (parts.Length == 1)
            {
                address = Convert.ToInt32(parts[0]);
                return address;
            }
            string[] parts2 = parts[1].Split("-");
            if (parts2.Length == 1)
            {
                startAddress = Convert.ToInt32(parts[1]);
                countAddress = 1;
                address = Convert.ToInt32(parts[0]);
                return address;
            }
            address = Convert.ToInt32(parts[0]);
            startAddress = Convert.ToInt32(parts2[0]);
            endAddress = Convert.ToInt32(parts2[1]);
            countAddress = endAddress - startAddress;
            return address;
        }

        /// <summary>
        /// Replacing the comma in the float string with a dot.
        /// </summary>
        private static string FloatPuttingInOrder(string s)
        {
            s = s.Replace(",", ".").Trim();
            return s;
        }

    }
    #endregion Class DriverTag

}
