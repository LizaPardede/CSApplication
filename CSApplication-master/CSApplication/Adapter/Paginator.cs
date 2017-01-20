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
using CSApplication.Activities;

namespace CSApplication.Adapter
{
    class Paginator
    {
        public static int TOTAL_NUM_ITEMS;
        public const int ITEMS_PER_PAGE = 1;
        public static int ITEMS_REMAINING;
        public static int LAST_PAGE;

        List<PertanyaanModel> pertanyaanModels;

        public Paginator(List<PertanyaanModel> data)
        {
            pertanyaanModels = data;
            TOTAL_NUM_ITEMS = pertanyaanModels.Count();
            ITEMS_REMAINING = TOTAL_NUM_ITEMS % ITEMS_PER_PAGE;
            LAST_PAGE = TOTAL_NUM_ITEMS / ITEMS_PER_PAGE;
        }

        public Paginator()
        {
        }

        public List<PertanyaanModel> generatePage(int currentPage)
        {
            List<PertanyaanModel> pageData = new List<PertanyaanModel>();
            

            int startItem = currentPage * ITEMS_PER_PAGE;
            int numOfItem = ITEMS_PER_PAGE;
            
            
            if (currentPage == LAST_PAGE && ITEMS_REMAINING<1)
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
    }
}