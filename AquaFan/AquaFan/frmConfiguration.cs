﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AquaFan
{
    public partial class frmConfiguration : Form
    {

        private Controller cntrl;

        public Controller ControllerObject
        {
            get { return cntrl; }
            set { cntrl = value; }
        }

        public frmConfiguration()
        {
            InitializeComponent();
        }

        private void frmConfiguration_Load(object sender, EventArgs e)
        {
            cbLanguages.Items.Clear();
            //Alle Sprachen zur auswahl hinzufuegen
            foreach(string sLanguage in cntrl.LanguageControllerObject.Languages.Keys)
            {
                cbLanguages.Items.Add(sLanguage);
            }


            //Die Aktuelle Sprache vorauswählen
            foreach(string sLanguage in cbLanguages.Items)
            {
                if(sLanguage == cntrl.XmlControllerObject.getDefaultLanguage())
                {
                    cbLanguages.SelectedIndex = cbLanguages.Items.IndexOf(sLanguage);
                }
            }

            //Sprache anpassen
            cntrl.CurrentForm = this;
            cntrl.LanguageControllerObject.collectControls();
            cntrl.LanguageControllerObject.changeLanguage(cntrl.LanguageControllerObject.CurrentLanguage);

            //AquaComputerCmd Pfad setzen
            tbAquaComputerCmdPath.Text = cntrl.XmlControllerObject.CmdPath;

            //Geräte Seriennummer
            tbDeviceSerial.Text = cntrl.XmlControllerObject.getDeviceSerial();

            //Änderungen an den Reglern sofort oder erst beim speichern
            chkSaveBeforeApply.Checked = Convert.ToBoolean(cntrl.XmlControllerObject.changeFanSpeedsByActiveProfile());

            chkApplyChangesAtStartup.Checked = Convert.ToBoolean(cntrl.XmlControllerObject.getApplyValuesAtProgramStartValue());

            chkStartMinimized.Checked = Convert.ToBoolean(cntrl.XmlControllerObject.getStartMinimizedValue());
        }

        private void btnSelectAquacomputerPath_Click(object sender, EventArgs e)
        {
            tbAquaComputerCmdPath.Text = cntrl.setAquacomputerCmdPath(tbAquaComputerCmdPath.Text);
        }

        private void btnSaveConfiguration_Click(object sender, EventArgs e)
        {
            cntrl.XmlControllerObject.setAquacomputerCmd(tbAquaComputerCmdPath.Text);
            cntrl.XmlControllerObject.setDefaultLanguage(cbLanguages.Text);
            cntrl.XmlControllerObject.setDeviceSerial(tbDeviceSerial.Text);
            cntrl.XmlControllerObject.setChangeFanSpeedsByAciveProfile(chkSaveBeforeApply.Checked);
            cntrl.ApplyChangesWhenChangingActiveProfile = chkSaveBeforeApply.Checked;
            cntrl.XmlControllerObject.setApplyChangesAtActiveProfile(chkApplyChangesAtStartup.Checked);
            cntrl.XmlControllerObject.setStartMinimizedValue(chkStartMinimized.Checked);
            cntrl.setStatus();
        }
    }
}
