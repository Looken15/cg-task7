using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static task7.myGeometry;

namespace task7
{
    public class Polyhedrons
    {
        public static Mesh Hexahedron(int scale)
        {
            int counter = 0;
            scale /= 2;
            var p = new List<Point3D>
            {
            new Point3D(-scale, scale, -scale, counter++),
            new Point3D(scale, scale, -scale, counter++),
            new Point3D(scale, -scale, -scale, counter++),
            new Point3D(-scale, -scale, -scale, counter++),
            new Point3D(-scale, scale, scale, counter++),
            new Point3D(scale, scale, scale, counter++),
            new Point3D(scale, -scale, scale, counter++),
            new Point3D(-scale, -scale, scale, counter++)
            };
            var c = new SortedDictionary<int, List<int>>();
            c.Add(0, new List<int>() { 1, 3, 4 });
            c.Add(1, new List<int>() { 0, 2, 5 });
            c.Add(2, new List<int>() { 1, 3, 6 });
            c.Add(3, new List<int>() { 0, 2, 7 });
            c.Add(4, new List<int>() { 0, 5, 7 });
            c.Add(5, new List<int>() { 1, 4, 6 });
            c.Add(6, new List<int>() { 2, 5, 7 });
            c.Add(7, new List<int>() { 3, 4, 6 });

            return new Mesh(p, c);
        }

        public static Mesh Tetrahedron(int scale)
        {
            int counter = 0;
            scale /= 2;
            var p = new List<Point3D>
            {
                new Point3D(-scale, scale, -scale, counter++),
                new Point3D(scale, scale, scale, counter++),
                new Point3D(scale, -scale, -scale, counter++),
                new Point3D(-scale, -scale, scale, counter++)
            };
            var c = new SortedDictionary<int, List<int>>();
            c.Add(0, new List<int> { 1, 2, 3 });
            c.Add(1, new List<int> { 0, 2, 3 });
            c.Add(2, new List<int> { 0, 1, 3 });
            c.Add(3, new List<int> { 0, 1, 2 });
            return new Mesh(p, c);
        }

        public static Mesh Octahedron(int scale)
        {
            int counter = 0;
            scale /= 2;
            var p = new List<Point3D>
            {
                new Point3D(0, 0, -scale, counter++),
                new Point3D(-scale, 0, 0, counter++),
                new Point3D(0, scale, 0, counter++),
                new Point3D(scale, 0, 0, counter++),
                new Point3D(0, -scale, 0, counter++),
                new Point3D(0, 0, scale, counter++)
            };
            var c = new SortedDictionary<int, List<int>>();
            c.Add(0, new List<int> { 1, 2, 3, 4 });
            c.Add(1, new List<int> { 0, 2, 4, 5 });
            c.Add(2, new List<int> { 0, 1, 3, 5 });
            c.Add(3, new List<int> { 0, 2, 4, 5 });
            c.Add(4, new List<int> { 0, 1, 3, 5 });
            c.Add(5, new List<int> { 1, 2, 3, 4 });
            return new Mesh(p, c);
        }

        public static Mesh Icosahedron(int scale)
        {
            int counter = 0;
            scale = scale / 2;
            var p = new List<Point3D>();
            p.Add(new Point3D(0, 0, (float)Math.Sqrt(5) / 2 * scale, counter++));
            for (int i = 0; i < 5; i++)
            {
                p.Add(new Point3D(scale * (float)(Math.Cos(2 * i * 72 * Math.PI / 360)),
                                           scale * (float)(Math.Sin(2 * i * 72 * Math.PI / 360)),
                                           scale * (float)0.5, counter++));
            }
            for (int i = 0; i < 5; i++)
            {
                p.Add(new Point3D(scale * (float)(Math.Cos(2 * (36 + i * 72) * Math.PI / 360)),
                                           scale * (float)(Math.Sin(2 * (36 + i * 72) * Math.PI / 360)),
                                           scale * (float)0.5 * (-1), counter++));
            }
            p.Add(new Point3D(0, 0, -(float)Math.Sqrt(5) / 2 * scale, counter++));
            var c = new SortedDictionary<int, List<int>>();
            c.Add(0, new List<int> { 1, 2, 3, 4, 5 });
            c.Add(1, new List<int> { 0, 2, 5, 6, 10 });
            c.Add(2, new List<int> { 0, 1, 3, 6, 7 });
            c.Add(3, new List<int> { 0, 2, 4, 7, 8 });
            c.Add(4, new List<int> { 0, 3, 5, 8, 9 });
            c.Add(5, new List<int> { 0, 1, 4, 9, 10 });
            c.Add(6, new List<int> { 1, 2, 7, 10, 11 });
            c.Add(7, new List<int> { 2, 3, 6, 8, 11 });
            c.Add(8, new List<int> { 3, 4, 7, 9, 11 });
            c.Add(9, new List<int> { 4, 5, 8, 10, 11 });
            c.Add(10, new List<int> { 1, 5, 6, 9, 11 });
            c.Add(11, new List<int> { 6, 7, 8, 9, 10 });
            return new Mesh(p, c);
        }

