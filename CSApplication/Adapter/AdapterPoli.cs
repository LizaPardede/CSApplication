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
    class AdapterPoli : BaseAdapter<PoliModel>
    {
        private List<PoliModel> mItems;
        private Activity mContext;

        public AdapterPoli(Activity context, List<PoliModel> items) {
            mItems = items;
            mContext = context;
        }

        public override PoliModel this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_poli_adapter, parent, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.textPoli);
            txtName.Text = mItems[position].getPoliName();

            return row;
        }
    }
}