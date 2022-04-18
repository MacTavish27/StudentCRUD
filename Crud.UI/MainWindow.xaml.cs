using Crud.domain.Model;
using Crud.EF;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crud.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudentCrudServices _crudServices;
        public MainWindow()
        {
            InitializeComponent();
            _crudServices = new StudentCrudServices();

            ButtonAdd.Click += ButtonAdd_Click;
            ButtonList.Click += ButtonList_Click;
            DataGridBrand.SelectionChanged += DataGridBrand_SelectionChanged;

            ButtonEdit.Click += ButtonEdit_Click;

            ButtonDelete.Click += ButtonDelete_Click;


            ButtonSearch.Click += ButtonSearch_Click;
            
        }

        private async void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (txtStudentID.Text != string.Empty && txtStudent.Text != string.Empty)
                {
                    await _crudServices.UpdateBrand(int.Parse(txtStudentID.Text), txtStudent.Text, txtCourse.Text);
                    throw new Exception("Data Successfully Updateddd");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ListBrands();
            }


        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtStudentID.Text != string.Empty && txtStudent.Text != string.Empty && txtCourse.Text != string.Empty)
                {
                    await _crudServices.DeleteBrand(int.Parse(txtStudentID.Text));
                    throw new Exception("Data Successfully Deleteddd");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ListBrands();
            }
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            var SearchName = await _crudServices.SearchBrandByName(txtStudent.Text);
            DataGridBrand.ItemsSource = SearchName.ToList();
        }

        private void DataGridBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var activelist = (Student)DataGridBrand.CurrentItem;

                if (activelist != null)
                {
                    txtStudentID.Text = activelist.id.ToString();
                    txtStudent.Text = activelist.stname;
                    txtCourse.Text = activelist.course;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ButtonList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await ListBrands();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task ListBrands()
        {
            progressBar.IsIndeterminate = true;
            progressBar.Visibility = Visibility.Visible;
            var brandList = await _crudServices.ListBrands();
            DataGridBrand.ItemsSource = brandList.ToList();
            progressBar.IsIndeterminate = false;
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _crudServices.AddBrand(txtStudent.Text, txtCourse.Text);


                throw new Exception("Data Successfully Addedd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                txtStudentID.Clear();
                txtStudent.Clear();
                txtCourse.Clear();
                txtStudentID.Focus();
            }
        }
    }


}
