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
using Java.Lang;
using CSApplication.Model;
using Java.Util;
using CSApplication.Activities;

namespace CSApplication.Adapter
{
    class ExpandableDataAdapter : BaseExpandableListAdapter
    {

        readonly Activity Context;
        private List<DetailPertanyaanModel> mListDetailPertanyaan;
        private List<PertanyaanModel> mListPertanyaan;
        public ExpandableDataAdapter(Activity newContext, List<PertanyaanModel> listPertanyaan) : base()
        {
            Context = newContext;
            mListPertanyaan = listPertanyaan;
        }

        protected List<Data> DataList { get; set; }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {

            View header = convertView;

            if (header == null)
            {
                header = Context.LayoutInflater.Inflate(Resource.Layout.ListGroup, null);
            }
            header.FindViewById<TextView>(Resource.Id.DataHeader).Text = mListPertanyaan[groupPosition].getPertanyaanNama();
            ExpandableListView elv = (ExpandableListView)parent;
            elv.ExpandGroup(groupPosition);
            return header;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {

            View row = convertView;
            if (row == null)
            {
                row = Context.LayoutInflater.Inflate(Resource.Layout.DataListItem, null);
            }

            mListDetailPertanyaan = Data.listDetailPertanyaan(mListPertanyaan[groupPosition].getPertanyaanId());
            DetailPertanyaanModel detail = mListDetailPertanyaan[childPosition];
            row.FindViewById<TextView>(Resource.Id.DataId).Text = detail.getDetailPertanyaan();

            return row;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            List<DetailPertanyaanModel> mListDetailPertanyaan = Data.listDetailPertanyaan(mListPertanyaan[groupPosition].getPertanyaanId());
            return mListDetailPertanyaan.Count;
        }

        public override int GroupCount
        {
            get
            {

                return mListPertanyaan.Count;
            }
        }


        #region implemented abstract members of BaseExpandableListAdapter

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
            //mListDetailPertanyaan = Data.listDetailPertanyaan(mListPertanyaan[groupPosition].getPertanyaanId());
            //return mListDetailPertanyaan[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            mListDetailPertanyaan = Data.listDetailPertanyaan(mListPertanyaan[groupPosition].getPertanyaanId());

            //return Convert.ToInt64(mListDetailPertanyaan[childPosition].getIdPertanyaan());
            return groupPosition;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        public override bool HasStableIds
        {
            get
            {
                return true;
            }
        }

        #endregion
    }
}
