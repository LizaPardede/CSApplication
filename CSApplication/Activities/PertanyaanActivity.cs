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
using CSApplication.Fragments;

namespace CSApplication.Activities
{
    [Activity(Label = "PertanyaanActivity")]
    public class PertanyaanActivity : Activity
    {
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
                var pertanyaanFragmen = new PertanyaanFragment();
                
                FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
                fragmentTx.Add(Resource.Id.listPertanyaan, pertanyaanFragmen);
                fragmentTx.Commit();
            }
            else if (id == "Dep2")
            {
                ds = mService.GetDataPoly();
                var intent = new Intent(this, typeof(PoliActivity));
                StartActivity(intent);
            }
        }

        private List<DetailPertanyaanModel> getDetailPertanyaan(DataSet ds1)
        {
            List<DetailPertanyaanModel> tempDetail = new List<DetailPertanyaanModel>();
            DetailPertanyaanModel mDetail = null;

            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                mDetail = new DetailPertanyaanModel();
                mDetail.setIdDetailPertanyaan(dr["Id_Detail_Pertanyaan"].ToString());
                mDetail.setDetailPertanyaan(dr["detail_pertanyaan"].ToString());
                tempDetail.Add(mDetail);
            }
            return tempDetail;
        }

        /*private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var mPertanyaan = mPertanyaanList[e.Position];
            var intent = new Intent(this, typeof(DetailPertanyaanActivity));
            intent.PutExtra("idPertanyaan", mPertanyaan.getPertanyaanId());
            StartActivity(intent);
        }*/

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