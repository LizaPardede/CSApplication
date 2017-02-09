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
                var db = new SQLite.SQLiteAsyncConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db"));
                if (db.InsertAsync(userResult) != null) {
                    db.UpdateAsync(userResult);
                }
                
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

        //public List<UserResult> selectQueryTableResult(string id) {
        //    try
        //    {
        //        using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db"))) {

        //            UserResult uRes = new UserResult();
        //            if (uRes == null)
        //            {
        //                Console.WriteLine("Kenyaaaaaaaaaaaaaaaaaaanggggg");
        //            }
        //            else {
        //                Console.WriteLine("Laaaaaaapppppppppppppppppaaaaaaaaaaarrr");
        //                //connection.Query<UserResult>("SELECT * FROM UserResult WHERE pertanyaan_id='AFR'");
                        
        //            }
        //            return connection.Table<UserResult>().ToList();
        //            }
        //    }
        //    catch (SQLiteException ex) {
        //        Log.Info("SQLiteEx", ex.Message);
        //        return null;
        //    }
        //}
        
        public bool deleteData(List<UserResult> userResult)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db")))
                {
                    foreach(UserResult user in userResult)
                    {
                        connection.Delete(user);
                    }
                    return true;
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;

            }
        }


        public bool RjInsertIntoTableResult(ResultRawatJalan resultRawatJalan)
        {
            try
            {

                var db = new SQLite.SQLiteAsyncConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db"));
                if (db.InsertAsync(resultRawatJalan) != null)
                {
                    db.UpdateAsync(resultRawatJalan);
                }

                return true;

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;

            }
        }

        public List<ResultRawatJalan> RjSelectTableResult()
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db")))
                {
                    connection.CreateTable<ResultRawatJalan>();
                    return connection.Table<ResultRawatJalan>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;

            }
        }

        public bool deleteDataRawatJalan(List<ResultRawatJalan> resultRawatJalan)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, "CUSTOMER_SATISFACTION.db")))
                {
                    foreach (ResultRawatJalan resultRj in resultRawatJalan)
                    {
                        connection.Delete(resultRj);
                    }
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