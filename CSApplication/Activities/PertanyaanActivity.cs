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
    [Activity(Label = "PertanyaanActivity")]
    public class PertanyaanActivity : Activity
    {
        private List<PertanyaanModel> mPertanyaanList;
        private ListView mListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_pertanyaan);
            string id = Intent.GetStringExtra("idDepartemen") ?? "Data tidak tersedia";

            mListView = FindViewById<ListView>(Resource.Id.listPertanyaan);
            mPertanyaanList = new List<PertanyaanModel>();
            CSService.WebService1 mService = new CSService.WebService1();

            DataSet ds = mService.GetPertanyaanByDepartmen(id);
            mPertanyaanList = getPertanyaan(ds);
            mListView.Adapter = new AdapterPertanyaan(this, mPertanyaanList);
            mListView.ItemClick += MListView_ItemClick;
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var mPertanyaan = mPertanyaanList[e.Position];
            var intent = new Intent(this, typeof(DetailPertanyaanActivity));
            intent.PutExtra("idPertanyaan", mPertanyaan.getPertanyaanId());
            StartActivity(intent);
        }

        private List<PertanyaanModel> getPertanyaan(DataSet ds)
        {
            List<PertanyaanModel> tempDokter = new List<PertanyaanModel>();
            PertanyaanModel mPertanyaan = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                mPertanyaan = new PertanyaanModel();
                mPertanyaan.setPertanyaanId(dr["Id_Pertanyaan"].ToString());
                mPertanyaan.setPertanyaanNama(dr["Nama_Pertanyaan"].ToString());
                tempDokter.Add(mPertanyaan);
            }
            return tempDokter;
        }
    }
}