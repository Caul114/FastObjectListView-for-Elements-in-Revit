using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using FastObjectListViewProva.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FastObjectListViewProva
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        #region Private data members
        Element m_slab = null;              // Store the selected element
        private List<SingleData> _data;      // Stores the list of characteristics of the selected items
        #endregion

        #region class public property
        /// <summary>
        /// With the selected elements, export the list of all its characteristics
        /// </summary>
        public List<SingleData> Elements
        {
            get
            {
                return _data;
            }
        }
        #endregion

        #region class public method
        /// <summary>
        /// Default constructor of Command
        /// </summary>
        public Command()
        {
            // Construct the data members for the property
            _data = new List<SingleData>();
        }
        #endregion

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            SingleData singleData;

            try
            {
                Selection choices = uidoc.Selection;
                ElementSet collection = new ElementSet();
                foreach (ElementId elementId in choices.GetElementIds())
                {
                    collection.Insert(uidoc.Document.GetElement(elementId));
                }

                // If the item is not selected, it prompts you to select at least one
                foreach (Element e in collection)
                {
                    m_slab = e as Element;
                    if (null == m_slab)
                    {
                        message = "Please select an element.";
                        return Autodesk.Revit.UI.Result.Failed;
                    }
                }

#nullable enable
                // Get all elements id

                foreach (Element el in collection)
                {
                    ElementId eTypeId = el.GetTypeId();
                    ElementType? eType = doc.GetElement(eTypeId) as ElementType;

                    // With the element selected, judge if the Id of the element exists, 
                    // if it doesn't exist, it should be zero.                
                    if (el.Id == null)
                    {
                        el.Name = "null";
                        singleData = new SingleData(
                            el.Id, el.Name, el.Category.Name, eType?.FamilyName, eType?.Name
                            );
                        _data.Add(singleData);
                    }
                    else
                    {
                        singleData = new SingleData(
                            el.Id, el.Name, el.Category.Name, eType?.FamilyName, eType?.Name
                            );
                        _data.Add(singleData);
                    }
                    
                }
                #nullable disable

                // Display them in a form
                FastObjectListViewProvaWF displayForm = new FastObjectListViewProvaWF(this);
                displayForm.ShowDialog();

                return Result.Succeeded;

            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
