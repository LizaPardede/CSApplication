using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CSApplication.Model;
using System.Data;
using CSApplication.Adapter;

namespace CSApplication.Activities
{
    [Activity(Label = "DokterActivity")]
    public class DokterActivity : Activity
    {
        private List<DokterModel> mDokterList;
        private ListView mListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_dokter);
            string id = Intent.GetStringExtra("idPoli") ?? "Data not available";

            mListView = FindViewById<ListView>(Resource.Id.listDokter);
            mDokterList = new List<DokterModel>();
            CSService.WebService1 mService = new CSService.WebService1();

            DataSet ds = mService.GedDataDoctorbyPoly(id);
            mDokterList = getDokterName(ds);
            mListView.Adapter = new AdapterDokter(this, mDokterList);
            mListView.ItemClick += MListView_ItemClick;

        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var mDokter = mDokterList[e.Position];
            var intent = new Intent(this, typeof(PertanyaanActivity));
            intent.PutExtra("idDepartemen", mDokter.getDepartemenId ());
            StartActivity(intent);
        }

        private List<DokterModel> getDokterName(DataSet ds)
        {
            List<DokterModel> tempDokter = new List<DokterModel>();
            DokterModel mDokter = null;

            foreach (DataRow dr in ds.Tables[0].Rows) {
                mDokter = new DokterModel();
                mDokter.setDokterId(dr["ID"].ToString());
                mDokter.setDokterName(dr["Nama"].ToString());
                tempDokter.Add(mDokter);
            }
            return tempDokter;
        }
    }
}