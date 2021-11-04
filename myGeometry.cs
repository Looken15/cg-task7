using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Drawing;

namespace task7
{
    public class myGeometry
    {
        public class Point3D : IComparable<Point3D>
        {
            public double X;
            public double Y;
            public double Z;
            public int index;
            //public Point3D(float x, float y, float z, int ind)
            //{
            //    X = x;
            //    Y = y;
            //    Z = z;
            //    index = ind;
            //}

            public Point3D(double x, double y, double z, int ind)
            {
                X = x;
                Y = y;
                Z = z;
                index = ind;
            }

            public Point3D()
            {
                X = 0;
                Y = 0;
                Z = 0;
                index = 0;
            }
            public Point3D(Point3D p)
            {
                X = p.X;
                Y = p.Y;
                Z = p.Z;
                index = p.index;
            }
            public int CompareTo(Point3D that)
            {
                if (X < that.X) return -1;
                if (Y < that.Y) return -1;
                if (X == that.X && Y == that.Y) return 0;
                return 1;
            }

            public Point3D(string s)
            {
                var values = s.Split(' ').ToArray();
                var provider = CultureInfo.InvariantCulture;
                X = double.Parse(values[0], provider);
                Y = double.Parse(values[1], provider);
                Z = double.Parse(values[2], provider);
                index = int.Parse(values[3]);
            }

            public override string ToString()
            {
                return X.ToString(CultureInfo.InvariantCulture) +
                    " " + Y.ToString(CultureInfo.InvariantCulture) +
                    " " + Z.ToString(CultureInfo.InvariantCulture) +
                    " " + index.ToString(CultureInfo.InvariantCulture);
            }
        }
        public class Edge
        {
            public Point3D p1;
            public Point3D p2;
            public Edge(Point3D pp1, Point3D pp2)
            {
                p1 = pp1;
                p2 = pp2;
            }
            public Edge()
            {
                p1 = new Point3D();
                p2 = new Point3D();
            }

            public Edge(string s)
            {
                var points = s.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                p1 = new Point3D(points[0]);
                p2 = new Point3D(points[1]);
            }

            public override string ToString()
            {
                return p1.ToString() + ";" + p2.ToString();
            }
        }
        public class Polygon
        {
            public List<Point3D> points;
            public Polygon()
            {
                points = new List<Point3D>();
            }
            public Polygon(Polygon oldP)
            {
                points = new List<Point3D>();
                foreach (var p in oldP.points)
                    points.Add(new Point3D(p));
            }
            public Polygon(List<Point3D> l)
            {
                points = new List<Point3D>();
                foreach (Point3D p in l)
                {
                    Point3D t = new Point3D(p);
                    points.Add(t);
                }
            }

            public Polygon(string s)
            {
                points = s.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).
                    Select(p => new Point3D(p)).ToList();
            }

            public override string ToString()
            {
                var s = new StringBuilder();
                foreach (var p in points)
                    s.Append(p.ToString() + ";");
                return s.ToString();
            }
        }



