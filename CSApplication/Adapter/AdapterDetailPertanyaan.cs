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

namespace CSApplication.Adapter
{
    
    class AdapterDetailPertanyaan : BaseAdapter<DetailPertanyaanModel>
    {
        private List<DetailPertanyaanModel> mItems;
        private Activity mContext;

        public AdapterDetailPertanyaan(Activity context, List<DetailPertanyaanModel> items) {
            mItems = items;
            mContext = context;
        }

        public override DetailPertanyaanModel this[int position]
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
                return mItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null) {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_detail_pertanyaan_adapter, parent, false);
            }
             
            TextView txtDetail = row.FindViewById<TextView>(Resource.Id.textDetailPertanyaan);
            txtDetail.Text = mItems[position].getDetailPertanyaan();

            return row; 
        }
    }
}