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

namespace CSApplication.Activities
{
    [Activity(Label = "DetailPertanyaanActivity")]
    public class DetailPertanyaanActivity : Activity
    {
        private List<DetailPertanyaanModel> mDetailList;
        private ListView mListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_detail_pertanyaan);
            string id = Intent.GetStringExtra("idPertanyaan") ?? "Data tidak tersedia";

            mListView = FindViewById<ListView>(Resource.Id.listDetailPertanyaan);
            mDetailList = new List<DetailPertanyaanModel>();

            CSService.WebService1 mService = new CSService.WebService1();

            DataSet ds = mService.GetDetailPertanyaanByPertanyaan(id);
            mDetailList = getDetailPertanyaan(ds);
            mListView.Adapter = new Adapter.AdapterDetailPertanyaan(this, mDetailList);
        }

        private List<DetailPertanyaanModel> getDetailPertanyaan(DataSet ds)
        {
            List<DetailPertanyaanModel> tempDetail = new List<DetailPertanyaanModel>();
            DetailPertanyaanModel mDetail = null;

            foreach (DataRow dr in ds.Tables[0].Rows) {
                mDetail = new DetailPertanyaanModel();
                mDetail.setIdDetailPertanyaan(dr["Id_Detail_Pertanyaan"].ToString());
                mDetail.setIdPertanyaan(dr["Id_Pertanyaan"].ToString());
                mDetail.setDetailPertanyaan(dr["detail_pertanyaan"].ToString());
                tempDetail.Add(mDetail);
            }
            return tempDetail;
        }
    }
}