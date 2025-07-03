using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
	public class ListViewJP: ListView
	{
		public ListViewJP()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}
	}
}
