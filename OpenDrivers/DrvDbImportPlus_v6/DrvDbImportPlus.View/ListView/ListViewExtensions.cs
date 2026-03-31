namespace Scada.Comm.Drivers.DrvDbImportPlus.View
{
    public enum MoveDirection { Up = -1, Down = 1 };
    public static class ListViewExtensions
    {
        /// <summary>
        /// Move row up or down dependent on direction parameter
        /// </summary>
        /// <param name="direction">Up or Down</param>
        public static void MoveListViewItems(this ListView sender, MoveDirection direction)
        {
            int dir = (int)direction;

            bool valid = sender.SelectedItems.Count > 0 &&
                            ((direction == MoveDirection.Down &&
                            (sender.SelectedItems[sender.SelectedItems.Count - 1]
                                .Index <
                            sender.Items.Count - 1)) ||
                            (direction == MoveDirection.Up &&
                            (sender.SelectedItems[0]
                                .Index >
                            0)));

            if (valid)
            {
                sender.SuspendLayout();

                try
                {
                    foreach (ListViewItem item in sender.SelectedItems)
                    {
                        var index = item.Index + dir;
                        sender.Items.RemoveAt(item.Index);
                        sender.Items.Insert(index, item);
                        sender.Items[index].Selected = true;
                        sender.Focus();
                    }
                }
                finally
                {
                    sender.ResumeLayout();
                }
            }
        }

        /// <summary>
        /// Automatically adjust the width of the column according to the name and its contents.
        /// </summary>
        public static void ResizeColumn(ListView listView, string columnNameOrTag)
        {
            int index = GetColumnIndexByName(listView, columnNameOrTag);
            if (index >= 0)
            {
                listView.AutoResizeColumn(index, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView.AutoResizeColumn(index, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView.Columns[index].Width += 20;
            }
        }

        /// <summary>
        /// Automatically adjust the width of the column according to the name and its contents.
        /// </summary>
        public static void ResizeColumnDefault(ListView listView, string columnNameOrTag)
        {
            int index = GetColumnIndexByName(listView, columnNameOrTag);
            if (index >= 0)
            {
                listView.AutoResizeColumn(index, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView.AutoResizeColumn(index, ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        /// <summary>
        /// Get the index by column name.
        /// </summary>
        /// <param name="listView">Name ListView</param>
        /// <param name="columnIdentifier">Name Column</param>
        /// <returns>Index Column</returns>
        public static int GetColumnIndexByName(ListView listView, string columnIdentifier)
        {
            for (int i = 0; i < listView.Columns.Count; i++)
            {
                if (listView.Columns[i].Name == columnIdentifier)
                {
                    return i;
                }
            }

            for (int i = 0; i < listView.Columns.Count; i++)
            {
                if (listView.Columns[i].Text == columnIdentifier)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}

