﻿using System;
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
    /// Логика взаимодействия для WindowReg.xaml
    /// </summary>
    public partial class WindowReg : Window
    {
        public WindowReg()
        {
            InitializeComponent();
        }

        private void IOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new WindowAuth().ShowDialog();
        }

        private void IReg_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbLogin.Text) ||
                string.IsNullOrWhiteSpace(PbPassword.Password) ||
                string.IsNullOrWhiteSpace(TbSurname.Text) ||
                string.IsNullOrWhiteSpace(TbName.Text) ||
                string.IsNullOrWhiteSpace(TbPhone.Text))
            {
                ClassMB.Error("Необходимо заполнить все поля");
                return;
            }
            else if (string.IsNullOrWhiteSpace(PbRepeatPassword.Password))
            {
                ClassMB.Information("Повторите пароль");
                return;
            }
            else if (PbPassword.Password != PbRepeatPassword.Password)
            {
                ClassMB.Error("Введенные пароли не совпадают");
                return ;
            }
            else if (DBEntities.GetContext().User.FirstOrDefault(u => u.Login ==
                TbLogin.Text) != null)
            {
                ClassMB.Information("Пользователь с данным логином уже есть");
                return;
            }
            else
            {
                try
                {
                    DBEntities.GetContext().User.Add(new User()
                    {
                        IdUser = 2,
                        Login = TbLogin.Text,
                        Password = PbPassword.Password,
                        Surname = TbSurname.Text,
                        Name = TbName.Text,
                        Patronymic = TbPatronymic.Text,
                        Phone = TbPhone.Text,
                        IdRole = 3,
                    });
                    DBEntities.GetContext().SaveChanges();
                    ClassMB.Information("Вы успешно зарегистрировались");
                    this.Close();
                }
                catch (Exception ex)
                {
                    ClassMB.MBError(ex);
                }
            }

        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
            return ;
        }

        private void HidePassword_Click(object sender, MouseButtonEventArgs e)
        {
            PbPassword.Password = TbPassword.Text;
            TbPassword.Visibility = Visibility.Hidden;
            PbPassword.Visibility = Visibility.Visible;
            HidePassword.Visibility = Visibility.Hidden;
            ShowPassword.Visibility = Visibility.Visible;
        }
        private void ShowPassword_Click(object sender, MouseButtonEventArgs e)
        {
            TbPassword.Text = PbPassword.Password;
            TbPassword.Visibility = Visibility.Visible;
            PbPassword.Visibility = Visibility.Hidden;
            HidePassword.Visibility = Visibility.Visible;
        }
    }

}