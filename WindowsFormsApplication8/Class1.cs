using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace WindowsFormsApplication8
{
    class MyLine 
                            
    {
        public Point start, end;
        public Color color;
        public float width;
        public MyLine(Point p1, Point p2, Color c, float w)
        {
            start = p1;
            end = p2;
            color = c;
            width = w;
            
            
            
        }
    }
}
