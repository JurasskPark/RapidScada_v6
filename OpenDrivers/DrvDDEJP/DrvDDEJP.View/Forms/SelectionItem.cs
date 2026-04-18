namespace Scada.Comm.Drivers.DrvDDEJP.View.Forms
{
    /// <summary>
    /// Represents a selectable UI item with a value and display text.
    /// <para>Представляет элемент выбора интерфейса со значением и отображаемым текстом.</para>
    /// </summary>
    public sealed class SelectionItem<T>
    {
        /// <summary>
        /// Gets or sets the internal value.
        /// </summary>
        public T Value { get; set; } = default!;

        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}
