namespace Scada.Comm.Drivers.DrvPingJP.View
{
    /// <summary>
    /// Defines the move direction.
    /// </summary>
    public enum MoveDirection { Up = -1, Down = 1 };

    /// <summary>
    /// Provides extensions for the ListView control.
    /// </summary>
    public static class ListViewExtensions
    {
        /// <summary>
        /// Moves rows up or down depending on the direction.
        /// </summary>
        /// <param name="sender">The list view.</param>
        /// <param name="direction">The move direction.</param>
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
    }
}