        public class Mesh
        {
            public List<Point3D> points;
            public SortedDictionary<int, List<int>> connections;
            public List<Edge> edges = new List<Edge>();
            public List<Polygon> polygons = new List<Polygon>();
            public Mesh()
            {
                points = new List<Point3D>();
                connections = new SortedDictionary<int, List<int>>();
            }
            public Mesh(List<Point3D> l, SortedDictionary<int, List<int>> sd, List<Edge> le = null, List<Polygon> lp = null)
            {
                points = new List<Point3D>();
                connections = new SortedDictionary<int, List<int>>();
                edges = new List<Edge>();
                polygons = new List<Polygon>();
                foreach (Point3D p in l)
                {
                    Point3D p3D = new Point3D(p);
                    points.Add(p3D);
                    List<int> temp = new List<int>();
                    if (sd.ContainsKey(p.index))
                        foreach (int pp in sd[p.index])
                        {
                            temp.Add(pp);
                        }
                    if (!connections.Keys.Contains(p.index)) connections.Add(p.index, temp);
                }
                if (le != null && le.Count() == 0)
                {
                    int countP = points.Count();
                    bool[,] flags = new bool[countP, countP];

                    foreach (Point3D p1 in points)
                    {
                        int p1ind = p1.index;
                        foreach (int p2ind in connections[p1ind])
                        {
                            if (!flags[p1ind, p2ind])
                            {
                                flags[p1ind, p2ind] = true;
                                flags[p2ind, p1ind] = true;
                                Point3D t1 = new Point3D(p1);
                                Point3D t2 = new Point3D(points[p2ind]);
                                edges.Add(new Edge(t1, t2));
                            }
                        }
                    }
                }
                else
                {
                    if (le != null)
                    {
                        foreach (Edge e in le)
                        {
                            Point3D t1 = new Point3D(e.p1);
                            Point3D t2 = new Point3D(e.p2);
                            edges.Add(new Edge(t1, t2));
                        }
                    }
                }
                if (lp != null && lp.Count != 0)
                {
                    foreach (Polygon p in lp)
                    {
                        polygons.Add(new Polygon(p.points));
                    }
                }

            }
            public Mesh(Mesh oldM)
            {
                var l = oldM.points;
                var sd = oldM.connections;
                var le = oldM.edges;
                var lp = oldM.polygons;
                points = new List<Point3D>();
                connections = new SortedDictionary<int, List<int>>();
                edges = new List<Edge>();
                polygons = new List<Polygon>();
                foreach (Point3D p in l)
                {
                    Point3D p3D = new Point3D(p);
                    points.Add(p3D);
                    List<int> temp = new List<int>();
                    if (sd.ContainsKey(p.index))
                        foreach (int pp in sd[p.index])
                        {
                            temp.Add(pp);
                        }
                    if (!connections.Keys.Contains(p.index)) connections.Add(p.index, temp);
                }
                if (le.Count() == 0)
                {
                    int countP = points.Count();
                    bool[,] flags = new bool[countP, countP];
                    foreach (Point3D p1 in points)
                    {
                        int p1ind = p1.index;
                        foreach (int p2ind in connections[p1ind])
                        {
                            if (!flags[p1ind, p2ind])
                            {
                                flags[p1ind, p2ind] = true;
                                flags[p2ind, p1ind] = true;
                                Point3D t1 = new Point3D(p1);
                                Point3D t2 = new Point3D(points[p2ind]);
                                edges.Add(new Edge(t1, t2));
                            }
                        }
                    }
                }
                else
                {
                    foreach (Edge e in le)
                    {
                        Point3D t1 = new Point3D(e.p1);
                        Point3D t2 = new Point3D(e.p2);
                        edges.Add(new Edge(t1, t2));
                    }
                }
                if (lp.Count != 0)
                {
                    foreach (Polygon p in lp)
                    {
                        polygons.Add(new Polygon(p.points));
                    }
                }
            }

            public override string ToString()
            {
                var s = new StringBuilder();
                foreach (var p in points)
                    s.Append(p.ToString() + ";");
                s.Append("|");
                foreach (var pair in connections)
                {
                    s.Append(pair.Key + ";");
                    foreach (var i in pair.Value)
                        s.Append(i + ";");
                    s.Append(" ");
                }
                s.Append("|");
                foreach (var pol in polygons)
                    s.Append(pol.ToString() + "#");
                return s.ToString();
            }

            public void Save(string path)
            {
                File.WriteAllText(path, ToString());
            }

            public void Load(string fileName)
            {
                var values = File.ReadAllText(fileName).Split('|');
                points = values[0].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).
                    Select(p => new Point3D(p)).ToList();
                connections = new SortedDictionary<int, List<int>>();
                foreach (var pair in values[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var lst = pair.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).
                        Select(s => int.Parse(s)).ToList();
                    var id = lst.First(); lst.RemoveAt(0);
                    connections[id] = lst;
                }
                if (values.Length >= 3)
                    polygons = values[2].Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).
                        Select(s => new Polygon(s)).ToList();
            }
        }
    }
}
