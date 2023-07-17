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
using RUN.DataFolder;

namespace RUN.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для WindowAddUser.xaml
    /// </summary>
    public partial class WindowAddUser : Window
    {
        public WindowAddUser()
        {
            InitializeComponent();
        }

        private void IAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbLogin.Text) ||
                string.IsNullOrWhiteSpace(TbPassword.Text) ||
                string.IsNullOrWhiteSpace(TbSurname.Text) ||
                string.IsNullOrWhiteSpace(TbName.Text) ||
                string.IsNullOrWhiteSpace(TbPhone.Text))
            {
                ClassMB.Information("Заполните все поля");
                TbSurname.Focus();
                TbName.Focus();
                TbLogin.Focus();
                TbPassword.Focus();
                TbPhone.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().Database.ExecuteSqlCommand($"insert into [dbo].[User](Login,Password,Surname, Name, Patronymic, Phone, IdRole) values ('{TbLogin.Text}','{TbPassword.Text}','{TbSurname.Text}', '{TbName.Text}','{TbMiddleName.Text}','{TbPhone.Text}','3');");
                    ClassMB.Information("Вы добавили пользователя");
                    this.Close();
                }
                catch (Exception ex)
                {
                    ClassMB.Error(ex.Message);
                }
            }
        }

        private void IOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
