using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraduateWork
{
    class GenerateObjects
    {

        public void GenerateAttributes(ref Form1 form, string inPath, ref int countAttributes, int rows,
                                    Tuple<int,int> start, Tuple<int,int> shift  )
        {
            var sourceStream = new System.IO.StreamReader(System.IO.File.Open(inPath, System.IO.FileMode.Open));
            string s = "";
            StringBuilder sb = new StringBuilder();

            while (!s.Contains("attribute"))
                s = sourceStream.ReadLine();

            var count = 0;
            while (s.Contains("attribute") && !s.Contains("class"))
            {
                var arr = s.Split(' ');
                count = count % rows;
                int x = start.Item1 + (countAttributes / rows) * shift.Item1;
                int y = start.Item2 + shift.Item2 * count;
                form.Controls.Add(new CheckBox()
                {
                    Name = "Attribute" + countAttributes.ToString(),
                    Location = new Point(x, y),
                    Size = new Size(65, 20),
                    Text = arr[1].Substring(1, arr[1].Length - 2),
                    Checked = true

                });
                countAttributes++;
                count++;

                s = sourceStream.ReadLine();
            }

            sourceStream.Close();
        }

        public void DeleteLayers(ref Form1 form)
        {
            for (int i = 0; i < 10; i++)
            {
                var control = form.Controls["layer" + (i + 1).ToString()];
                if (form.Controls.Contains(control))
                {
                    form.Controls.Remove(control);
                    form.Controls.Remove(form.Controls["layerlabel" + (i + 1).ToString()]);
                }
            }
        }

        public void GenerateLayers(ref Form1 form, int x, int y, int between,  int shift)
        {
            DeleteLayers(ref form);
            var count = Int32.Parse(form.layers.SelectedItem.ToString());

            int k = 0;
            for (int i = 0; i < count; i += 2)
            {
                form.Controls.Add(new Label()
                {
                    Name = "layerlabel" + k.ToString(),
                    Text = (k+1).ToString() + ":",
                    Location = new Point(x, y + k * between),
                    Size = new Size(23, 13)
                });
                form.Controls.Add(new TextBox()
                {
                    Name = "layer" + k.ToString(),
                    Location = new Point(x + between, y + k * between),
                    Size = new Size(46, 20)
                });
                k++;
            }

            int j = 0;
            for (int i = 1; i < count; i += 2)
            {
                form.Controls.Add(new Label()
                {
                    Name = "layerlabel" + k.ToString(),
                    Text = (k+1).ToString() + ":",
                    Location = new Point(x + shift, y + j * between),
                    Size = new Size(23, 13)
                });
                form.Controls.Add(new TextBox()
                {
                    Name = "layer" + k.ToString(),
                    Location = new Point(x + between + shift, y + j * between),
                    Size = new Size(46, 20)
                });
                j++; k++;

            }
        }


    }
}
