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
    class ResultRawatJalan
    {
        [SQLite.PrimaryKey]
        public string poly_id { get; set; }
        public string doctor_id { get; set; }
        public string kategori_id { get; set; }
        public string pertanyaan_id { get; set; }
        public string detail_pertanyaan_id { get; set; }
        public DateTime starttime { get; set; }
        public string user { get; set; }
    }
}