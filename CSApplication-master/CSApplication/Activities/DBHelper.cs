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
using Android.Util;
using Android.Database.Sqlite;

namespace CSApplication.Activities
{
    class DBHelper
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public DBHelper() { }

        public bool createDatabase()
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db")))
                {
                    connection.CreateTable<UserResult>();
                    return true;
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;

            }
        }

        public bool InsertIntoTableResult(UserResult userResult)
        {
            try
            {
                //using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db")))
                //{
                //    connection.Insert(userResult);
                //    return true;
                //}

                var db = new SQLite.SQLiteAsyncConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db"));
                db.InsertAsync(userResult);
                return true;

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;

            }
        }

        public List<UserResult> selectTableResult()
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db")))
                {
                    connection.CreateTable<UserResult>();
                    return connection.Table<UserResult>().ToList();
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;

            }
        }

        public bool selectQueryTableResult(int id)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db")))
                {
                    connection.Query<UserResult>("SELECT * FROM UserResult Where Detail_Pertanyaan_ID=?", id);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;

            }
        }




    }
}