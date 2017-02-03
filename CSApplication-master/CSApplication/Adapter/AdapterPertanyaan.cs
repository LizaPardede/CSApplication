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
using CSApplication.Fragments;
using CSApplication.Model;
using System.Data;
using CSApplication.Activities;

namespace CSApplication.Adapter
{
    class AdapterPertanyaan : BaseAdapter<PertanyaanModel>
    {
        private List<PertanyaanModel> mItems;
        private List<DetailPertanyaanModel> mDetails;
        private Activity mContext;
        private PertanyaanFragment pertanyaanFragment;
        private List<PertanyaanModel> mPertanyaanList;
        private DBHelper dbHelper;


        public AdapterPertanyaan(Activity context, List<PertanyaanModel> items, DBHelper dbHelper)
        {
            this.mItems = items;
            this.mContext = context;
            this.dbHelper = dbHelper;

        }

        public AdapterPertanyaan(PertanyaanFragment pertanyaanFragment, List<PertanyaanModel> mPertanyaanList)
        {
            this.pertanyaanFragment = pertanyaanFragment;
            this.mPertanyaanList = mPertanyaanList;
        }

        public override PertanyaanModel this[int position]
        {
            get
            {
                return mItems[position];
            }
        }

        public override int Count
        {
            get
            {
                return mItems.Count();
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_pertanyaan_adapter, parent, false);
                TextView txtPertanyaan = row.FindViewById<TextView>(Resource.Id.textPertanyaan);

                if (txtPertanyaan == null)
                {
                    Console.WriteLine("null");
                }
                CSService.WebService1 mService = new CSService.WebService1();
                mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

                DataSet ds = mService.GetDetailPertanyaanByPertanyaan(mItems[position].getPertanyaanId());
                mDetails = getDetailPertanyaan(ds);

                ListView listDetail = row.FindViewById<ListView>(Resource.Id.lvDetail);
                listDetail.Adapter = new AdapterDetailPertanyaan(mContext, mDetails, dbHelper, mItems[position].getPertanyaanId());
                txtPertanyaan.Text = mItems[position].getPertanyaanNama();
                
            }
            return row;
        }


        private static List<DetailPertanyaanModel> getDetailPertanyaan(DataSet ds)
        {
            List<DetailPertanyaanModel> tempDetail = new List<DetailPertanyaanModel>();
            DetailPertanyaanModel mDetail = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
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