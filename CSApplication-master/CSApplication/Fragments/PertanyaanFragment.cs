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
using Android.Preferences;

namespace CSApplication.Fragments
{
    class PertanyaanFragment:Fragment
    {
        private List<PertanyaanModel> mPertanyaanList;
        private ListView mListView;

        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.layout_pertanyaan_fragment, container, false);
            CSService.WebService1 mService = new CSService.WebService1();
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";
            
            string id = this.Activity.Intent.GetStringExtra("idDepartemen") ?? "Data tidak tersedia";


            Toast.MakeText(this.Activity, id, ToastLength.Long).Show();
            //TextView txtView = view.FindViewById<TextView>(Resource.Id.textView1);
            //txtView.Text = id;


          mListView = view.FindViewById<ListView>(Android.Resource.Id.List);

          mPertanyaanList = new List<PertanyaanModel>();

          DataSet ds = mService.GetPertanyaanByDepartmen(id);
          mPertanyaanList = getPertanyaan(ds);

          mListView.Adapter = new AdapterPertanyaan(this, mPertanyaanList);
          return view;
        }

        private List<PertanyaanModel> getPertanyaan(DataSet ds)
        {
            List<PertanyaanModel> tempPertanyaan = new List<PertanyaanModel>();
            PertanyaanModel mPertanyaan;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                mPertanyaan = new PertanyaanModel();
                mPertanyaan.setPertanyaanId(dr["Id_Pertanyaan"].ToString() ?? "hello");
                mPertanyaan.setPertanyaanNama(dr["Nama_Pertanyaan"].ToString() ?? "halo");
                tempPertanyaan.Add(mPertanyaan);
            }
            return tempPertanyaan;
        }


    }
}