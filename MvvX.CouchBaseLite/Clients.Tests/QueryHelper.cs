using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using MvvX.Plugins.CouchBaseLite.Managers;

namespace Clients.Tests
{
    public static class QueryHelper
    {
        public static IView CreateAgeView(IDatabase database, string name, string version)
        {
            IView view = database.GetView(name);
            view.SetMap(
                (doc, emit) =>
                {
                    object ageObj;

                    if (doc.TryGetValue("age", out ageObj))
                    {
                        try
                        {
                            int age = Convert.ToInt32(ageObj);
                            emit(age, doc["id"]);
                        }
                        catch (InvalidCastException ex)
                        {
                            // If invalid format, ignore document
                        }
                    }
                }
            , version);

            return view;
        }


        public static IView CreateCityView(IDatabase database, string name, string version, IJsonSerializer jsonSerializer)
        {
            IView view = database.GetView(name);
            view.SetMap(
                (doc, emit) =>
                {
                    object citiesObject;

                    if (doc.TryGetValue("city", out citiesObject))
                    {
                        try
                        {
                            IList<string> cities = jsonSerializer.ConvertToList<string>(citiesObject);
                            foreach (var city in cities)
                            {
                                emit(city, doc["id"]);
                            }
                        }
                        catch (InvalidCastException ex)
                        {
                            // If invalid format, ignore document
                        }
                    }
                }
            , version);

            return view;
        }


    }
}