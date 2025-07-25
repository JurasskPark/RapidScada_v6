﻿using Scada.Forms;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
{
    /// <summary>
    /// A form with settings.
    /// <para>Форма с настройками.</para>
    /// </summary>
    public partial class FrmSettings : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmSettings()
        {
            InitializeComponent();
        }

        #region Variables
        public FrmConfig formParent;                        // parent form
        public Project project;                             // the project configuration
        private bool isRussian;                             // language

        private bool modified;                              // the configuration was modified
        #endregion Variables

        #region Form Load
        /// <summary>
        /// Load the form.
        /// <para>Загрузка формы.</para>
        /// </summary>
        private void FrmSettings_Load(object sender, EventArgs e)
        {
            ConfigToControls();
            Translate();
        }
        #endregion Form Load

        #region Config 
        /// <summary>
        /// Sets the controls according to the configuration.
        /// <para>Установить элементы управления в соответствии с конфигурацией.</para>
        /// </summary>
        private void ConfigToControls()
        {
            if (formParent.isDll)
            {
                lblLogDays.Visible = false;
                nudLogDays.Visible = false;

                gpbLanguage.Visible = false;
            }

            ckbWriteDriverLog.Checked = project.DebugerSettings.LogWrite;
            nudLogDays.Value = Convert.ToDecimal(project.DebugerSettings.LogDays);

            isRussian = project.LanguageIsRussian;
            cmbLanguage.SelectedIndex = Convert.ToInt32(isRussian);

            Modified = false;
        }

        /// <summary>
        /// Sets the controls according to the configuration.
        /// <para>Устанавливает элементы управления в соответствии с конфигурацией.</para>>
        /// </summary>
        private void ControlsToConfig()
        {
            project.DebugerSettings.LogWrite = ckbWriteDriverLog.Checked;
            project.DebugerSettings.LogDays = Convert.ToInt32(nudLogDays.Value);

            int index = cmbLanguage.SelectedIndex;
            project.LanguageIsRussian = Convert.ToBoolean(index);
        }

        #endregion Config 

        #region Translate
        /// <summary>
        /// Translation of the form.
        /// <para>Перевод формы.</para>
        /// </summary>
        private void Translate()
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
        }
        #endregion Translate

        #region Modified
        /// <summary>
        /// Gets or sets a value indicating whether the configuration was modified.
        /// Возвращает или задает значение, указывающее, была ли изменена конфигурация.
        /// </summary>
        private bool Modified
        {
            get
            {
                return modified;
            }
            set
            {
                modified = value;
                btnSave.Enabled = modified;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the configuration was modified.
        /// <para>Возвращает или задает значение, указывающее, была ли изменена конфигурация.</para>
        /// </summary>
        private void control_Changed(object sender, EventArgs e)
        {
            Modified = true;
        }

        #endregion Modified

        #region Control
        /// <summary>
        /// Close the form and save the settings.
        /// <para>Закрытие формы и сохранение настроек.</para>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ControlsToConfig();
            formParent.SaveData();
            Close();
        }

        /// <summary>
        /// Closing the form without saving settings.
        /// <para>Закрытие формы без сохранения настроек.</para>>
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion Control

    }
}
