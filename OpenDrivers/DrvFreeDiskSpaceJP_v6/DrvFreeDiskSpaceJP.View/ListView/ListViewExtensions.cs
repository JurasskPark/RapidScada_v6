namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View
{
    public enum MoveDirection { Up = -1, Down = 1 };
        public static class ListViewExtensions
        {
            /// <summary>
            /// Move row up or down dependent on direction parameter
            /// </summary>
            /// <param name="sender"></param>
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
        }
    }

