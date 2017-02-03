using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Data;
using System;
using Android.Content;
using CSApplication.Model;
using CSApplication.Fragments;
using Android.Runtime;
using Android.Views;

namespace CSApplication
{
    [Activity(Label = "Customer Satisfaction", Theme = "@style/Theme.NoTitle", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        public const int FINISH_QUESTION = 0;

        private List<ModelDepartemen> mDepartemenList;
        private ListView mListView;
       

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            

            CSService.WebService1 mService = new CSService.WebService1();
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

            mListView = FindViewById<ListView>(Resource.Id.listDepartemen);
            mDepartemenList = new List<ModelDepartemen>();

            DataSet ds = mService.GetDataDepartmen();
            mDepartemenList = getDepartemen(ds);

            mListView.Adapter = new Adapter.AdapterDepartemen(this, mDepartemenList);
            mListView.ItemClick += MListView_ItemClick;
            
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var mDepartemen = mDepartemenList[e.Position];
            //Toast.MakeText(this, mDepartemen.getDepartemenId(), ToastLength.Long).Show();
            var intent = new Intent(this, typeof(Activities.PertanyaanPager));
            intent.PutExtra("idDepartemen", mDepartemen.getDepartemenId());
            StartActivityForResult(intent, FINISH_QUESTION);
            //StartActivity(intent);
        }

        private List<ModelDepartemen> getDepartemen(DataSet ds)
        {
            List<ModelDepartemen> tempDepartemen = new List<ModelDepartemen>();
            ModelDepartemen mDepartemen = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                mDepartemen = new ModelDepartemen();
                mDepartemen.setDepartemenId(dr["Departemen_ID"].ToString());
                mDepartemen.setDepartemenName(dr["Departemen_Nama"].ToString());
                tempDepartemen.Add(mDepartemen);
            }
            return tempDepartemen;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            
            if (resultCode == Result.Ok)
            {
                if (resultCode == FINISH_QUESTION) {
                    Toast.MakeText(this, "Thanks", ToastLength.Short).Show();
                    Console.WriteLine("Pantaaaaaaaaaaaaaaaaiiiiiiiiiiii");

                }
            }
        }
    }
}
