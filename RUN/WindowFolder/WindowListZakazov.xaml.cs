﻿using RUN.DataFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RUN.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для WindowListZakaz.xaml
    /// </summary>
    public partial class WindowListZakazov : Window
    {
        public WindowListZakazov()
        {
            InitializeComponent();
            DgZakazi.ItemsSource = DBEntities.GetContext().Zakaz.ToList().OrderBy(u => u.Status.Name);
        }

        private void updateDataGrid()
        {
            DgZakazi.ItemsSource = DBEntities.GetContext().Zakaz.ToList().OrderBy(u => u.Status.Name);
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DgZakazi.ItemsSource = DBEntities.GetContext().Zakaz.Where
                    (c => c.User.Name.StartsWith(tbSearch.Text)
                    || c.User.Surname.StartsWith(tbSearch.Text)).ToList();

                if (DgZakazi.Items.Count < 1)
                    ClassMB.Error("Не найдено");
            }
            catch (Exception ex)
            {
                ClassMB.MBError(ex);
            }
        }

        private void IOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IDel_Click(object sender, MouseButtonEventArgs e)
        {
            if (DgZakazi.SelectedItem == null)
                return;
            if (MessageBox.Show("Вы действительно хотите удалить заказ?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                DBEntities.GetContext().Database.ExecuteSqlCommand($"delete from [Zakaz] where IdZakaz = ('{(DgZakazi.SelectedItem as Zakaz).IdZakaz}')");
            else
            {
                return;
            }
            updateDataGrid();
            ClassMB.Information("Вы успешно удалили заказ");
        }
    }
}
