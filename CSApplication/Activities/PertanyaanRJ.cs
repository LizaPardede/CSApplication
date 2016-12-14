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
    [Activity(Label = "PertanyaanRJ")]
    public class PertanyaanRJ : Activity
    {
        private List<PertanyaanModel> mPrjList;
        private ListView mListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_pertanyaan_rj);
            string idDep = Intent.GetStringExtra("idDepartemen") ?? "Data tidak tersedia";

            mListView = FindViewById<ListView>(Resource.Id.listPertanyaanRj);
            mPrjList = new List<PertanyaanModel>();
            CSService.WebService1 mService = new CSService.WebService1();
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

            DataSet ds = mService.GetPertanyaanByDepartmen(idDep);
            mPrjList = getPrj(ds);
            mListView.Adapter = new AdapterPertanyaan(this, mPrjList);
            mListView.ItemClick += MListView_ItemClick;
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var mPertanyaan = mPrjList[e.Position];
            var intent = new Intent(this, typeof(DetailPertanyaanActivity));
            intent.PutExtra("idPertanyaan", mPertanyaan.getPertanyaanId());
            StartActivity(intent);
        }

        private List<PertanyaanModel> getPrj(DataSet ds)
        {
            List<PertanyaanModel> tempPrj = new List<PertanyaanModel>();
            PertanyaanModel mPrj = null;

            foreach (DataRow dr in ds.Tables[0].Rows) {
                mPrj = new PertanyaanModel();
                mPrj.setDepId(dr["Departemen_ID"].ToString());
                mPrj.setPertanyaanId(dr["Id_Pertanyaan"].ToString());
                mPrj.setPertanyaanNama(dr["Nama_Pertanyaan"].ToString());
                tempPrj.Add(mPrj);
            }
            return tempPrj;
        }
    }
}