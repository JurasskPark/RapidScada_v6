
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
	public partial class ProgressBarEx : ProgressBar
	{

		private string _text = string.Empty;

		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		public override string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				Invalidate();
			}
		}

		private SolidBrush _textBrush = new SolidBrush(Color.DarkRed);

		public Color TextColor
		{
			get { return _textBrush.Color; }
			set
			{
				_textBrush.Color = value;
				Invalidate();
			}
		}

		private SolidBrush _foreBrush = new SolidBrush(Color.White);

		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				_foreBrush.Color = value;
				Invalidate();
			}
		}

		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				Invalidate();
			}
		}

		private Pen _borderPen = new Pen(Color.Black, 2);

		private const int MARGINS = 4;

		public ProgressBarEx()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			SizeF textSize = e.Graphics.MeasureString(Text, Font);
			PointF pos = new PointF((float)Size.Width / 2 - textSize.Width / 2, (float)Size.Height / 2 - textSize.Height / 2);

			e.Graphics.DrawRectangle(_borderPen, 0, 0, Width - 1, Height - 1);
			e.Graphics.FillRectangle(_foreBrush, MARGINS, MARGINS, ((Width - MARGINS * 2 - 1) * Value) / Maximum, Height - MARGINS * 2 - 1);
			e.Graphics.DrawString(Text, Font, _textBrush, pos);
		}

	}
}
