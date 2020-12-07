using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastObjectListViewProva.Data
{
    public class SingleData
    {
        private ElementId _id;
        private string _instance;
        private string _category;
        private string _family;
        private string _type;
        public SingleData(ElementId id, string instance, string category, string family, string type)
        {
            _id = id;
            _instance = instance;
            _category = category;
            _family = family;
            _type = type;
        }
    }
}
