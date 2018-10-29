/* Author: Gabor Buzasi (40338733)
 * Description: MainWindow class contains all objects 
 * and event handlers related to the presentation of 
 * the main window that opens when the application starts
 * 
 * Date last modified: 29 Oct 2018
 */

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;

namespace CustMgmtSys40338733
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MailingList store = new MailingList();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the event when the users clicks on the Add customer button, 
        /// takes care of validation and storing the record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newCustomer = new Customer();
                int customerId;

                if (!store.Ids.Any())
                {
                    customerId = 10001;
                }
                else
                {
                    customerId = store.Ids.Last() + 1;
                }

                newCustomer.Id = customerId;
                newCustomer.FirstName = txtFirstName.Text;
                newCustomer.Surname = txtSurname.Text;
                newCustomer.EmailAddress = txtEmailAddress.Text;
                newCustomer.Telephone = txtTelephone.Text;
                newCustomer.SkypeId = txtSkypeId.Text;

                if (!Enum.TryParse(cbxPreferredMethod.SelectedValue?.ToString(), out PreferredContactEnum preferredContact))
                {
                    throw new ArgumentException("Please provide a vaid option for preferred contact");
                }

                newCustomer.PreferredContact = preferredContact;
                store.Add(newCustomer);
                ApplyDatabinding();
                ClearInputs();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error:");
            }
        }

        /// <summary>
        /// Event handler that binds the accepted values of the preferred
        /// contact methods to the ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbxPreferredMethod.ItemsSource = Enum.GetValues(typeof(PreferredContactEnum)).Cast<PreferredContactEnum>();            
        }

        /// <summary>
        /// Refreshes the data for the customer listbox
        /// </summary>
        private void ApplyDatabinding()
        {
            lstCustomers.ItemsSource = null;
            lstCustomers.ItemsSource = store.Customers;
        }

        /// <summary>
        /// Event handler that fires when a customer 
        /// is selected in the lstCustomer listbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCustomer = ((ListBox)e.Source).SelectedItem;
            if (selectedCustomer != null)
            {
                LoadFormData((selectedCustomer as Customer).Id);
            }
        }

        /// <summary>
        /// Loads a customer's data into the form
        /// </summary>
        /// <param name="customerId">Id of customer</param>
        private void LoadFormData(int customerId)
        {
            var cust = store.Find(customerId);

            if (cust != null)
            {
                txtCustomerId.Text = cust.Id.ToString();
                txtFirstName.Text = cust.FirstName;
                txtSurname.Text = cust.Surname;
                txtEmailAddress.Text = cust.EmailAddress;
                txtSkypeId.Text = cust.SkypeId;
                txtTelephone.Text = cust.Telephone;
                cbxPreferredMethod.SelectedValue = cust.PreferredContact;
            }
            else
            {
                MessageBox.Show($"There is no customer exist for id {customerId}");
            }
        }

        /// <summary>
        /// Handles the event when the Delete button is clicked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtCustomerId.Text, out int customerId))
            {
                store.Delete(customerId);
                ApplyDatabinding();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Please enter a valid customer id");
            }
        }

        /// <summary>
        /// Clears all inputs on the stackpanel that contains all
        /// input fields
        /// </summary>
        private void ClearInputs()
        {
            foreach (var ctl in container.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    ((TextBox)ctl).Text = string.Empty;
                }
                    
                if (ctl.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)ctl).SelectedItem = null;
                }
            }
        }

        /// <summary>
        /// Handles the event for the Find button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtCustomerId.Text, out int customerId))
            {
                LoadFormData(customerId);
            }
            else
            {
                MessageBox.Show("Please enter a valid customer id");
            }
        }

        /// <summary>
        /// Opens the Customer Listing window and 
        /// binds the list of customers as datasource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListAll_Click(object sender, RoutedEventArgs e)
        {
            ListingCustomers listingCustomersWindow = new ListingCustomers();
            listingCustomersWindow.lstCustomers.ItemsSource = store.Customers;
            listingCustomersWindow.Show();
        }
    }
}
