﻿using Microsoft.Xrm.Sdk;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Views
{
    public class WindowEntitySelect<T> : WindowEntitySelect where T : Entity
    {
        private readonly ObservableCollection<T> _itemsSource;

        private readonly Func<string, Task<IEnumerable<T>>> _listGetter;

        public T SelectedEntity { get; private set; }

        public WindowEntitySelect(
            IWriteToOutput outputWindow
            , ConnectionData connectionData
            , string entityName
            , Func<string, Task<IEnumerable<T>>> listGetter
            , IEnumerable<DataGridColumn> columns
        ) : base(outputWindow, connectionData, entityName, columns)
        {
            this._listGetter = listGetter;

            this._itemsSource = new ObservableCollection<T>();

            this.lstVwEntities.ItemsSource = _itemsSource;

            ShowExistingEntities();
        }

        protected override async Task ShowExistingEntities()
        {
            if (!_controlsEnabled)
            {
                return;
            }

            ToggleControls(false, Properties.WindowStatusStrings.LoadingEntities);

            this._itemsSource.Clear();

            string filter = string.Empty;

            txtBFilterEnitity.Dispatcher.Invoke(() =>
            {
                filter = txtBFilterEnitity.Text.Trim();
            });

            IEnumerable<T> list = Enumerable.Empty<T>();

            try
            {
                if (_listGetter != null)
                {
                    list = await _listGetter(filter);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(ex);

                list = Enumerable.Empty<T>();
            }

            this.lstVwEntities.Dispatcher.Invoke(() =>
            {
                foreach (var entity in list)
                {
                    _itemsSource.Add(entity);
                }

                if (_itemsSource.Count == 1)
                {
                    this.lstVwEntities.SelectedItem = this.lstVwEntities.Items[0];
                }
            });

            ToggleControls(true, Properties.WindowStatusStrings.LoadingEntitiesCompletedFormat1, list.Count());
        }

        private T GetSelectedEntity()
        {
            return this.lstVwEntities.SelectedItems.OfType<T>().Count() == 1
                ? this.lstVwEntities.SelectedItems.OfType<T>().SingleOrDefault() : null;
        }

        protected override void SelectEntityAction(Entity Entity)
        {
            if (!_controlsEnabled)
            {
                return;
            }

            if (Entity == null)
            {
                return;
            }

            this.SelectedEntity = Entity.ToEntity<T>();

            this.DialogResult = true;

            this.Close();
        }

        protected override void btnSelectEntity_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            SelectEntityAction(entity);
        }
    }
}
