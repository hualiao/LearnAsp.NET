using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DemoObject
{
    /// <summary>
    /// Ref:http://stackoverflow.com/questions/2923137/repeater-in-repeater
    /// </summary>
    public class RepeaterInRepeaterModel
    {

        #region Remark
        /*           
         * I prefer to do it at the control level instead of the ItemDataBound level so that 
         * if you ever have to remove controls or items within your templates 
         * you don't have to worry about looking for code in the parent controls that use it. 
         * It get's all localize witht he control itself. 
         * Plus you never have to do a FindControl.          
         * If you want to replace a control in the future you can just delete it 
         * and your code will still work since it is all self contained. 
         * Using the ItemDataBound would cause your code to still compile but crash or act unexpectedly at runtime 
         * because of it's reliance on child controls
        */
        #endregion

        public int ParentID { get; set; }

        public IEnumerable<Item> Children { get; set; }

        public class Item
        {
            public string Name { get; set; }

            public string No { get; set; }
        }

        /// <summary>
        /// Get Mock Data
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<RepeaterInRepeaterModel> GetData()
        {
            var range = Enumerable.Range(1, 3);
            foreach (var item in range)
            {
                var model = new RepeaterInRepeaterModel()
                {
                    ParentID = item,
                    Children = new RepeaterInRepeaterModel.Item[]
                    {
                        new RepeaterInRepeaterModel.Item(){Name="liao",No="233"},
                        new RepeaterInRepeaterModel.Item(){Name="liao",No="233"},
                        new RepeaterInRepeaterModel.Item(){Name="liao",No="233"}
                    }
                };
                yield return model;
            }
        }        

    }    

}