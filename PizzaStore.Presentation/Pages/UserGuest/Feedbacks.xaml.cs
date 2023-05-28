using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using PizzaStore.Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaStore.Presentation.Pages
{
    public sealed partial class Feedbacks : Page
    {
        public Feedbacks()
        {
            this.InitializeComponent();
        }
        private Database database;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is Database)
            {
                database = (Database)e.Parameter;
                FeedbacksSavingData frep = new FeedbacksSavingData(database);
                string path_feedbacks = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "feedbacks.json");
                database.FeedbacksList = frep.ReadFromJson(path_feedbacks);
                feedbackListView.ItemsSource = database.FeedbacksList;
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string path_feedbacks = "";
            if (LogInUser.InAccount)
            {
                string feedbacktext = FeedbackTextbox.Text;
                FeedbackTextbox.Text = "";
                string name = LogInUser.FirstName+ " " + LogInUser.LastName;
                UserAction userAction = new UserAction(database);
                if (!string.IsNullOrEmpty(feedbacktext))
                {
                    userAction.LeaveFeedback(feedbacktext, LogInUser.Id, DateTime.Now, name);
                    FeedbacksSavingData feedbacksRepository = new FeedbacksSavingData(database);
                    path_feedbacks = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "feedbacks.json");
                    feedbacksRepository.SaveDataToJson(database.FeedbacksList, path_feedbacks);
                }
               
            }
            else
            {
                FeedbackTextbox.Text = "";
                await new MessageDialog("You should log in.").ShowAsync();
            }
            FeedbacksSavingData frep = new FeedbacksSavingData(database);
            path_feedbacks = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "feedbacks.json");
            database.FeedbacksList = frep.ReadFromJson(path_feedbacks);
            feedbackListView.ItemsSource = database.FeedbacksList;
        }
    }
}
