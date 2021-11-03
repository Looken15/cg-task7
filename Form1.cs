using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static task7.myGeometry;
using static task7.Athene;
using static task7.Polyhedrons;

namespace task7
{
    public partial class Form1 : Form
    {
        Bitmap pic;
        ActType at;
        bool mDown;
        Point curP;
        Mesh mesh;
        Mesh meshOrig;
        string curMesh;
        Point3D zeroPoint;
        bool form_loaded = false;
        Mesh localAxisX;
        Mesh localAxisXorig;
        Mesh localAxisY;
        Mesh localAxisYorig;
        Mesh localAxisZ;
        Mesh localAxisZorig;

        public static Mesh rotMesh;
        Mesh rotMeshOrig;

        double scaleFactorX;
        double scaleFactorY;
        double scaleFactorZ;
        double curscaleFactorX;
        double curscaleFactorY;
        double curscaleFactorZ;
        double rotateAngleX;
        double currotateAngleX;
        double rotateAngleY;
        double currotateAngleY;
        double rotateAngleZ;
        double currotateAngleZ;
        bool[] transformAxis;
        int translateX;
        int translateY;
        int translateZ;
        int curtranslateX;
        int curtranslateY;
        int curtranslateZ;
        double[,] MoveMatrix;
        double[,] RotateMatrix;
        double[,] ScaleMatrix;
        double[,] firstMatrix;
        double[,] lastMatrix;

        bool from_c;

        int edit_mode;