        public static Mesh Dodecahedron(int scale)
        {
            Mesh Ico = Icosahedron(scale);
            List<Polygon> lp = listIcoPolys(Ico);
            int counter = 0;
            scale /= 2;
            var p = new List<Point3D>();
            foreach (Polygon pol in lp)
            {
                double x = (pol.points[0].X + pol.points[1].X + pol.points[2].X) / 3;
                double y = (pol.points[0].Y + pol.points[1].Y + pol.points[2].Y) / 3;
                double z = (pol.points[0].Z + pol.points[1].Z + pol.points[2].Z) / 3;
                p.Add(new Point3D(x, y, z, counter++));
            }
            var c = new SortedDictionary<int, List<int>>();
            c.Add(0, new List<int> { 1, 4, 15 });
            c.Add(1, new List<int> { 0, 2, 16 });
            c.Add(2, new List<int> { 1, 3, 17 });
            c.Add(3, new List<int> { 2, 4, 18 });
            c.Add(4, new List<int> { 0, 3, 19 });
            c.Add(5, new List<int> { 6, 9, 10 });
            c.Add(6, new List<int> { 5, 7, 11 });
            c.Add(7, new List<int> { 6, 8, 12 });
            c.Add(8, new List<int> { 7, 9, 13 });
            c.Add(9, new List<int> { 5, 8, 14 });
            c.Add(10, new List<int> { 5, 15, 19 });
            c.Add(11, new List<int> { 6, 15, 16 });
            c.Add(12, new List<int> { 7, 16, 17 });
            c.Add(13, new List<int> { 8, 17, 18 });
            c.Add(14, new List<int> { 9, 18, 19 });
            c.Add(15, new List<int> { 0, 10, 11 });
            c.Add(16, new List<int> { 1, 11, 12 });
            c.Add(17, new List<int> { 2, 12, 13 });
            c.Add(18, new List<int> { 3, 14, 14 });
            c.Add(19, new List<int> { 4, 10, 14 });
            return new Mesh(p, c);
        }

        private static List<Polygon> listIcoPolys(Mesh ico)
        {
            List<Polygon> lp = new List<Polygon>();
            List<Point3D> lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[0]));
            lst.Add(new Point3D(ico.points[1]));
            lst.Add(new Point3D(ico.points[2]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[0]));
            lst.Add(new Point3D(ico.points[2]));
            lst.Add(new Point3D(ico.points[3]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[0]));
            lst.Add(new Point3D(ico.points[3]));
            lst.Add(new Point3D(ico.points[4]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[0]));
            lst.Add(new Point3D(ico.points[4]));
            lst.Add(new Point3D(ico.points[5]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[0]));
            lst.Add(new Point3D(ico.points[5]));
            lst.Add(new Point3D(ico.points[1]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[10]));
            lst.Add(new Point3D(ico.points[11]));
            lst.Add(new Point3D(ico.points[6]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[11]));
            lst.Add(new Point3D(ico.points[7]));
            lst.Add(new Point3D(ico.points[6]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[11]));
            lst.Add(new Point3D(ico.points[8]));
            lst.Add(new Point3D(ico.points[7]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[11]));
            lst.Add(new Point3D(ico.points[9]));
            lst.Add(new Point3D(ico.points[8]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[11]));
            lst.Add(new Point3D(ico.points[10]));
            lst.Add(new Point3D(ico.points[9]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[6]));
            lst.Add(new Point3D(ico.points[1]));
            lst.Add(new Point3D(ico.points[10]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[2]));
            lst.Add(new Point3D(ico.points[6]));
            lst.Add(new Point3D(ico.points[7]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[3]));
            lst.Add(new Point3D(ico.points[7]));
            lst.Add(new Point3D(ico.points[8]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[4]));
            lst.Add(new Point3D(ico.points[8]));
            lst.Add(new Point3D(ico.points[9]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[5]));
            lst.Add(new Point3D(ico.points[9]));
            lst.Add(new Point3D(ico.points[10]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[6]));
            lst.Add(new Point3D(ico.points[2]));
            lst.Add(new Point3D(ico.points[1]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[7]));
            lst.Add(new Point3D(ico.points[3]));
            lst.Add(new Point3D(ico.points[2]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[8]));
            lst.Add(new Point3D(ico.points[4]));
            lst.Add(new Point3D(ico.points[3]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[9]));
            lst.Add(new Point3D(ico.points[5]));
            lst.Add(new Point3D(ico.points[4]));
            lp.Add(new Polygon(lst));

            lst = new List<Point3D>();
            lst.Add(new Point3D(ico.points[10]));
            lst.Add(new Point3D(ico.points[1]));
            lst.Add(new Point3D(ico.points[5]));
            lp.Add(new Polygon(lst));

            return lp;
        }
    }
}
