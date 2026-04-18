using System;

namespace Scada.Comm.Drivers.DrvDDEJP
{
    /// <summary>
    /// Specifies the command data kinds.
    /// <para>Задает виды данных команд.</para>
    /// </summary>
    public enum CommandDataKind
    {
        Hex = 0,
        Ascii = 1,
        Unicode = 2,
        Template = 3
    }

    /// <summary>
    /// Specifies the command run modes.
    /// <para>Задает режимы выполнения команд.</para>
    /// </summary>
    public enum CommandRunMode
    {
        Sequence = 0,
        Single = 1,
        Cyclic = 2
    }

    /// <summary>
    /// Represents a project command configuration.
    /// <para>Представляет конфигурацию команды проекта.</para>
    /// </summary>
    public class ProjectCommand
    {
        #region Variable

        private string name = "Command";                            // the command name
        private string payload = string.Empty;                      // the command payload
        private string note = string.Empty;                         // a note about the command

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets or sets the command ID.
        /// <para>Получает или задает идентификатор команды.</para>
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the command order.
        /// <para>Получает или задает порядок команды.</para>
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the command is enabled.
        /// <para>Получает или задает значение, указывающее, включена ли команда.</para>
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the command name.
        /// <para>Получает или задает имя команды.</para>
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the command data kind.
        /// <para>Получает или задает вид данных команды.</para>
        /// </summary>
        public CommandDataKind DataKind { get; set; } = CommandDataKind.Hex;

        /// <summary>
        /// Gets or sets the command payload.
        /// <para>Получает или задает полезную нагрузку команды.</para>
        /// </summary>
        public string Payload
        {
            get => payload;
            set => payload = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the command delay in milliseconds.
        /// <para>Получает или задает задержку команды в миллисекундах.</para>
        /// </summary>
        public int DelayMs { get; set; }

        /// <summary>
        /// Gets or sets the command note.
        /// <para>Получает или задает примечание к команде.</para>
        /// </summary>
        public string Note
        {
            get => note;
            set => note = value ?? string.Empty;
        }

        #endregion Property

        #region Basic

        /// <summary>
        /// Normalizes the command configuration.
        /// <para>Нормализует конфигурацию команды.</para>
        /// </summary>
        /// <param name="order">The command order.</param>
        public void Normalize(int order)
        {
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }

            Order = order;
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = "Command";
            }
            Payload = Payload.Trim();
            Note = Note.Trim();
            DelayMs = Math.Max(0, DelayMs);
        }

        #endregion Basic
    }
}