        double rotateAngleL;
        double currotateAngleL;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] shapes = { "Тетраэдр", "Гексаэдр", "Октаэдр", "Икосаэдр", "Додекаэдр" };
            foreach (string s in shapes)
            {
                comboBox1.Items.Add(s);
            }
            curMesh = "Тетраэдр";
            comboBox1.Text = comboBox1.Items[0].ToString();
            comboBox2.Text = comboBox2.Items[0].ToString();
            //comboBox2.Text = comboBox2.Items[0].ToString();
            transformAxis = new bool[3] { false, false, false };
            pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = pic;
            at = ActType.Move;
            zeroPoint = new Point3D(pictureBox1.Width / 2, pictureBox1.Height / 2, 0, 0);
            mDown = false;
            mesh = Tetrahedron(100);
            meshOrig = new Mesh(mesh);
            rotMesh = Rot_Edge(100);
            rotMeshOrig = new Mesh(rotMesh);
            defineLocalAxis();
            ResetAthene();
            DrawScene(pic);
            form_loaded = true;
            from_c = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!form_loaded) return;
            curMesh = comboBox1.Text;
            pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = pic;
            ResetAthene();
            defineLocalAxis();
            SetMesh();
            DrawScene(pic);
        }
        void SetMesh()
        {
            if (curMesh == "Тетраэдр")
            {
                mesh = Tetrahedron(100);
                meshOrig = new Mesh(mesh);
            }
            else if (curMesh == "Гексаэдр")
            {
                mesh = Hexahedron(100);
                meshOrig = new Mesh(mesh);
            }
            else if (curMesh == "Октаэдр")
            {
                mesh = Octahedron(100);
                meshOrig = new Mesh(mesh);
            }
            else if (curMesh == "Икосаэдр")
            {
                mesh = Icosahedron(100);
                meshOrig = new Mesh(mesh);
            }
            else if (curMesh == "Додекаэдр")
            {
                mesh = Dodecahedron(100);
                meshOrig = new Mesh(mesh);
            }
        }
        void defineLocalAxis()
        {
            localAxisX = new Mesh();
            localAxisX.points.Add(new Point3D(0, 0, 0, 0));
            localAxisX.points.Add(new Point3D(50, 0, 0, 1));
            List<int> l = new List<int>();
            l.Add(1);
            localAxisX.connections.Add(0, l);
            localAxisXorig = new Mesh(localAxisX);

            localAxisY = new Mesh();
            localAxisY.points.Add(new Point3D(0, 0, 0, 0));
            localAxisY.points.Add(new Point3D(0, 50, 0, 1));
            l = new List<int>();
            l.Add(1);
            localAxisY.connections.Add(0, l);
            localAxisYorig = new Mesh(localAxisY);

            localAxisZ = new Mesh();
            localAxisZ.points.Add(new Point3D(0, 0, 0, 0));
            localAxisZ.points.Add(new Point3D(0, 0, 50, 1));
            l = new List<int>();
            l.Add(1);
            localAxisZ.connections.Add(0, l);
            localAxisZorig = new Mesh(localAxisZ);
        }

        private void drawAxis(Bitmap b)
        {
            Graphics g = Graphics.FromImage(b);
            Pen pen = new Pen(Color.Black, 1);
            int h = pic.Height;
            int w = pic.Width - 1;
            int arrowL = 5;
            g.DrawLine(pen, 0, h / 2, w, h / 2);
            g.DrawLine(pen, w / 2, 0, w / 2, h);
            g.DrawLine(pen, w / 2, 0, w / 2 + arrowL, arrowL);
            g.DrawLine(pen, w / 2, 0, w / 2 - arrowL, arrowL);
            g.DrawLine(pen, w, h / 2, w - arrowL, h / 2 - arrowL);
            g.DrawLine(pen, w, h / 2, w - arrowL, h / 2 + arrowL);

            g.DrawLine(pen, ScreenPos(new Point3D(0, 0, -100, 0)), ScreenPos(new Point3D(0, 0, 100, 0)));
        }

        Mesh Rot_Edge(int scale)
        {
            Mesh ans = new Mesh();
            int counter = 0;
            scale = scale / 2;

            ans.points.Add(new Point3D(-scale, 0, 0, counter++));
            ans.points.Add(new Point3D(scale, 0, 0, counter++));

            List<int> l = new List<int>();
            l.Add(1);
            ans.connections.Add(0, l);

            l = new List<int>();
            l.Add(0);
            ans.connections.Add(1, l);

            return ans;
        }

        ////  x->   y ^    z .
        //Mesh Tetrahedron(int scale)
        //{
        //    Mesh ans = new Mesh();
        //    int counter = 0;
        //    scale = scale / 2;
        //    ans.points.Add(new Point3D(-scale, scale, -scale, counter++));
        //    ans.points.Add(new Point3D(scale, scale, scale, counter++));
        //    ans.points.Add(new Point3D(scale, -scale, -scale, counter++));
        //    ans.points.Add(new Point3D(-scale, -scale, scale, counter++));
        //    List<int> l1 = new List<int>();
        //    l1.Add(1);
        //    l1.Add(2);
        //    l1.Add(3);
        //    ans.connections.Add(0, l1);

        //    List<int> l2 = new List<int>();
        //    l2.Add(0);
        //    l2.Add(2);
        //    l2.Add(3);
        //    ans.connections.Add(1, l2);

        //    List<int> l3 = new List<int>();
        //    l3.Add(0);
        //    l3.Add(1);
        //    l3.Add(3);
        //    ans.connections.Add(2, l3);

        //    List<int> l4 = new List<int>();
        //    l4.Add(0);
        //    l4.Add(1);
        //    l4.Add(2);
        //    ans.connections.Add(3, l4);
        //    return ans;
        //}
        //Mesh Hexahedron(int scale)
        //{
        //    Mesh ans = new Mesh();
        //    int counter = 0;
        //    scale = scale / 2;
        //    ans.points.Add(new Point3D(-scale, scale, -scale, counter++));
        //    ans.points.Add(new Point3D(scale, scale, -scale, counter++));
        //    ans.points.Add(new Point3D(scale, -scale, -scale, counter++));
        //    ans.points.Add(new Point3D(-scale, -scale, -scale, counter++));
        //    ans.points.Add(new Point3D(-scale, scale, scale, counter++));
        //    ans.points.Add(new Point3D(scale, scale, scale, counter++));
        //    ans.points.Add(new Point3D(scale, -scale, scale, counter++));
        //    ans.points.Add(new Point3D(-scale, -scale, scale, counter++));

        //    List<int> l = new List<int>();
        //    l.Add(1);
        //    l.Add(3);
        //    l.Add(4);
        //    ans.connections.Add(0, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(2);
        //    l.Add(5);
        //    ans.connections.Add(1, l);

        //    l = new List<int>();
        //    l.Add(1);
        //    l.Add(3);
        //    l.Add(6);
        //    ans.connections.Add(2, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(2);
        //    l.Add(7);
        //    ans.connections.Add(3, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(5);
        //    l.Add(7);
        //    ans.connections.Add(4, l);

        //    l = new List<int>();
        //    l.Add(1);
        //    l.Add(4);
        //    l.Add(6);
        //    ans.connections.Add(5, l);

        //    l = new List<int>();
        //    l.Add(2);
        //    l.Add(5);
        //    l.Add(7);
        //    ans.connections.Add(6, l);

        //    l = new List<int>();
        //    l.Add(3);
        //    l.Add(4);
        //    l.Add(6);
        //    ans.connections.Add(7, l);

        //    return ans;
        //}
        //Mesh Octahedron(int scale)
        //{
        //    Mesh ans = new Mesh();
        //    int counter = 0;
        //    scale = scale / 2;
        //    ans.points.Add(new Point3D(0, 0, -scale, counter++));
        //    ans.points.Add(new Point3D(-scale, 0, 0, counter++));
        //    ans.points.Add(new Point3D(0, scale, 0, counter++));
        //    ans.points.Add(new Point3D(scale, 0, 0, counter++));
        //    ans.points.Add(new Point3D(0, -scale, 0, counter++));
        //    ans.points.Add(new Point3D(0, 0, scale, counter++));

        //    List<int> l = new List<int>();
        //    l.Add(1);
        //    l.Add(2);
        //    l.Add(3);
        //    l.Add(4);
        //    ans.connections.Add(0, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(2);
        //    l.Add(4);
        //    l.Add(5);
        //    ans.connections.Add(1, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(1);
        //    l.Add(3);
        //    l.Add(5);
        //    ans.connections.Add(2, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(2);
        //    l.Add(4);
        //    l.Add(5);
        //    ans.connections.Add(3, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(1);
        //    l.Add(3);
        //    l.Add(5);
        //    ans.connections.Add(4, l);

        //    l = new List<int>();
        //    l.Add(1);
        //    l.Add(2);
        //    l.Add(3);
        //    l.Add(4);
        //    ans.connections.Add(5, l);

        //    return ans;
        //}

        //Mesh Icosahedron(int scale)
        //{
        //    Mesh ans = new Mesh();
        //    int counter = 0;
        //    scale = scale / 2;
        //    ans.points.Add(new Point3D(0, 0, (float)Math.Sqrt(5) / 2 * scale, counter++));
        //    for (int i = 0; i < 5; i++)
        //        ans.points.Add(new Point3D(scale * (float)(Math.Cos(2 * i * 72 * Math.PI / 360)),
        //                                   scale * (float)(Math.Sin(2 * i * 72 * Math.PI / 360)),
        //                                   scale * (float)0.5, counter++));
        //    for (int i = 0; i < 5; i++)
        //        ans.points.Add(new Point3D(scale * (float)(Math.Cos(2 * (36 + i * 72) * Math.PI / 360)),
        //                                   scale * (float)(Math.Sin(2 * (36 + i * 72) * Math.PI / 360)),
        //                                   scale * (float)0.5 * (-1), counter++));
        //    ans.points.Add(new Point3D(0, 0, -(float)Math.Sqrt(5) / 2 * scale, counter++));

        //    List<int> l = new List<int>();
        //    l.Add(1);
        //    l.Add(2);
        //    l.Add(3);
        //    l.Add(4);
        //    l.Add(5);
        //    ans.connections.Add(0, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(5);
        //    l.Add(2);
        //    l.Add(10);
        //    l.Add(6);
        //    ans.connections.Add(1, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(1);
        //    l.Add(3);
        //    l.Add(6);
        //    l.Add(7);
        //    ans.connections.Add(2, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(2);
        //    l.Add(4);
        //    l.Add(7);
        //    l.Add(8);
        //    ans.connections.Add(3, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(3);
        //    l.Add(5);
        //    l.Add(8);
        //    l.Add(9);
        //    ans.connections.Add(4, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(4);
        //    l.Add(1);
        //    l.Add(9);
        //    l.Add(10);
        //    ans.connections.Add(5, l);

        //    l = new List<int>();
        //    l.Add(11);
        //    l.Add(1);
        //    l.Add(2);
        //    l.Add(10);
        //    l.Add(7);
        //    ans.connections.Add(6, l);

        //    l = new List<int>();
        //    l.Add(11);
        //    l.Add(2);
        //    l.Add(3);
        //    l.Add(6);
        //    l.Add(8);
        //    ans.connections.Add(7, l);

        //    l = new List<int>();
        //    l.Add(11);
        //    l.Add(3);
        //    l.Add(4);
        //    l.Add(7);
        //    l.Add(9);
        //    ans.connections.Add(8, l);

        //    l = new List<int>();
        //    l.Add(11);
        //    l.Add(4);
        //    l.Add(5);
        //    l.Add(8);
        //    l.Add(10);
        //    ans.connections.Add(9, l);

        //    l = new List<int>();
        //    l.Add(11);
        //    l.Add(5);
        //    l.Add(1);
        //    l.Add(9);
        //    l.Add(6);
        //    ans.connections.Add(10, l);

        //    l = new List<int>();
        //    l.Add(6);
        //    l.Add(7);
        //    l.Add(8);
        //    l.Add(9);
        //    l.Add(10);
        //    ans.connections.Add(11, l);

        //    return ans;
        //}
        //List<Polygon> listIcoPolys(Mesh ico)
        //{
        //    List<Polygon> lp = new List<Polygon>();
        //    List<Point3D> lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[0]));
        //    lst.Add(new Point3D(ico.points[1]));
        //    lst.Add(new Point3D(ico.points[2]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[0]));
        //    lst.Add(new Point3D(ico.points[2]));
        //    lst.Add(new Point3D(ico.points[3]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[0]));
        //    lst.Add(new Point3D(ico.points[3]));
        //    lst.Add(new Point3D(ico.points[4]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[0]));
        //    lst.Add(new Point3D(ico.points[4]));
        //    lst.Add(new Point3D(ico.points[5]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[0]));
        //    lst.Add(new Point3D(ico.points[5]));
        //    lst.Add(new Point3D(ico.points[1]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[10]));
        //    lst.Add(new Point3D(ico.points[11]));
        //    lst.Add(new Point3D(ico.points[6]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[11]));
        //    lst.Add(new Point3D(ico.points[7]));
        //    lst.Add(new Point3D(ico.points[6]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[11]));
        //    lst.Add(new Point3D(ico.points[8]));
        //    lst.Add(new Point3D(ico.points[7]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[11]));
        //    lst.Add(new Point3D(ico.points[9]));
        //    lst.Add(new Point3D(ico.points[8]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[11]));
        //    lst.Add(new Point3D(ico.points[10]));
        //    lst.Add(new Point3D(ico.points[9]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[6]));
        //    lst.Add(new Point3D(ico.points[1]));
        //    lst.Add(new Point3D(ico.points[10]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[2]));
        //    lst.Add(new Point3D(ico.points[6]));
        //    lst.Add(new Point3D(ico.points[7]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[3]));
        //    lst.Add(new Point3D(ico.points[7]));
        //    lst.Add(new Point3D(ico.points[8]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[4]));
        //    lst.Add(new Point3D(ico.points[8]));
        //    lst.Add(new Point3D(ico.points[9]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[5]));
        //    lst.Add(new Point3D(ico.points[9]));
        //    lst.Add(new Point3D(ico.points[10]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[6]));
        //    lst.Add(new Point3D(ico.points[2]));
        //    lst.Add(new Point3D(ico.points[1]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[7]));
        //    lst.Add(new Point3D(ico.points[3]));
        //    lst.Add(new Point3D(ico.points[2]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[8]));
        //    lst.Add(new Point3D(ico.points[4]));
        //    lst.Add(new Point3D(ico.points[3]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[9]));
        //    lst.Add(new Point3D(ico.points[5]));
        //    lst.Add(new Point3D(ico.points[4]));
        //    lp.Add(new Polygon(lst));

        //    lst = new List<Point3D>();
        //    lst.Add(new Point3D(ico.points[10]));
        //    lst.Add(new Point3D(ico.points[1]));
        //    lst.Add(new Point3D(ico.points[5]));
        //    lp.Add(new Polygon(lst));

        //    return lp;
        //}
        //Mesh Dodecahedron(int scale)
        //{
        //    Mesh Ico = Icosahedron(scale);
        //    List<Polygon> lp = listIcoPolys(Ico);

        //    Mesh ans = new Mesh();
        //    int counter = 0;
        //    scale = scale / 2;

        //    foreach (Polygon pol in lp)
        //    {
        //        float x = (pol.points[0].X + pol.points[1].X + pol.points[2].X) / 3;
        //        float y = (pol.points[0].Y + pol.points[1].Y + pol.points[2].Y) / 3;
        //        float z = (pol.points[0].Z + pol.points[1].Z + pol.points[2].Z) / 3;
        //        ans.points.Add(new Point3D(x, y, z, counter++));
        //    }

        //    List<int> l = new List<int>();
        //    l.Add(4);
        //    l.Add(1);
        //    l.Add(15);
        //    ans.connections.Add(0, l);

        //    l = new List<int>();
        //    l.Add(0);
        //    l.Add(2);
        //    l.Add(16);
        //    ans.connections.Add(1, l);

        //    l = new List<int>();
        //    l.Add(1);
        //    l.Add(3);
        //    l.Add(17);
        //    ans.connections.Add(2, l);

        //    l = new List<int>();
        //    l.Add(2);
        //    l.Add(4);
        //    l.Add(18);
        //    ans.connections.Add(3, l);

        //    l = new List<int>();
        //    l.Add(3);
        //    l.Add(0);
        //    l.Add(19);
        //    ans.connections.Add(4, l);

        //    l = new List<int>();
        //    l.Add(9);
        //    l.Add(6);
        //    l.Add(10);
        //    ans.connections.Add(5, l);

        //    l = new List<int>();
        //    l.Add(5);
        //    l.Add(7);
        //    l.Add(11);
        //    ans.connections.Add(6, l);

        //    l = new List<int>();
        //    l.Add(6);
        //    l.Add(8);
        //    l.Add(12);
        //    ans.connections.Add(7, l);

        //    l = new List<int>();
        //    l.Add(7);
        //    l.Add(9);
        //    l.Add(13);
        //    ans.connections.Add(8, l);

        //    l = new List<int>();
        //    l.Add(8);
        //    l.Add(5);
        //    l.Add(14);
        //    ans.connections.Add(9, l);

        //    l = new List<int>();
        //    l.Add(19);
        //    l.Add(15);
        //    l.Add(5);
        //    ans.connections.Add(10, l);

        //    l = new List<int>();
        //    l.Add(15);
        //    l.Add(16);
        //    l.Add(6);
        //    ans.connections.Add(11, l);

        //    l = new List<int>();
        //    l.Add(16);
        //    l.Add(17);
        //    l.Add(7);
        //    ans.connections.Add(12, l);

        //    l = new List<int>();
        //    l.Add(17);
        //    l.Add(18);
        //    l.Add(8);
        //    ans.connections.Add(13, l);

        //    l = new List<int>();
        //    l.Add(18);
        //    l.Add(19);
        //    l.Add(9);
        //    ans.connections.Add(14, l);

        //    l = new List<int>();
        //    l.Add(10);
        //    l.Add(11);
        //    l.Add(0);
        //    ans.connections.Add(15, l);

        //    l = new List<int>();
        //    l.Add(11);
        //    l.Add(12);
        //    l.Add(1);
        //    ans.connections.Add(16, l);

        //    l = new List<int>();
        //    l.Add(12);
        //    l.Add(13);
        //    l.Add(2);
        //    ans.connections.Add(17, l);

        //    l = new List<int>();
        //    l.Add(13);
        //    l.Add(14);
        //    l.Add(3);
        //    ans.connections.Add(18, l);

        //    l = new List<int>();
        //    l.Add(14);
        //    l.Add(10);
        //    l.Add(4);
        //    ans.connections.Add(19, l);

        //    return ans;
        //}
        private void ResetAthene()
        {
            scaleFactorX = 1;
            scaleFactorY = 1;
            scaleFactorZ = 1;
            curscaleFactorX = 1;
            curscaleFactorY = 1;
            curscaleFactorZ = 1;
            rotateAngleX = 0;
            currotateAngleX = 0;
            rotateAngleY = 0;
            currotateAngleY = 0;
            rotateAngleZ = 0;
            currotateAngleZ = 0;
            translateX = 0;
            translateY = 0;
            translateZ = 0;
            curtranslateX = 0;
            curtranslateY = 0;
            curtranslateZ = 0;
            MoveMatrix = AtheneMove(0, 0, 0);
            RotateMatrix = AtheneRotate(0, 'x');
            ScaleMatrix = AtheneScale(1, 1, 1);
            firstMatrix = AtheneMove((int)(-zeroPoint.X), (int)(-zeroPoint.Y), (int)(-zeroPoint.Z));
            lastMatrix = AtheneMove((int)zeroPoint.X, (int)zeroPoint.Y, (int)zeroPoint.Z);

            rotateAngleL = 0;
            currotateAngleL = 0;
        }

        

        Point ScreenPos(Point3D p)
        {
            return new Point((int)p.X + (int)zeroPoint.X, (int)p.Y + (int)zeroPoint.Y);
        }

        private void drawLocalAxis(Bitmap bm)
        {
            Graphics g = Graphics.FromImage(bm);
            Color col = Color.Red;
            Pen pen = new Pen(col, 2);
            Point screenp1 = ScreenPos(localAxisX.points[0]);
            Point screenp2 = ScreenPos(localAxisX.points[1]);
            g.DrawLine(pen, screenp1, screenp2);

            col = Color.Green;
            pen = new Pen(col, 2);
            screenp1 = ScreenPos(localAxisY.points[0]);
            screenp2 = ScreenPos(localAxisY.points[1]);
            g.DrawLine(pen, screenp1, screenp2);

            col = Color.Blue;
            pen = new Pen(col, 2);
            screenp1 = ScreenPos(localAxisZ.points[0]);
            screenp2 = ScreenPos(localAxisZ.points[1]);
            g.DrawLine(pen, screenp1, screenp2);
        }

        private void DrawScene(Bitmap bm)
        {
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.Transparent);
            drawAxis(bm);
            Color col = Color.Orange;
            Color col_1 = Color.DeepPink;
            Pen pen = new Pen(col, 4);
            Pen pen_1 = new Pen(col_1, 4);

            foreach (Point3D p1 in mesh.points)
            {
                foreach (int p2index in mesh.connections[p1.index])
                {
                    Point screenp1 = ScreenPos(p1);
                    Point screenp2 = ScreenPos(mesh.points[p2index]);
                    g.DrawLine(pen, screenp1, screenp2);
                }
            }

            if(edit_mode > 0)
            {
                foreach (Point3D p1 in rotMesh.points)
                {
                    foreach (int p2index in rotMesh.connections[p1.index])
                    {
                        Point screenp1 = ScreenPos(p1);
                        Point screenp2 = ScreenPos(rotMesh.points[p2index]);
                        g.DrawLine(pen_1, screenp1, screenp2);
                    }
                }
            }

            drawLocalAxis(bm);
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            curP = e.Location;
            mDown = true;

            if(edit_mode == 1)
            {
                firstMatrix = AtheneMove(-(int)rotMesh.points[0].X, -(int)rotMesh.points[0].Y, -(int)rotMesh.points[0].Z);
                lastMatrix = AtheneMove((int)rotMesh.points[0].X, (int)rotMesh.points[0].Y, (int)rotMesh.points[0].Z);
                return;
            }

            firstMatrix = AtheneMove(-translateX, -translateY, -translateZ);
            lastMatrix = AtheneMove(translateX, translateY, translateZ);

            if (from_c)
            {
                double xx = 0;
                double yy = 0;
                double zz = 0;

                foreach (var p in mesh.points)
                {
                    xx += p.X;
                    yy += p.Y;
                    zz += p.Z; //Ошибка
                }

                xx /= mesh.points.Count();
                yy /= mesh.points.Count();
                zz /= mesh.points.Count();

                firstMatrix = AtheneMove((int)-xx, (int)-yy, (int)-zz);
                lastMatrix = AtheneMove((int)xx, (int)yy, (int)zz);
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mDown) return;
            if (at == ActType.Move)
            {
                curtranslateX = 0;
                curtranslateY = 0;
                curtranslateY = 0;

                if (transformAxis[0])
                {
                    curtranslateX = e.Location.X - curP.X;
                }
                if (transformAxis[1])
                {
                    curtranslateY = e.Location.Y - curP.Y;
                }
                if (transformAxis[2])
                {
                    curtranslateZ = (int)Distance(e.Location, curP);
                }

                MoveMatrix = AtheneMove(curtranslateX + translateX, curtranslateY + translateY, curtranslateZ + translateZ);

                if (edit_mode == 2)
                {
                    rotMesh = new Mesh(rotMeshOrig);
                    AtheneTransform(ref rotMesh, MoveMatrix);
                    DrawScene(pic);
                    return;
                }

                double[,] d1 = MatrixMult(MatrixMult(MoveMatrix, ScaleMatrix), RotateMatrix);
                double[,] matr = MatrixMult(MatrixMult(firstMatrix, d1), lastMatrix);
                mesh = new Mesh(meshOrig);
                AtheneTransform(ref mesh, matr);

                d1 = MatrixMult(MoveMatrix, RotateMatrix);
                matr = MatrixMult(MatrixMult(firstMatrix, d1), lastMatrix);
                localAxisX = new Mesh(localAxisXorig);
                localAxisY = new Mesh(localAxisYorig);
                localAxisZ = new Mesh(localAxisZorig);
                AtheneTransform(ref localAxisX, matr);
                AtheneTransform(ref localAxisY, matr);
                AtheneTransform(ref localAxisZ, matr);

                DrawScene(pic);
            }
            else if (at == ActType.Rotate)
            {
                Point p1 = new Point(pictureBox1.Width / 2 - curP.X, pictureBox1.Height / 2 - curP.Y);
                Point p2 = new Point(pictureBox1.Width / 2 - e.X, pictureBox1.Height / 2 - e.Y);
                double[,] RotateMatrixX = new double[4, 4];
                double[,] RotateMatrixY = new double[4, 4];
                double[,] RotateMatrixZ = new double[4, 4];
                if (transformAxis[0])
                {
                    currotateAngleX = AngleBetween(p1, p2);
                    RotateMatrixX = AtheneRotate(rotateAngleX + currotateAngleX, 'x');
                }
                else
                {
                    RotateMatrixX = AtheneRotate(rotateAngleX, 'x');
                }
                if (transformAxis[1])
                {
                    currotateAngleY = AngleBetween(p1, p2);
                    RotateMatrixY = AtheneRotate(rotateAngleY + currotateAngleY, 'y');
                }
                else
                {
                    RotateMatrixY = AtheneRotate(rotateAngleY, 'y');
                }
                if (transformAxis[2])
                {
                    currotateAngleZ = AngleBetween(p1, p2);
                    RotateMatrixZ = AtheneRotate(rotateAngleZ + currotateAngleZ, 'z');
                }
                else
                {
                    RotateMatrixZ = AtheneRotate(rotateAngleZ, 'z');
                }

                if(edit_mode == 1)
                {
                    //double[,] matrr = MatrixMult(MatrixMult(firstMatrix, RotateMatrix), lastMatrix);
                    RotateMatrix = LineRotate(AngleBetween(p1, p2));
                }
                else
                {
                    RotateMatrix = MatrixMult(MatrixMult(RotateMatrixX, RotateMatrixY), RotateMatrixZ);
                }

                if (edit_mode == 2)
                {

                    rotMesh = new Mesh(rotMeshOrig);
                    AtheneTransform(ref rotMesh, RotateMatrix);
                    DrawScene(pic); 
                    return;
                }

                double[,] d1 = MatrixMult(MatrixMult(MoveMatrix, ScaleMatrix), RotateMatrix);
                double[,] matr = MatrixMult(MatrixMult(firstMatrix, RotateMatrix), lastMatrix);
                mesh = new Mesh(meshOrig);
                AtheneTransform(ref mesh, matr);

                d1 = MatrixMult(MoveMatrix, RotateMatrix);
                matr = MatrixMult(MatrixMult(firstMatrix, d1), lastMatrix);
                localAxisX = new Mesh(localAxisXorig);
                localAxisY = new Mesh(localAxisYorig);
                localAxisZ = new Mesh(localAxisZorig);
                AtheneTransform(ref localAxisX, matr);
                AtheneTransform(ref localAxisY, matr);
                AtheneTransform(ref localAxisZ, matr);

                DrawScene(pic);
            }
            else if (at == ActType.Scale)
            {
                if (curP != e.Location)
                {
                    Point curPP = curP;
                    Point graphAnchor = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
                    if (curPP.X == graphAnchor.X) curPP.X += 1;
                    if (curPP.Y == graphAnchor.Y) curPP.Y += 1;
                    curscaleFactorX = scaleFactorX;
                    curscaleFactorY = scaleFactorY;
                    curscaleFactorZ = scaleFactorZ;

                    if (transformAxis[0])
                    {
                        double ss1 = Distance(curPP, graphAnchor) / Distance(e.Location, graphAnchor);
                        curscaleFactorX = scaleFactorX * ss1;
                        if (Math.Abs(curscaleFactorX) > 1000) curscaleFactorX = 1000;
                    }
                    if (transformAxis[1])
                    {
                        double ss2 = Distance(curPP, graphAnchor) / Distance(e.Location, graphAnchor);
                        curscaleFactorY = scaleFactorY * ss2;
                        if (Math.Abs(curscaleFactorY) > 1000) curscaleFactorY = 1000;
                    }
                    if (transformAxis[2])
                    {
                        double ss3 = Distance(curPP, graphAnchor) / Distance(e.Location, graphAnchor);
                        curscaleFactorZ = scaleFactorZ * ss3;
                        if (Math.Abs(curscaleFactorZ) > 1000) curscaleFactorZ = 1000;
                    }

                    ScaleMatrix = AtheneScale(1 / curscaleFactorX, 1 / curscaleFactorY, 1 / curscaleFactorZ);
                    double[,] d1 = MatrixMult(MatrixMult(MoveMatrix, ScaleMatrix), RotateMatrix);
                    double[,] matr = MatrixMult(MatrixMult(firstMatrix, d1), lastMatrix);
                    mesh = new Mesh(meshOrig);
                    AtheneTransform(ref mesh, matr);

                    d1 = MatrixMult(MoveMatrix, RotateMatrix);
                    matr = MatrixMult(MatrixMult(firstMatrix, d1), lastMatrix);
                    localAxisX = new Mesh(localAxisXorig);
                    localAxisY = new Mesh(localAxisYorig);
                    localAxisZ = new Mesh(localAxisZorig);
                    AtheneTransform(ref localAxisX, matr);
                    AtheneTransform(ref localAxisY, matr);
                    AtheneTransform(ref localAxisZ, matr);

                    DrawScene(pic);
                }

            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mDown) return;
            mDown = false;
            meshOrig = new Mesh(mesh);
            rotMeshOrig = new Mesh(rotMesh);
            localAxisXorig = new Mesh(localAxisX);
            localAxisYorig = new Mesh(localAxisY);
            localAxisZorig = new Mesh(localAxisZ);
            ResetAthene();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            at = ActType.Move;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            at = ActType.Rotate;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            at = ActType.Scale;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            transformAxis[0] = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            transformAxis[1] = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            transformAxis[2] = checkBox3.Checked;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (transformAxis[0])
            {
                scaleFactorX = -scaleFactorX;
            }
            if (transformAxis[1])
            {
                scaleFactorY = -scaleFactorY;
            }
            if (transformAxis[2])
            {
                scaleFactorZ = -scaleFactorZ;
            }

            ScaleMatrix = AtheneScale(scaleFactorX, scaleFactorY, scaleFactorZ);
            mesh = new Mesh(meshOrig);
            AtheneTransform(ref mesh, ScaleMatrix);

            rotMeshOrig = new Mesh(rotMesh);

            localAxisX = new Mesh(localAxisXorig);
            localAxisY = new Mesh(localAxisYorig);
            localAxisZ = new Mesh(localAxisZorig);
            AtheneTransform(ref localAxisX, ScaleMatrix);
            AtheneTransform(ref localAxisY, ScaleMatrix);
            AtheneTransform(ref localAxisZ, ScaleMatrix);

            meshOrig = new Mesh(mesh);
            localAxisXorig = new Mesh(localAxisX);
            localAxisYorig = new Mesh(localAxisY);
            localAxisZorig = new Mesh(localAxisZ);
            ResetAthene();

            DrawScene(pic);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            from_c = checkBox5.Checked;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Default mode")
            {
                edit_mode = 0;
            }
            else if (comboBox2.Text == "Rotation line")
            {
                edit_mode = 1;
            }
            else
            {
                edit_mode = 2;
            }

            if(form_loaded)
            {
                DrawScene(pic);
            }

        }
        enum ActType { Move = 1, Rotate, Scale }

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "obj files (*.obj)|*.obj";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
