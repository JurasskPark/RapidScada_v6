using System.Data;

namespace Scada.Comm.Drivers.DrvDbDataTransferJP
{
    /// <summary>
    /// Represents a database-to-database transfer result.
    /// <para>Представляет результат перекладки данных из одной БД в другую.</para>
    /// </summary>
    public class DbTransferResult
    {
        /// <summary>
        /// Gets or sets the number of rows read from the source database.
        /// <para>Возвращает или задает количество строк, прочитанных из исходной БД.</para>
        /// </summary>
        public int ReadRows { get; set; }

        /// <summary>
        /// Gets or sets the number of rows written to the target database.
        /// <para>Возвращает или задает количество строк, записанных в целевую БД.</para>
        /// </summary>
        public int WrittenRows { get; set; }

        /// <summary>
        /// Gets or sets the data table read from the source database.
        /// <para>Возвращает или задает таблицу данных, прочитанную из исходной БД.</para>
        /// </summary>
        public DataTable SourceData { get; set; } = new DataTable();

        /// <summary>
        /// Gets or sets the transfer error message.
        /// <para>Возвращает или задает сообщение об ошибке переноса.</para>
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether the transfer completed without errors.
        /// <para>Возвращает значение, показывающее, завершился ли перенос без ошибок.</para>
        /// </summary>
        public bool Success => string.IsNullOrEmpty(ErrorMessage);
    }
}
