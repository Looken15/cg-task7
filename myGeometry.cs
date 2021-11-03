using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace task7
{
    public class myGeometry
    {
        public class Point3D : IComparable<Point3D>
        {
            public float X;
            public float Y;
            public float Z;
            public int index;
            public Point3D(float x, float y, float z, int ind)
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

            public override string ToString()
            {
                return X.ToString() + " " + Y.ToString() + " " + Z.ToString() + " " + index.ToString();
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
            public Polygon(List<Point3D> l)
            {
                points = new List<Point3D>();
                foreach (Point3D p in l)
                {
                    points.Add(p);
                }
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
            public Mesh()
            {
                points = new List<Point3D>();
                connections = new SortedDictionary<int, List<int>>();
            }
            public Mesh(List<Point3D> l, SortedDictionary<int, List<int>> sd)
            {
                points = new List<Point3D>();
                connections = new SortedDictionary<int, List<int>>();
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
                    connections.Add(p.index, temp);
                }
            }
            public Mesh(Mesh oldM)
            {
                var l = oldM.points;
                var sd = oldM.connections;
                points = new List<Point3D>();
                connections = new SortedDictionary<int, List<int>>();
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
                    connections.Add(p.index, temp);
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
                    s.Append(pair.Key+";");
                    foreach (var i in pair.Value)
                        s.Append(i + ";");
                    s.Append(" ");
                }
                return s.ToString();
            }

            public void Save(string path)
            {
                File.WriteAllText(path, ToString());
            }
        }
    }
}
