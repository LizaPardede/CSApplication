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
using Java.Lang;

namespace CSApplication.Adapter
{
    class ListPagerAdapter : BaseExpandableListAdapter
    {
        public static int TOTAL_NUM_ITEMS;
        public const int ITEMS_PER_PAGE = 1;
        public static int ITEMS_REMAINING;
        public static int LAST_PAGE;

        List<PertanyaanModel> pertanyaanModels;

        public override int GroupCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool HasStableIds
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ListPagerAdapter(List<PertanyaanModel> data)
        {
            pertanyaanModels = data;
            TOTAL_NUM_ITEMS = pertanyaanModels.Count();
            ITEMS_REMAINING = TOTAL_NUM_ITEMS / ITEMS_PER_PAGE;
            LAST_PAGE = TOTAL_NUM_ITEMS % ITEMS_PER_PAGE;
        }

        public ListPagerAdapter()
        {
        }

        public List<PertanyaanModel> generatePage(int currentPage)
        {
            List<PertanyaanModel> pageData = new List<PertanyaanModel>();


            int startItem = currentPage * ITEMS_PER_PAGE + 1;
            int numOfItem = ITEMS_PER_PAGE;
            int sum = startItem + ITEMS_REMAINING;

            if (currentPage == LAST_PAGE && ITEMS_REMAINING == 0)
            {
                for (int i = startItem; i < startItem + ITEMS_REMAINING; i++)
                {
                    pageData.Add(pertanyaanModels[i]);
                }
            }
            else
            {
                for (int i = startItem; i < startItem + numOfItem; i++)
                {
                    pageData.Add(pertanyaanModels[i]);
                }
            }
            return pageData;
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
        }

        public override int GetChildrenCount(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetGroupId(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
        }
    }
}