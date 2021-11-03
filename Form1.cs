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
    enum Mode { Move = 1, Rotate, Scale, Draw }
    public partial class Form1 : Form
    {
        Bitmap pic;
        Mode md;
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
        bool need_axis;
        int edit_mode;
        Color lineColor = Color.Black;
        List<Point> drawnPoints = new List<Point>();
        int defaultCounter = 6;
        int counter = 6;

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
            transformAxis = new bool[3] { false, false, false };
            pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = pic;
            md = Mode.Move;
            moveButton.Checked = true;
            need_axis = true;
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
            need_axis = true;
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
        }

        Point ScreenPos(Point3D p)
        {
            return new Point((int)p.X + (int)zeroPoint.X, (int)p.Y + (int)zeroPoint.Y);
        }

        Point3D FromScreenPos(Point p)
        {
            return new Point3D((int)p.X - (int)zeroPoint.X, (int)p.Y - (int)zeroPoint.Y,0,0);
        }

        private void drawLocalAxis(Bitmap bm)
        {
            if (!need_axis)
                return;
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
            Color col = Color.DeepPink;
            Pen pen = new Pen(lineColor, 4);
            Pen pen_1 = new Pen(col, 4);

            foreach (Point3D p1 in mesh.points)
            {
                foreach (int p2index in mesh.connections[p1.index])
                {
                    Point screenp1 = ScreenPos(p1);
                    Point screenp2 = ScreenPos(mesh.points[p2index]);
                    g.DrawLine(pen, screenp1, screenp2);
                }
            }

            foreach (var pol in mesh.polygons)
            {
                var lst = pol.points.Select(p => new Point((int)p.X,(int)p.Y)).ToArray();
                //var lst = pol.points.Select(p => ScreenPos(p)).ToArray();
                g.DrawLines(pen, lst);
                g.DrawLine(pen, lst[0], lst[lst.Length - 1]);
            }

            if (edit_mode > 0)
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

        private void DrawPoint(Point p, Color c, int radius)
        {
            var g = Graphics.FromImage(pic);
            Rectangle r = new Rectangle(p.X - radius, p.Y - radius, radius * 2, radius * 2);
            SolidBrush br = new SolidBrush(c);
            g.FillEllipse(br, r);
        }

        private void DrawLines(List<Point> lst, Color c)
        {
            if (lst.Count < 2)
                return;
            var g = Graphics.FromImage(pic);
            g.DrawLines(new Pen(c), lst.ToArray());
        }

        private void AddPoint(Point p)
        {
            drawnPoints.Add(p);

            foreach (var x in drawnPoints)
                DrawPoint(x, lineColor, 2);
            DrawLines(drawnPoints, lineColor);
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (md == Mode.Draw)
            {
                AddPoint(e.Location);
                return;
            }
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
            if (!mDown || md == Mode.Draw) return;
            if (md == Mode.Move)
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
            else if (md == Mode.Rotate)
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
            else if (md == Mode.Scale)
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
        
        private void save_button_Click(object sender, EventArgs e)
        {
            var sd = new SaveFileDialog();
            sd.InitialDirectory = Environment.CurrentDirectory;
            sd.Filter = "obj files (*.obj)|*.obj";
            sd.FilterIndex = 2;
            sd.RestoreDirectory = true;
            if (sd.ShowDialog() == DialogResult.OK)
            {
                mesh.Save(sd.FileName);
            }
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            fd.Filter = "obj files (*.obj)|*.obj";
            fd.FilterIndex = 2;
            fd.RestoreDirectory = true;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                meshOrig.Load(fd.FileName);
                mesh = new Mesh(meshOrig);
                need_axis = false;
                DrawScene(pic);
            }
        }

        private void moveButton_CheckedChanged(object sender, EventArgs e)
        {
            md = Mode.Move;
        }

        private void rotateButton_CheckedChanged(object sender, EventArgs e)
        {
            md = Mode.Rotate;
        }

        private void ScaleButton_CheckedChanged(object sender, EventArgs e)
        {
            md = Mode.Scale;
        }

        private void drawButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!drawButton.Checked)
                return;
            md = Mode.Draw;
            need_axis = false;
            defineLocalAxis();
            ResetAthene();
            drawnPoints.Clear();
            mesh = new Mesh();
            DrawScene(pic); 
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            drawnPoints.Clear();
            need_axis = false;
            defineLocalAxis();
            ResetAthene();
            mesh = new Mesh();
            DrawScene(pic);
        }

        private void countBox_TextChanged(object sender, EventArgs e)
        {
            int count;
            if (!int.TryParse(countBox.Text,out count))
            {
                countBox.Text = defaultCounter.ToString();
                return;
            }
            counter = count;
        }

        private void mesh_button_Click(object sender, EventArgs e)
        {
            if (md != Mode.Draw || drawnPoints.Count < 2||
                !transformAxis[0] && !transformAxis[1] && !transformAxis[2])
                return;
            

            int count_points = 0;
            var points = drawnPoints;//.Select(p => FromScreenPos(p)).ToList();

            mesh = new Mesh();
            foreach (var p in points)
                mesh.points.Add(new Point3D(p.X, p.Y, 0.0f, count_points++));


            Mesh rotMesh = new Mesh(mesh);
            int split = (int)(360.0 / counter);
            double d_split = split * Math.PI / 180;
            


            double[,] RotateMatrixX = new double[4, 4];
            double[,] RotateMatrixY = new double[4, 4];
            double[,] RotateMatrixZ = new double[4, 4];


            if (transformAxis[0])
            {
                rotateAngleX = d_split;
                RotateMatrixX = AtheneRotate(rotateAngleX, 'x');
            }
            else
            {
                RotateMatrixX = AtheneRotate(0, 'x');
            }

            if (transformAxis[1])
            {
                rotateAngleY = d_split;
                RotateMatrixY = AtheneRotate(rotateAngleY, 'y');
            }
            else
            {
                RotateMatrixY = AtheneRotate(0, 'y');
            }

            if (transformAxis[2])
            {
                rotateAngleZ = d_split;
                RotateMatrixZ = AtheneRotate(rotateAngleZ, 'z');
            }
            else
            {
                RotateMatrixZ = AtheneRotate(0, 'z');
            }

            //firstMatrix = AtheneMove(-(int)zeroPoint.X, -(int)zeroPoint.Y, -(int)zeroPoint.Z);
            //lastMatrix = AtheneMove((int)zeroPoint.X, (int)zeroPoint.Y, (int)zeroPoint.Z);

            RotateMatrix = MatrixMult(MatrixMult(RotateMatrixX, RotateMatrixY), RotateMatrixZ);
            double[,] matr = MatrixMult(MatrixMult(firstMatrix, RotateMatrix), lastMatrix);

            for (int i = 1; i < (360 / split); i++)
            {
                AtheneTransform(ref rotMesh, matr);

                //firstMatrix = AtheneMove(0, 0, 0);
                //lastMatrix = AtheneMove(0, 0, 0);

                for (int j = 0; j < points.Count(); j++)
                {
                    rotMesh.points.Add(new Point3D(points[j].X, points[j].Y, 0, count_points));
                    count_points++;

                    if (j > 0)
                    {
                        List<Point3D> lp = new List<Point3D>();
                        lp.Add(rotMesh.points[(i - 1) * points.Count() + j - 1]);
                        lp.Add(rotMesh.points[(i - 1) * points.Count() + j]);
                        lp.Add(rotMesh.points[(i) * points.Count() + j]);
                        lp.Add(rotMesh.points[(i) * points.Count() + j - 1]);
                        rotMesh.polygons.Add(new Polygon(lp));
                    }
                }
            }

            for (int j = 0; j < points.Count(); j++)
            {
                if (j > 0)
                {
                    List<Point3D> lp = new List<Point3D>();
                    lp.Add(rotMesh.points[0 * points.Count() + j - 1]);
                    lp.Add(rotMesh.points[0 * points.Count() + j]);
                    lp.Add(rotMesh.points[((360 / split) - 1) * points.Count() + j]);
                    lp.Add(rotMesh.points[((360 / split) - 1) * points.Count() + j - 1]);
                    rotMesh.polygons.Add(new Polygon(lp));
                }
            }

            mesh = new Mesh(rotMesh);
            meshOrig = new Mesh(mesh);

            drawnPoints.Clear();
            DrawScene(pic);
        }
    }
}
