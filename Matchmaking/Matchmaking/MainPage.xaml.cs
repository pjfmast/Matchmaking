using Matchmaking.Controls;
using System;
using Xamarin.Forms;

namespace Matchmaking
{
    public partial class MainPage : ContentPage
    {
        private int _likeCount;
        private int _denyCount;

        public MainPage()
        {
            InitializeComponent();
            AddInitialPhotos();
        }

        private void AddInitialPhotos()
        {
            for (int i = 0; i < 10; i++)
            {
                InsertPhoto();
            }
        }

        // todo - in chapter 4 the focus is on a custom control, animation and gesture handling
        //          and does not use mvvm for this example. To use mvvm change OnDeny and OnLike to ICommands
        private void InsertPhoto()
        {
            var photo = new SwiperControl();
            photo.OnDeny += Handle_OnDeny;
            photo.OnLike += Handle_OnLike;

            this.MainGrid.Children.Insert(0, photo);
        }

        private void UpdateGui()
        {
            likeLabel.Text = _likeCount.ToString();
            denyLabel.Text = _denyCount.ToString();
        }

        private void Handle_OnLike(object sender, EventArgs e)
        {
            _likeCount++;
            InsertPhoto();
            UpdateGui();
        }

        private void Handle_OnDeny(object sender, EventArgs e)
        {
            _denyCount++;
            InsertPhoto();
            UpdateGui();
        }
    }
}
