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
    class AdapterDepartemen : BaseAdapter<ModelDepartemen>
    {
        private List<ModelDepartemen> mItems;
        private Activity mContext;

        public AdapterDepartemen(Activity context, List<ModelDepartemen> items)
        {
            mItems = items;
            mContext = context;
        }

        public override ModelDepartemen this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_departemen_adapter, parent, false);
            }

            TextView txtDepartemen = row.FindViewById<TextView>(Resource.Id.nameDepartment);
            txtDepartemen.Text = mItems[position].getDepartemenName();

            return row;
        }
    }
}