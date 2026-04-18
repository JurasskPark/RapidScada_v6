using System;

namespace Scada.Comm.Drivers.DrvDDEJP
{
    /// <summary>
    /// Specifies the tag data formats.
    /// <para>Задает форматы данных тегов.</para>
    /// </summary>
    public enum TagDataFormat
    {
        Bool = 0,
        Int16 = 1,
        UInt16 = 2,
        Int32 = 3,
        UInt32 = 4,
        Int64 = 5,
        UInt64 = 6,
        Float = 7,
        Double = 8,
        Ascii = 9,
        Unicode = 10,
        HexString = 11
    }

    /// <summary>
    /// Represents a project tag configuration.
    /// <para>Представляет конфигурацию тега проекта.</para>
    /// </summary>
    public class ProjectTag
    {
        #region Variable

        private string name = "Tag";                                // the tag name
        private string topic = string.Empty;                        // the DDE topic
        private string itemName = string.Empty;                     // the DDE item name

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets or sets the tag ID.
        /// <para>Получает или задает идентификатор тега.</para>
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the tag order.
        /// <para>Получает или задает порядок тега.</para>
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the tag is enabled.
        /// <para>Получает или задает значение, указывающее, включен ли тег.</para>
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the tag name.
        /// <para>Получает или задает имя тега.</para>
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the tag channel.
        /// <para>Получает или задает канал тега.</para>
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// Gets or sets the DDE topic.
        /// <para>Получает или задает DDE топик.</para>
        /// </summary>
        public string Topic
        {
            get => topic;
            set => topic = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the DDE item name.
        /// <para>Получает или задает имя DDE элемента.</para>
        /// </summary>
        public string ItemName
        {
            get => itemName;
            set => itemName = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the data length in bytes.
        /// <para>Получает или задает длину данных в байтах.</para>
        /// </summary>
        public int DataLength { get; set; } = 2;

        /// <summary>
        /// Gets or sets the data format.
        /// <para>Получает или задает формат данных.</para>
        /// </summary>
        public TagDataFormat DataFormat { get; set; } = TagDataFormat.UInt16;

        #endregion Property

        #region Basic

        /// <summary>
        /// Normalizes the tag configuration.
        /// <para>Нормализует конфигурацию тега.</para>
        /// </summary>
        /// <param name="order">The tag order.</param>
        public void Normalize(int order)
        {
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }

            Order = order;
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = "Tag";
            }
            Topic = Topic.Trim();
            ItemName = ItemName.Trim();
            DataLength = Math.Max(GetMinimumDataLength(DataFormat), DataLength);
        }

        /// <summary>
        /// Gets the minimum data length for the specified format.
        /// <para>Получает минимальную длину данных для указанного формата.</para>
        /// </summary>
        private static int GetMinimumDataLength(TagDataFormat format)
        {
            return format switch
            {
                TagDataFormat.Bool => 1,
                TagDataFormat.Int16 => 2,
                TagDataFormat.UInt16 => 2,
                TagDataFormat.Int32 => 4,
                TagDataFormat.UInt32 => 4,
                TagDataFormat.Float => 4,
                TagDataFormat.Int64 => 8,
                TagDataFormat.UInt64 => 8,
                TagDataFormat.Double => 8,
                _ => 1
            };
        }

        #endregion Basic
    }
}
