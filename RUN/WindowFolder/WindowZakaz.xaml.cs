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
    /// Логика взаимодействия для WindowZakaz.xaml
    /// </summary>
    public partial class WindowZakaz : Window
    {
        public WindowZakaz()
        {
            InitializeComponent();
            CbProduct.ItemsSource = DBEntities.GetContext().Product.ToList().OrderBy(r => r.Name);
            CbRole.ItemsSource = DBEntities.GetContext().User.ToList().Where(r => r.Role.IdRole == 3);
        }

        private void IZakaz_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CbRole.Text) ||
                string.IsNullOrWhiteSpace(CbProduct.Text) ||
                string.IsNullOrWhiteSpace(TbAmount.Text) ||
                string.IsNullOrWhiteSpace(TbAddress.Text))
            {
                ClassMB.Error("Необходимо заполнить все поля");
                return;
            }
            try
            {
                DBEntities.GetContext().Database.ExecuteSqlCommand($"insert into [Zakaz] (Amount,Address, IdProduct, IdStatus,IdUser) values ('{TbAmount.Text}','{TbAddress.Text}','{CbProduct.SelectedValue}','1','{CbRole.SelectedValue}');");

                DBEntities.GetContext().SaveChanges();
                ClassMB.Information("Вы успешно заказали продукты");
                this.Close();
                new WindowAuth().ShowDialog();
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

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
            return;
        }

    }
}
