using RUN.DataFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RUN.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для WindowListUsers.xaml
    /// </summary>
    public partial class WindowListUsers : Window
    {

        public WindowListUsers()
        {
            InitializeComponent();
            DgClient.ItemsSource = DBEntities.GetContext().User.ToList().Where(u => u.Role.Name == "Пользователь");
        }

        private void updateDataGrid()
        {
            DgClient.ItemsSource = DBEntities.GetContext().User.ToList().Where(u => u.Role.Name == "Пользователь");
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DgClient.ItemsSource = DBEntities.GetContext().User.Where
                    (c => c.Surname.StartsWith(tbSearch.Text)
                    || c.Name.StartsWith(tbSearch.Text)).ToList().Where(c => c.Role.Name == "Пользователь");

                if (DgClient.Items.Count < 1)
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

        private void DgClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new WindowEditUser(DgClient.SelectedItem as User).ShowDialog();
        }

        private void IAdd_Click(object sender, MouseButtonEventArgs e)
        {
            new WindowAddUser().ShowDialog();
            updateDataGrid();
        }

        private void IDel_Click(object sender, MouseButtonEventArgs e)
        {
            if (DgClient.SelectedItem == null)
                return;
            if(MessageBox.Show("Вы действительно хотите удалить пользователя?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) 
            DBEntities.GetContext().Database.ExecuteSqlCommand($"delete from [User] where IdUser = ('{(DgClient.SelectedItem as User).IdUser}')");
            else
            {
                return;
            }
            updateDataGrid();
            ClassMB.Information("Вы успешно удалили пользователя");
        }
    }
}
