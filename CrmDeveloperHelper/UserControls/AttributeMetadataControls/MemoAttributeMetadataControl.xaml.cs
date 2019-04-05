using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.UserControls.AttributeMetadataControls
{
    public partial class MemoAttributeMetadataControl : UserControl, IAttributeMetadataControl<MemoAttributeMetadata>
    {
        public MemoAttributeMetadata AttributeMetadata { get; private set; }

        private readonly string _initialValue;

        private readonly bool _fillAllways;

        public MemoAttributeMetadataControl(bool fillAllways, MemoAttributeMetadata attributeMetadata, string initialValue)
        {
            InitializeComponent();

            AttributeMetadataControlFactory.SetGroupBoxNameByAttributeMetadata(gbAttribute, attributeMetadata);

            if (string.IsNullOrEmpty(initialValue))
            {
                initialValue = string.Empty;
            }

            this._initialValue = initialValue;
            this._fillAllways = fillAllways;
            this.AttributeMetadata = attributeMetadata;

            txtBValue.Text = initialValue;

            btnRemoveControl.IsEnabled = _fillAllways;
            chBChanged.IsEnabled = !_fillAllways;

            btnRemoveControl.Visibility = btnRemoveControl.IsEnabled ? Visibility.Visible : Visibility.Collapsed;
            chBChanged.Visibility = chBChanged.IsEnabled ? Visibility.Visible : Visibility.Collapsed;
        }

        private void txtBValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentValue = GetCurrentString();

            chBChanged.IsChecked = !string.Equals(currentValue, _initialValue);
        }

        private object GetCurrentString()
        {
            var currentValue = txtBValue.Text;

            if (string.IsNullOrEmpty(currentValue))
            {
                currentValue = string.Empty;
            }

            return currentValue;
        }

        public void AddChangedAttribute(Entity entity)
        {
            var currentValue = GetCurrentString();

            if (this._fillAllways || !string.Equals(currentValue, _initialValue))
            {
                entity.Attributes[AttributeMetadata.LogicalName] = currentValue;
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            txtBValue.Focus();

            base.OnGotFocus(e);
        }

        public event EventHandler RemoveControlClicked;

        private void btnRemoveControl_Click(object sender, RoutedEventArgs e)
        {
            RemoveControlClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}