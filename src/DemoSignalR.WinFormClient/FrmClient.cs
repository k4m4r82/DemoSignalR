using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.AspNet.SignalR.Client;
using DemoSignalR.Model;

namespace DemoSignalR.WinFormClient
{
    public partial class FrmClient : Form
    {
        private const string SERVER_URL = "http://192.168.56.1:8282";

        private HubConnection hubConnection;
        private IHubProxy hubProxy;

        public FrmClient()
        {
            InitializeComponent();

            InisialisasiListView();

            hubConnection = new HubConnection(SERVER_URL);
            
            // membuat objek proxy dari class hub yang ada di server
            hubProxy = hubConnection.CreateHubProxy("ServerHub");

            // set mode listen untuk method RefreshData
            // method RefreshData sebelumnya harus didefinisikan di server
            hubProxy.On<Customer>("RefreshData", (customer) => RefreshData(customer));
            ConnectAsync();
        }

        private void RefreshData(Customer customer)
        {
            lvwCustomer.Invoke(new MethodInvoker(() => FillToListView(customer)));
        }

        /// <summary>
        /// Creates and connects the hub connection and hub proxy.
        /// </summary>
        private async void ConnectAsync()
        {
            try
            {
                await hubConnection.Start();
                btnSave.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Unable to connect to server: Start server before connecting clients.");
                return;
            }

        }

        // pengaturan propery listview
        private void InisialisasiListView()
        {
            lvwCustomer.View = System.Windows.Forms.View.Details;
            lvwCustomer.FullRowSelect = true;
            lvwCustomer.GridLines = true;

            lvwCustomer.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwCustomer.Columns.Add("Customer Id", 70, HorizontalAlignment.Left);
            lvwCustomer.Columns.Add("Company Name", 150, HorizontalAlignment.Left);
            lvwCustomer.Columns.Add("Contact Name", 155, HorizontalAlignment.Left);
        }

        // method untuk menampilkan data customer ke listview
        private void FillToListView(Customer customer)
        {
            int noUrut = lvwCustomer.Items.Count + 1;

            ListViewItem item = new ListViewItem(noUrut.ToString());
            item.SubItems.Add(customer.CustomerId);
            item.SubItems.Add(customer.CompanyName);
            item.SubItems.Add(customer.ContactName);

            lvwCustomer.Items.Add(item);
        }

        private void FrmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hubConnection != null)
            {
                hubConnection.Stop();
                hubConnection.Dispose();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var customer = new Customer
            {
                CustomerId = txtCustomerId.Text,
                CompanyName = txtCompanyName.Text,
                ContactName = txtContactName.Text
            };

            // panggil method server
            hubProxy.Invoke("AddCustomer", customer);
        }
    }
}
