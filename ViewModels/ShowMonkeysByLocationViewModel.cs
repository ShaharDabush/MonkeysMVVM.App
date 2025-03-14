﻿using MonkeysMVVM.Models;
using MonkeysMVVM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonkeysMVVM.ViewModels
{
    public class ShowMonkeysByLocationViewModel : ViewModelBase
    {
        private string name;
        
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                OnPropertyChanged();
            }
        }
        private string location;
        public string Location
        {
            get { return this.location; }
            set
            {
                this.location = value;
                OnPropertyChanged();
            }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return this.imageUrl; }
            set
            {
                this.imageUrl = value;
                OnPropertyChanged();
            }
        }

        private int overAllMonkeys;
        public int OverAllMonkeys
        {
            get { return this.overAllMonkeys; }
            set
            {
                this.overAllMonkeys = value;
                OnPropertyChanged();
            }
        }

        public Command GetMonkeyCommand { get; set; }

        public ShowMonkeysByLocationViewModel()
        {
            GetMonkeyCommand = new Command(GetMonkey);

            
        }

        private async void GetMonkey()
        {
            MonkeysService service = new MonkeysService();
            List<Monkey> list = await service.GetMonkeysByLocation(Location);
            OverAllMonkeys = list.Count;
            if (list.Count > 0)
            {
                Name = list[0].Name;
                Location = list[0].Location;
                ImageUrl = list[0].ImageUrl;
            }
            else
            {
                Name = "Not Found!";
                ImageUrl = "";
            }
            
        }

    
        #region Single Selection
        private Object selectedMonkey;
        public Object SelectedMonkey
        {
            get
            {
                return this.selectedMonkey;
            }
            set
            {
                this.selectedMonkey = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectMonkey);

        async void OnSingleSelectMonkey()
        {
            if (SelectedMonkey != null)
            {
                var navParam = new Dictionary<string, object>()
                {
                    { "selectedMonkey",SelectedMonkey }
                };
                await Shell.Current.GoToAsync($"monkeyDetails", navParam);
                SelectedMonkey = null;
            }
        }


        #endregion
    }
}
