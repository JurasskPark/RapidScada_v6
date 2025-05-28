using Scada;
using Scada.Comm.Drivers.DrvFreeDiskSpaceJP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using static Scada.Comm.Drivers.DrvFreeDiskSpaceJP.DriverTag;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    #region Class DriverGroupTag
    public class DriverGroupTag
    {
        public DriverGroupTag()
        {
            Group = new List<DriverTag>();
        }

        #region Tag group
        public List<DriverTag> Group { get; set; }
        #endregion Tag group
    }
    #endregion Class DriverGroupTag

    #region Class DriverTag
    public class DriverTag
    {
        public DriverTag()
        {
            TagID = Guid.NewGuid();
            TagName = string.Empty;
            TagCode = string.Empty;
            TagAddress = string.Empty;
            TagDescription = string.Empty;
            TagEnabled = true;
            TagDataValue = new object();
            TagValueFormat = DriverTag.FormatData.Float;
            TagDateTime = DateTime.MinValue;
            TagNumberDecimalPlaces = 3;
        }

        #region Tag
        private Guid tagID;
        public Guid TagID
        {
            get { return tagID; }
            set { tagID = value; }
        }

        private string tagAddress;
        public string TagAddress
        {
            get { return tagAddress; }
            set { tagAddress = value; }
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
        }

        private FormatData tagValueFormat;
        public FormatData TagValueFormat
        {
            set { tagValueFormat = value; }
            get { return tagValueFormat; }
        }

        private int tagNumberDecimalPlaces;
        public int TagNumberDecimalPlaces
        {
            set { tagNumberDecimalPlaces = value; }
            get { return tagNumberDecimalPlaces; }
        }

        #endregion Tag

        #region Xml
        /// <summary>
        /// Loads the settings from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException("xmlNode");
            }

            TagID = DriverUtils.StringToGuid(xmlNode.GetChildAsString("TagID"));
            TagName = xmlNode.GetChildAsString("TagName");
            TagCode = xmlNode.GetChildAsString("TagCode");
            
            try
            {
                TagValueFormat = (FormatData)Enum.Parse(typeof(FormatData), xmlNode.GetChildAsString("TagValueFormat"));
            }
            catch { TagValueFormat = FormatData.Float; }

            TagNumberDecimalPlaces = xmlNode.GetChildAsInt("TagNumberDecimalPlaces");
            TagAddress = xmlNode.GetChildAsString("TagAddress");
            TagDescription = xmlNode.GetChildAsString("TagDescription");
            TagEnabled = xmlNode.GetChildAsBool("TagEnabled");
            TagDataValue = new object();
            TagDateTime = DateTime.MinValue;
        }


        /// <summary>
        /// Saves the settings into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
            {
                throw new ArgumentNullException("xmlElem");
            }

            xmlElem.AppendElem("TagID", TagID.ToString());
            xmlElem.AppendElem("TagName", TagName);
            xmlElem.AppendElem("TagCode", TagCode);
            xmlElem.AppendElem("TagValueFormat", Enum.GetName(typeof(FormatData), TagValueFormat));
            xmlElem.AppendElem("TagNumberDecimalPlaces", TagNumberDecimalPlaces.ToString());
            xmlElem.AppendElem("TagAddress", TagAddress);
            xmlElem.AppendElem("TagDescription", TagDescription);
            xmlElem.AppendElem("TagEnabled", TagEnabled.ToString());
           
        }
        #endregion Xml

        #region Getting tag value by data type

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

        #endregion Getting tag value by data type

        #region Convert to string

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

        #endregion Convert to string

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
