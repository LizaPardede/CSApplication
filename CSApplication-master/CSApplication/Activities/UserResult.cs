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

namespace CSApplication.Activities
{
    class UserResult
    {
        //public const String KATEGORI_ID = "Kategori_ID";
        //public const String PERTANYAAN_ID = "Pertanyaan_ID";
        //public const String DETAIL_PERTANYAAN_ID = "Detail_Pertanyaan_ID";
        //public const String STARTTIME = "StartTime";
        //public const String ENDTIME = "EndTime";
        //public const String USER = "user";

        public string kategori_id { get; set; }
        public string pertanyaan_id { get; set; }
        public string detail_pertanyaan_id { get; set; }
        public DateTime starttime { get; set; }

        public string user { get; set; }


    }
}