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

namespace CSApplication.Fragments
{
    class PertanyaanFragment : Fragment
    {
        private List<PertanyaanModel> mPertanyaanList;
        private ListView mListView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.layout_pertanyaan_fragment, container, false);
            mListView = view.FindViewById<ListView>(Resource.Id.listPertanyaan);
            mPertanyaanList = new List<PertanyaanModel>();

            CSService.WebService1 mService = new CSService.WebService1();
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

            DataSet ds = new DataSet();
            ds = mService.GetDataPoly();
            mPertanyaanList = getPertanyaanList(ds);

            mListView.Adapter = new AdapterPertanyaan(this, mPertanyaanList);
            
            return view;
        }

        private List<PertanyaanModel> getPertanyaanList(DataSet ds)
        {
            List<PertanyaanModel> tempPoli = new List<PertanyaanModel>();
            PertanyaanModel mPoli = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                mPoli = new PertanyaanModel();
                mPoli.setPertanyaanId(dr["Id_Pertanyaan"].ToString());
                mPoli.setPertanyaanNama(dr["Nama_Pertanyaan"].ToString());
                tempPoli.Add(mPoli);
            }
            return tempPoli;
        }
    }
}