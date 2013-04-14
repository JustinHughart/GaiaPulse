using System.Collections.Generic;

namespace GaiaPulsePreviewer.SC.DeferredDrawingSystem
{
    public class DDSList
    {
        protected List<DDSObject> DrawingList; //The list of drawn objects.

        public DDSList()
        {
            DrawingList = new List<DDSObject>();
        }

        public void AddToDrawList(DDSObject DrawingObject)
        {
            DrawingList.Add(DrawingObject);
        }

        public void ClearDrawList()
        {
            DrawingList.Clear();
        }

        public void SortList()
        {
            DrawingList.Sort();
            DrawingList.Reverse();
        }
    }
}