using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DemoObject
{
    /// <summary>
    /// Ref:http://stackoverflow.com/questions/1778221/understanding-asp-net-eval-and-bind
    /// </summary>
    public class CustomDataSource
    {
        public class Model
        {
            public string Name { get; set; }
        }

        static int state = 0;

        public IEnumerable<Model> Select()
        {           
            return new[] 
            {
                new Model { Name = "some value" },
                new Model{Name="Bob"},
                new Model{Name="Alice"}
            };
        }

        public void Update(string Name)
        {
            // This method will be called if you used Bind for the TextBox
            // and you will be able to get the new name and update the
            // data source accordingly
        }

        public void Update()
        {            
            // This method will be called if you used Eval for the TextBox
            // and you will not be able to get the new name that the user
            // entered
        }
    }
}