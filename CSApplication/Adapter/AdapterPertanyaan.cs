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
    class AdapterPertanyaan : BaseAdapter<PertanyaanModel>
    {
        private List<PertanyaanModel> mItems;
        private Activity mContext;

        public AdapterPertanyaan(Activity context, List<PertanyaanModel> items) {
            mItems = items;
            mContext = context;
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
            }

            TextView txtPertanyaan = row.FindViewById<TextView>(Resource.Id.textPertanyaan);
            txtPertanyaan.Text = mItems[position].getPertanyaanNama();

            return row;
        }
    }
}