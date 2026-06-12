using System.Diagnostics;

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmAbout : Form
    {
        /// <summary>
        /// Initializes a new instance of the form.
        /// <para>Инициализирует новый экземпляр формы.</para>
        /// </summary>
        public FrmAbout()
        {
            InitializeComponent();
        }

        public string AppTitle
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                if (value == string.Empty)
                {
                    lblTitle.Visible = false;
                    return;
                }
                lblTitle.Visible = true;
                lblTitle.Text = ReplaceTokens(lblTitle.Text, value);
            }
        }

        public string AppDescription
        {
            get
            {
                return lblDescription.Text;
            }
            set
            {
                if (value == string.Empty)
                {
                    lblDescription.Visible = false;
                    return;
                }
                lblDescription.Visible = true;
                lblDescription.Text = ReplaceTokens(lblDescription.Text, value);
            }
        }

        public string AppVersion
        {
            get
            {
                return lblVersion.Text;
            }
            set
            {
                if (value == string.Empty)
                {
                    lblVersion.Visible = false;
                    return;
                }
                lblVersion.Visible = true;
                lblVersion.Text = ReplaceTokens(lblVersion.Text, value);
            }
        }

        public string AppCopyright
        {
            get
            {
                return lblCopyright.Text;
            }
            set
            {
                if (value == string.Empty)
                {
                    lblCopyright.Visible = false;
                    return;
                }
                lblCopyright.Visible = true;
                lblCopyright.Text = ReplaceTokens(lblCopyright.Text, value);
            }
        }

        public string AppInfoMore
        {
            get
            {
                return rchInfoMore.Text;
            }
            set
            {
                if (value == null || value == string.Empty)
                {
                    rchInfoMore.Visible = false;
                    return;
                }
                rchInfoMore.Visible = true;
                rchInfoMore.Text = ReplaceTokens(rchInfoMore.Text, value);
            }
        }

        public string AppBuildDate
        {
            get
            {
                return lblBuildDate.Text;
            }
            set
            {
                if (value == null || value == string.Empty)
                {
                    lblBuildDate.Visible = false;
                    return;
                }
                lblBuildDate.Visible = true;
                lblBuildDate.Text = ReplaceTokens(lblBuildDate.Text, value);
            }
        }

        public string[] AppLinkInfo
        {
            get
            {
                return new string[4] { linkLabel1.Text, linkLabel2.Text, linkLabel3.Text, linkLabel4.Text };
            }
            set
            {
                if (value == null || value[0] == null || value[0] == string.Empty)
                {
                    pcbIcon1.Image = null;
                    linkLabel1.Visible = false;
                }
                else
                {
                    int num = SearchImageSocialNerwork(value[0]);
                    if (num > 0)
                    {
                        pcbIcon1.Image = imgList.Images[num];
                    }
                    pcbIcon1.Visible = true;
                    linkLabel1.Visible = true;
                    linkLabel1.Text = value[0];
                }
                if (value == null || value[1] == null || value[1] == string.Empty)
                {
                    pcbIcon2.Image = null;
                    linkLabel2.Visible = false;
                }
                else
                {
                    pcbIcon2.Image = imgList.Images[SearchImageSocialNerwork(value[1])];
                    pcbIcon2.Visible = true;
                    linkLabel2.Visible = true;
                    linkLabel2.Text = value[1];
                }
                if (value == null || value[2] == null || value[2] == string.Empty)
                {
                    pcbIcon3.Image = null;
                    linkLabel3.Visible = false;
                }
                else
                {
                    pcbIcon3.Image = imgList.Images[SearchImageSocialNerwork(value[2])];
                    pcbIcon3.Visible = true;
                    linkLabel3.Visible = true;
                    linkLabel3.Text = value[2];
                }
                if (value == null || value[3] == null || value[3] == string.Empty)
                {
                    pcbIcon4.Image = null;
                    linkLabel4.Visible = false;
                    return;
                }
                pcbIcon4.Image = imgList.Images[SearchImageSocialNerwork(value[3])];
                pcbIcon4.Visible = true;
                linkLabel4.Visible = true;
                linkLabel4.Text = value[3];
            }
        }

        /// <summary>
        /// Executes ReplaceTokens.
        /// <para>Выполняет ReplaceTokens.</para>
        /// </summary>
        private string ReplaceTokens(string text, string value)
        {
            text = text.Replace("%title%", value);
            text = text.Replace("%copyright%", value);
            text = text.Replace("%description%", value);
            text = text.Replace("%company%", value);
            text = text.Replace("%info%", value);
            text = text.Replace("%trademark%", value);
            text = text.Replace("%year%", value);
            text = text.Replace("%version%", value);
            text = text.Replace("%builddate%", value);
            return text;
        }

        /// <summary>
        /// Executes SearchImageSocialNerwork.
        /// <para>Выполняет SearchImageSocialNerwork.</para>
        /// </summary>
        private int SearchImageSocialNerwork(string value)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>
            {
                [0] = "github",
                [1] = "mailto",
                [2] = "jurasskpark.ru",
                [3] = "youtube"
            };
            for (int i = 0; i < dictionary.Count; i++)
            {
                string value2 = dictionary[i];
                int num = value.IndexOf(value2);
                if (num >= 0)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Handles the linkLabel1_LinkClicked event.
        /// <para>Обрабатывает событие linkLabel1_LinkClicked.</para>
        /// </summary>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(linkLabel1.Text.Trim());
        }

        /// <summary>
        /// Handles the linkLabel2_LinkClicked event.
        /// <para>Обрабатывает событие linkLabel2_LinkClicked.</para>
        /// </summary>
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(linkLabel2.Text.Trim());
        }

        /// <summary>
        /// Handles the linkLabel3_LinkClicked event.
        /// <para>Обрабатывает событие linkLabel3_LinkClicked.</para>
        /// </summary>
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(linkLabel3.Text.Trim());
        }

        /// <summary>
        /// Handles the linkLabel4_LinkClicked event.
        /// <para>Обрабатывает событие linkLabel4_LinkClicked.</para>
        /// </summary>
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(linkLabel4.Text.Trim());
        }

        /// <summary>
        /// Executes OpenUrl.
        /// <para>Выполняет OpenUrl.</para>
        /// </summary>
        private void OpenUrl(string url)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = url
                };
                Process.Start(startInfo);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles the btnOK_Click event.
        /// <para>Обрабатывает событие btnOK_Click.</para>
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            Close();
        }

    }
}
