using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using De.Hdodenhof.CircleImageView;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private CircleImageView ivdog;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            IntializeViews();
        }
        private void IntializeViews()
        {
            ivdog = FindViewById<CircleImageView>(Resource.Id.ivdog);
            ImageView click = FindViewById<ImageView>(Resource.Id.ivcamera);
            click.Click += Click_Click;
        }

        private void Click_Click(object sender, System.EventArgs e)
        {
            ShowDialog();
        }
        private void ShowDialog()
        {
            Android.App.AlertDialog.Builder alertDiag = new Android.App.AlertDialog.Builder(this);
            alertDiag.SetTitle("select");
            alertDiag.SetMessage("select one of the options");
            alertDiag.SetCancelable(true);

            alertDiag.SetPositiveButton("GALLERY", (senderAlert, args)=>
            {
                Intent = new Intent();
                Intent.SetType("image/*");
                Intent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent, 1);

            });

            alertDiag.SetNegativeButton("CAMERA", (senderAlert, args)=>
            {
                Intent takePicture = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(takePicture, 0);
            });
            Dialog diag = alertDiag.Create();
            diag.Show();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == 0)    
            {
                if (resultCode == Result.Ok)
                {
                    Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                    ivdog.SetImageBitmap(bitmap);
                }
            }

            else                    
            {
                if (resultCode == Result.Ok)
                {
                    Android.Net.Uri uri = data.Data;

                    // Convert to Bitmap
                    ImageDecoder.Source source =
                    ImageDecoder.CreateSource(ContentResolver, uri);
                    Bitmap bitmap = ImageDecoder.DecodeBitmap(source);
                    ivdog.SetImageBitmap(bitmap);
                }
            }

        }
    }
}