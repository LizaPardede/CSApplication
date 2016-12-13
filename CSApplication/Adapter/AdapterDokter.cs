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
    class AdapterDokter : BaseAdapter<DokterModel>
    {
        private List<DokterModel> mItems;
        private Activity mContext;

        public AdapterDokter(Activity context, List<DokterModel> items) {
            mItems = items;
            mContext = context;
        }

        public override DokterModel this[int position]
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
            if (row == null) {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_dokter_adapter, parent, false);
            }

            TextView txtDokter = row.FindViewById<TextView>(Resource.Id.textDokter);
            txtDokter.Text = mItems[position].getDokterName();

            return row;
        }
    }
}