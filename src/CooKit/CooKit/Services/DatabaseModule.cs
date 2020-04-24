using System;
using Autofac;
using SQLite;

namespace CooKit.Services
{
    public sealed class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => OpenSQLiteConnection()).SingleInstance();
        }

        // TODO: move this to a factory
        private static SQLiteAsyncConnection OpenSQLiteConnection()
        {
            var path = GetSQLitePath();
            return new SQLiteAsyncConnection(path);
        }

        // TODO: move this to the configuration class
        private static string GetSQLitePath()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return System.IO.Path.Combine(folder, "CooKit.db3");
        }
    }
}
