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

        private string idPoli;
        private string idDep;
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_dokter);
            idPoli = Intent.GetStringExtra("idPoli") ?? "Data not available";
            idDep = Intent.GetStringExtra("idDepartemen") ?? "Data tidak tersedia";
            //disini buat
            Console.Write(idDep);

            mListView = FindViewById<ListView>(Resource.Id.listDokter);
            mDokterList = new List<DokterModel>();
            CSService.WebService1 mService = new CSService.WebService1();
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

            DataSet ds = mService.GedDataDoctorbyPoly(idPoli);
            mDokterList = getDokterName(ds);
            mListView.Adapter = new AdapterDokter(this, mDokterList);
            mListView.ItemClick += MListView_ItemClick;

        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var mDokter = mDokterList[e.Position];
            var intent = new Intent(this, typeof(PertanyaanRJ));
            intent.PutExtra("idPoli", idPoli);
            intent.PutExtra("idDepartemen", idDep);
            intent.PutExtra("idDokter", mDokter.getDokterId());
            StartActivityForResult(intent, MainActivity.FINISH_QUESTION);
        }

        private List<DokterModel> getDokterName(DataSet ds)
        {
            List<DokterModel> tempDokter = new List<DokterModel>();
            DokterModel mDokter = null;

            foreach (DataRow dr in ds.Tables[0].Rows) {
                mDokter = new DokterModel();
                mDokter.setDolterId(dr["ID"].ToString());
                mDokter.setDokterName(dr["Nama"].ToString());
                tempDokter.Add(mDokter);
            }
            return tempDokter;
        }
        
    }
}