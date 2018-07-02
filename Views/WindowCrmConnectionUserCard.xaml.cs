﻿using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Views
{
    public partial class WindowCrmConnectionUserCard : WindowBase
    {
        public ConnectionUserData User { get; private set; }

        public WindowCrmConnectionUserCard(ConnectionUserData user)
        {
            InitializeComponent();

            InputLanguageManager.SetInputLanguage(this, CultureInfo.CreateSpecificCulture("en-US"));

            if (user != null)
            {
                this.User = user;

                this.txtBUsername.Text = user.Username;
                this.passBPassword.Password = user.Password;
            }

            txtBUsername.Focus();
        }

        private bool FieldsIsOk(out string message)
        {
            bool result = true;
            message = string.Empty;

            StringBuilder builder = new StringBuilder();

            if (string.IsNullOrEmpty(this.txtBUsername.Text.Trim()))
            {
                result = false;

                if (builder.Length > 0) { builder.AppendLine(); }

                builder.Append("Username is empty.");
            }

            if (string.IsNullOrEmpty(this.passBPassword.Password))
            {
                result = false;

                if (builder.Length > 0) { builder.AppendLine(); }

                builder.Append("Password is empty.");
            }

            message = builder.ToString();
            return result;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            bool fieldsIsOk = FieldsIsOk(out message);

            if (!fieldsIsOk)
            {
                MessageBox.Show(this, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (this.User == null)
                {
                    this.User = new ConnectionUserData();
                }

                this.User.Username = this.txtBUsername.Text.Trim();
                this.User.Password = this.passBPassword.Password;

                if (this.User.UserId == Guid.Empty)
                {
                    this.User.UserId = Guid.NewGuid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unable to save connection details. Error: {0}", ex.Message), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

            this.Close();
        }
    }
}
