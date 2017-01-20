using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Data;
using System;
using Android.Content;
using CSApplication.Model;
using CSApplication.Fragments;

namespace CSApplication
{
    [Activity(Label = "Customer Satisfaction", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
       
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
            StartActivity(intent);
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
    }
}
