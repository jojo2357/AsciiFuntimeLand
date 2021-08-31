using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsciiFuntimeLand
{
    public static class Program
    {
        private static Form1 form;
        private static Point _pos;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
	        Debug.WriteLine("Welcome");
            //new RenderableObject();
            new RenderablePlane(new SquareFace('@', new Vector3(255, 255, -1), new Vector3(-255, 255, -1), new Vector3(-255, -255, -1)));
            /*new RenderableObject(new Vector3(1, 1, 1), new Vector3(1, 1, 1), '!', Color.White);
            new RenderableObject(new Vector3(1, 1, 1), new Vector3(2, -2, 2), '#', Color.Red);
            new RenderableObject(new Vector3(255, 255, 1), new Vector3(-128, -128, -2f), '@', Color.Cyan);*/
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            form = new Form1();
            Cursor.Position = form.Location + (Divide(form.Size, 2));
            new Task(DoIt).Start();
            while (form.IsOpen())
            {
                Thread.Sleep(10);
                _pos = Cursor.Position;
                Cursor.Position = form.Location + (Divide(form.Size, 2));
                form.HandleMouseMovement(new Point(_pos.X - (form.Location.X + (form.Size.Width / 2)), _pos.Y - (form.Location.Y + (form.Size.Height / 2))));
                //Console.WriteLine(new Point(pos.X - (form.Location.X + (form.Size.Width / 2)), pos.Y - (form.Location.Y + (form.Size.Height / 2))));
            }
        }

        private static void DoIt()
        {
            Application.Run(form);
        }

        public static float MathematicalMod(double value, double modulo)
        {
            return (float)(value - Math.Floor(value / modulo) * modulo);
        }

        public static Size Divide(Size a, int b)
        {
            return new Size(a.Width / 2, a.Height / 2);
        }
    }
}
