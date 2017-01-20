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
using Java.Interop;

namespace CSApplication.Activities
{
    [Activity(Label = "Customer Satisfaction")]
    public class PertanyaanActivity : Activity
    {
        [Export("radioButton_Click")]
        
        public void radioButton_OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.radioButton1:
                    Toast.MakeText(this, "No!", ToastLength.Short).Show();
                    
                    break;

                case Resource.Id.radioButton2:
                    Toast.MakeText(this, "Yes!", ToastLength.Short).Show();
                    break;
            }
        }

     

        private List<PertanyaanModel> mPertanyaanList;
        private ListView mListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_pertanyaan);
            string id = Intent.GetStringExtra("idDepartemen") ?? "Data tidak tersedia";

            DataSet ds;
            CSService.WebService1 mService = new CSService.WebService1();
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

            if (id == "Dep1" || id == "Dep3")
            {
                //ds = mService.GetPertanyaanByDepartmen(id);
                //mListView = FindViewById<ListView>(Resource.Id.listPertanyaan);

                //mPertanyaanList = new List<PertanyaanModel>();
                //mPertanyaanList = getPertanyaan(ds);

                //mListView.Adapter = new AdapterPertanyaan(this, mPertanyaanList);
                //mListView.ItemClick += MListView_ItemClick;

                List<PertanyaanModel> listPertanyaan = Data.listPertanyaan(id);
                var listView = FindViewById<ExpandableListView>(Resource.Id.expandableListView1);
                listView.SetAdapter(new ExpandableDataAdapter(this, listPertanyaan));

                Button btnKirim = FindViewById<Button>(Resource.Id.btnKirim);
                btnKirim.Click += delegate
                {
                    var intent = new Intent(this, typeof(Result));
                    StartActivity(intent);
                };

            }

            else if (id == "Dep2")
            {
                ds = mService.GetDataPoly();
                var intent = new Intent(this, typeof(PoliActivity));
                StartActivity(intent);
            }
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