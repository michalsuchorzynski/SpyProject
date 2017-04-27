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
using SpyAdminApplication.ServiceReference1;
using SpyAdminApplication.Pages;

namespace SpyAdminApplication.Windows
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        private AcceptablePagesPage sourcePage;
        private AddWindowType type;
        private ListBox target;

        public AddItemWindow(AcceptablePagesPage sourcePage, AddWindowType type)
        {
            this.type = type;
            this.sourcePage = sourcePage;
            InitializeComponent();
            GenerateContent();
        }
        public AddItemWindow(AcceptablePagesPage sourcePage, AddWindowType type, ListBox target) : this(sourcePage, type)
        {
            this.target = target;
        }
        private void GenerateContent()
        {
            switch (type)
            {
                case AddWindowType.AddPage:
                    this.labelTittle.Content = "Nowa akceptowalna strona";
                    this.labelField1.Content = "Podaj adres url strony";
                    break;
                case AddWindowType.AddGroup:
                    this.labelTittle.Content = "Nowa grupa stron";
                    this.labelField1.Content = "Podaj nazwe grupy";
                    break;
                default:
                    break;
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            foreach(ListBoxItem l_item in this.target.Items)
            {
                if (l_item.Content.ToString() == this.textboxField1.Text)
                {
                    MessageBox.Show("Podana dana już istnieje");
                    return;
                }
            }
            switch(type)
            {
                case AddWindowType.AddPage:
                case AddWindowType.AddGroup:
                    this.target.Items.Add(new ListBoxItem()
                    {
                        Content = this.textboxField1.Text
                    });
                    break;
            }
            using (ClientServiceClient client = new ClientServiceClient())
            {
                switch (type)
                {
                    case AddWindowType.AddPage:
                        client.AddAcceptablePage(new AcceptablePage()
                        {
                            Url = this.textboxField1.Text,
                        });
                        sourcePage._pagescontrol.GenerateAcceptablePages(client, this.target);
                        break;
                    case AddWindowType.AddGroup:
                        client.AddPagesGroup(new AcceptablePagesGroup()
                        {
                            Name = this.textboxField1.Text
                        });
                        sourcePage._pagescontrol.GenerateAcceptablePagesGroups(client, this.target);
                        break;
                    default:
                        break;
                }
            }
            this.Close();
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    public enum AddWindowType
    {
        AddPage,
        AddGroup
        
    } 
}
