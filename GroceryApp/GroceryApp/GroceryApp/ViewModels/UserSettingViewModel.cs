using GroceryApp.Data;
using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.ViewModels
{
    public interface IUserSettingViewModel
    {

    }
    public class UserSettingViewModel: BaseViewModel, IUserSettingViewModel
    {
        
        DataProvider dataProvider = DataProvider.GetInstance();

        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); }
        }

        public UserSettingViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            CurrentUser = dataProvider.GetUserByIDUser(Infor.IDUser);
            BirthDate = CurrentUser.BirthDate;
        }

    }
}
