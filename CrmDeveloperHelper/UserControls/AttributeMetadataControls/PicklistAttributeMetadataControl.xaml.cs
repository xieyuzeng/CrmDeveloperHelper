using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.UserControls.AttributeMetadataControls
{
    public partial class PicklistAttributeMetadataControl : UserControl, IAttributeMetadataControl<PicklistAttributeMetadata>
    {
        public PicklistAttributeMetadata AttributeMetadata { get; }

        private readonly int? _initialValue;

        private readonly bool _fillAllways;

        public PicklistAttributeMetadataControl(bool fillAllways, Entity entity, PicklistAttributeMetadata attributeMetadata, int? initialValue)
        {
            InitializeComponent();

            AttributeMetadataControlFactory.SetGroupBoxNameByAttributeMetadata(gbAttribute, attributeMetadata);

            this._initialValue = initialValue;
            this._fillAllways = fillAllways;
            this.AttributeMetadata = attributeMetadata;

            FillComboBox(entity);

            btnRemoveControl.IsEnabled = _fillAllways;

            btnRemoveControl.Visibility = btnRemoveControl.IsEnabled ? Visibility.Visible : Visibility.Collapsed;
            chBChanged.Visibility = _fillAllways ? Visibility.Collapsed : Visibility.Visible;

            btnRestore.IsEnabled = !_fillAllways;
            btnRestore.Visibility = btnRestore.IsEnabled ? Visibility.Visible : Visibility.Collapsed;
        }

        private void FillComboBox(Entity entity)
        {
            cmBValue.Items.Clear();
            cmBValue.Items.Add("<Null>");

            ComboBoxItem currentItem = null;

            var optionSetOptions = AttributeMetadata.OptionSet.Options;

            if (_initialValue.HasValue && !optionSetOptions.Any(o => o.Value == _initialValue.Value))
            {
                StringBuilder name = new StringBuilder();

                name.Append(_initialValue.Value);

                if (entity != null
                    && entity.FormattedValues.ContainsKey(AttributeMetadata.LogicalName)
                    && !string.IsNullOrEmpty(entity.FormattedValues[AttributeMetadata.LogicalName])
                )
                {
                    name.AppendFormat(" - {0}", entity.FormattedValues[AttributeMetadata.LogicalName]);
                }
                else
                {
                    name.Append(" - UnknownValue");
                }

                currentItem = new ComboBoxItem()
                {
                    Content = name.ToString(),
                    Tag = _initialValue.Value,
                };

                cmBValue.Items.Add(currentItem);
            }

            foreach (var item in optionSetOptions.OrderBy(o => o.Value))
            {
                StringBuilder name = new StringBuilder();

                name.Append(item.Value);

                var label = CreateFileHandler.GetLocalizedLabel(item.Label);
                var description = CreateFileHandler.GetLocalizedLabel(item.Description);

                if (!string.IsNullOrEmpty(label))
                {
                    name.AppendFormat(" - {0}", label);
                }
                else if (!string.IsNullOrEmpty(description))
                {
                    name.AppendFormat(" - {0}", description);
                }

                var newItem = new ComboBoxItem()
                {
                    Content = name.ToString(),
                    Tag = item.Value.Value,
                };

                cmBValue.Items.Add(newItem);

                if (item.Value == _initialValue)
                {
                    currentItem = newItem;
                }
            }

            if (currentItem != null)
            {
                cmBValue.SelectedItem = currentItem;
            }
            else
            {
                cmBValue.SelectedIndex = 0;
            }
        }

        private void cmBValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentValue = GetIntValue();

            chBChanged.IsChecked = currentValue != _initialValue;
        }

        private int? GetIntValue()
        {
            if (cmBValue.SelectedItem is ComboBoxItem item
                && item.Tag != null
                && item.Tag is int optionSetValue
            )
            {
                return optionSetValue;
            }

            return null;
        }

        public void AddAttribute(Entity entity)
        {
            var currentValue = GetIntValue();

            if (currentValue.HasValue)
            {
                entity.Attributes[AttributeMetadata.LogicalName] = new OptionSetValue(currentValue.Value);
            }
            else
            {
                entity.Attributes[AttributeMetadata.LogicalName] = null;
            }
        }

        public void AddChangedAttribute(Entity entity)
        {
            var currentValue = GetIntValue();

            if (this._fillAllways || currentValue != _initialValue)
            {
                if (currentValue.HasValue)
                {
                    entity.Attributes[AttributeMetadata.LogicalName] = new OptionSetValue(currentValue.Value);
                }
                else
                {
                    entity.Attributes[AttributeMetadata.LogicalName] = null;
                }
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            cmBValue.Focus();

            base.OnGotFocus(e);
        }

        public event EventHandler RemoveControlClicked;

        private void btnRemoveControl_Click(object sender, RoutedEventArgs e)
        {
            RemoveControlClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (_initialValue.HasValue)
            {
                foreach (var item in cmBValue.Items.OfType<ComboBoxItem>())
                {
                    if (item.Tag != null
                        && item.Tag is int optionSetValue
                        && optionSetValue == _initialValue.Value
                    )
                    {
                        cmBValue.SelectedItem = item;
                        return;
                    }
                }
            }
            else
            {
                cmBValue.SelectedIndex = 0;
            }
        }
    }
}