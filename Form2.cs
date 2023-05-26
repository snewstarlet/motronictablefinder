using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace motronictablefinder
{
    public partial class Form2 : Form
    {
        public byte[] fileBytes;
        public byte[] xaxis;
        public byte[] yaxis;
        public byte[] tablecontent;
        public int tableaddr;
        public int xaddr;
        public int yaddr;
        public byte xsize;
        public byte ysize;
        public string size;
        public string type;

        double factor = 1;
        double offset = 0;

        public const byte rpmdesc = 0x3B;
        public const byte loaddesc = 0x40;
        public const byte cltdesc = 0x38;
        public const byte iatdesc = 0x37;
        public const byte voltdesc = 0x36;

        public Form2(byte[] filebytes, int tableaddr, int xaddr, int yaddr, string size, string type)
        {
            InitializeComponent();
            this.fileBytes = filebytes;
            this.tableaddr = tableaddr;
            this.xaddr = xaddr;
            this.yaddr = yaddr;
            this.size = size;
            this.type = type;
            this.Text = type + " | " + size;
            string[] values = size.Split('x');
            byte.TryParse(values[0], out ysize);
            byte.TryParse(values[1], out xsize);


            listView2.View = View.Details;
            listView2.GridLines = true;
            listView2.FullRowSelect = false;
            listView2.AllowColumnReorder = false;

            listView3.View = View.Details;
            listView3.GridLines = true;
            listView3.FullRowSelect = false;
            listView3.AllowColumnReorder = false;

        }
        public string gettypestring(byte desc)
        {
            string ret = "?";

            switch (desc)
            {
                case rpmdesc:
                    ret = "RPM";
                    break;
                case loaddesc:
                    ret = "LOAD";
                    break;
                case cltdesc:
                    ret = "CLT";
                    break;
                case iatdesc:
                    ret = "IAT";
                    break;
                case voltdesc:
                    ret = "VOLT";
                    break;
            }

            return ret;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            byte xtype = fileBytes[xaddr - 2];
            byte ytype = fileBytes[yaddr - 2];
            int[] xaxis = new int[xsize];
            int[] yaxis = new int[ysize];
            int[] tablecontent = new int[xsize * ysize];

            listView3.Columns.Add(gettypestring(ytype), 50, HorizontalAlignment.Center);

            factortext.Text = factor.ToString();
            offsettext.Text = offset.ToString();

            label3.Text = "";
            string xstr = "X AXIS: ";
            string ystr = "Y AXIS: ";

            for (int y = 0; y < ysize; y++)
            {
                yaxis[y] = fileBytes[yaddr + y];
                listView3.Items.Add(yaxis[y].ToString());
                ystr += yaxis[y].ToString() + " ";
            }


            switch(ytype)
            {
                case rpmdesc:
                    listView3.Items[ysize - 1].Text = ((256 - Convert.ToInt32(listView3.Items[ysize - 1].Text)) * 40).ToString();
                    for (int y = ysize - 2; y >= 0; y--)
                    {
                        listView3.Items[y].Text = (((Convert.ToInt32(listView3.Items[y+1].Text) / 40) - Convert.ToInt32(listView3.Items[y].Text)) * 40).ToString();
                    }
                    break;
                case voltdesc:
                    listView3.Items[ysize - 1].Text = Math.Round((256 - Convert.ToDouble(listView3.Items[ysize - 1].Text)) * 0.0681,2).ToString();
                    for (int y = ysize - 2; y >= 0; y--)
                    {
                        listView3.Items[y].Text = Math.Round(((Convert.ToDouble(listView3.Items[y + 1].Text) / 0.0681) - Convert.ToDouble(listView3.Items[y].Text)) * 0.0681,2).ToString();
                    }
                    break;
                case loaddesc:
                    listView3.Items[ysize - 1].Text = Math.Round((256 - Convert.ToDouble(listView3.Items[ysize - 1].Text)) / 20, 2).ToString();
                    for (int y = ysize - 2; y >= 0; y--)
                    {
                        listView3.Items[y].Text = Math.Round(((Convert.ToDouble(listView3.Items[y + 1].Text) * 20) - Convert.ToDouble(listView3.Items[y].Text)) / 20, 2).ToString();
                    }
                    break;
                case cltdesc:
                    listView3.Items[ysize - 1].Text = Math.Round(((256 - Convert.ToDouble(listView3.Items[ysize - 1].Text)) * 0.75) - 48, 2).ToString();
                    for (int y = ysize - 2; y >= 0; y--)
                    {
                        listView3.Items[y].Text = Math.Round( (((Convert.ToDouble(listView3.Items[y + 1].Text) + 48) * 1.333) - Convert.ToDouble(listView3.Items[y].Text)) * 0.75 -48 , 2).ToString();
                    }
                    break;
                case iatdesc:
                    listView3.Items[ysize - 1].Text = Math.Round(((256 - Convert.ToDouble(listView3.Items[ysize - 1].Text)) * 0.75) - 48, 2).ToString();
                    for (int y = ysize - 2; y >= 0; y--)
                    {
                        listView3.Items[y].Text = Math.Round((((Convert.ToDouble(listView3.Items[y + 1].Text) + 48) * 1.333) - Convert.ToDouble(listView3.Items[y].Text)) * 0.75 - 48, 2).ToString();
                    }
                    break;

            }







            if (xsize != 1)
            {

                for (int x = 0; x < xsize; x++)
                {

                    xaxis[x] = fileBytes[xaddr + x];
                    xstr += xaxis[x].ToString() + " ";
                    listView2.Columns.Add(xaxis[x].ToString());
                }

                switch (xtype)
                {
                    case rpmdesc:
                        label3.Text = "RPM";
                        listView2.Columns[xsize - 1].Text = ((256 - Convert.ToInt32(listView2.Columns[xsize - 1].Text)) * 40).ToString();
                        for (int x = xsize - 2; x >= 0; x--)
                        {
                            listView2.Columns[x].Text = (((Convert.ToInt32(listView2.Columns[x + 1].Text) / 40) - Convert.ToInt32(listView2.Columns[x].Text)) * 40).ToString();
                        }
                        break;
                    case voltdesc:
                        label3.Text = "VOLT";
                        listView2.Columns[xsize - 1].Text = Math.Round((256 - Convert.ToDouble(listView2.Columns[xsize - 1].Text)) * 0.0681,2).ToString();
                        for (int x = xsize - 2; x >= 0; x--)
                        {
                            listView2.Columns[x].Text = Math.Round(((Convert.ToDouble(listView2.Columns[x + 1].Text) / 0.0681) - Convert.ToDouble(listView2.Columns[x].Text)) * 0.0681, 2).ToString();

                        }
                        break;
                    case loaddesc:
                        label3.Text = "LOAD (ms)";
                        listView2.Columns[xsize - 1].Text = Math.Round((256 - Convert.ToDouble(listView2.Columns[xsize - 1].Text)) / 20, 2).ToString();
                        for (int x = xsize - 2; x >= 0; x--)
                        {
                            listView2.Columns[x].Text = Math.Round(((Convert.ToDouble(listView2.Columns[x + 1].Text) * 20) - Convert.ToDouble(listView2.Columns[x].Text)) / 20, 2).ToString();

                        }
                        break;
                    case cltdesc:
                        label3.Text = "CLT TEMP";
                        listView2.Columns[xsize - 1].Text = Math.Round(((256 - Convert.ToDouble(listView2.Columns[xsize - 1].Text)) * 0.75) - 48, 2).ToString();
                        for (int x = xsize - 2; x >= 0; x--)
                        {
                            listView2.Columns[x].Text = Math.Round((((Convert.ToDouble(listView2.Columns[x + 1].Text) + 48) * 1.333) - Convert.ToDouble(listView2.Columns[x].Text)) * 0.75 - 48, 2).ToString();
                        }
                        break;
                    case iatdesc:
                        label3.Text = "IAT TEMP";
                        listView2.Columns[xsize - 1].Text = Math.Round(((256 - Convert.ToDouble(listView2.Columns[xsize - 1].Text)) * 0.75) - 48, 2).ToString();
                        for (int x = xsize - 2; x >= 0; x--)
                        {
                            listView2.Columns[x].Text = Math.Round((((Convert.ToDouble(listView2.Columns[x + 1].Text) + 48) * 1.333) - Convert.ToDouble(listView2.Columns[x].Text)) * 0.75 - 48, 2).ToString();
                        }
                        break;
                }



                for (int y = 0; y < ysize; y++)
                {
                    string[] arr = new string[xsize];
                    ListViewItem itm;
                    for (int x = 0; x < xsize; x++)
                    {
                        arr[x] = Math.Round(Convert.ToDouble(fileBytes[tableaddr + x + (y * xsize)]) * factor + offset, 2).ToString();

                    }
                    itm = new ListViewItem(arr);
                    listView2.Items.Add(itm);
                }


            }
            else
            {
                listView2.Columns.Add(gettypestring(xtype));
                for (int x = 0; x < ysize; x++)
                {
                    tablecontent[x] = (int)(fileBytes[tableaddr + x] * factor + offset);
                    xstr += tablecontent[x].ToString() + " ";
                    listView2.Items.Add(tablecontent[x].ToString());
                }

                switch (xtype)
                {
                    case rpmdesc:
                        listView2.Items[xsize - 1].Text = ((256 - Convert.ToInt32(listView2.Items[xsize - 1].Text)) * 40).ToString();
                        for (int x = xsize - 2; x >= 0; x--)
                        {
                            listView2.Items[x].Text = (((Convert.ToInt32(listView2.Items[x + 1].Text) / 40) - Convert.ToInt32(listView2.Items[x].Text)) * 40).ToString();
                        }
                        break;
                    case voltdesc:
                        listView2.Items[xsize - 1].Text = Math.Round((256 - Convert.ToDouble(listView2.Items[xsize - 1].Text)) * 0.0681, 2).ToString();
                        for (int x = xsize - 2; x >= 0; x--)
                        {
                            listView2.Items[x].Text = Math.Round(((Convert.ToDouble(listView2.Items[x + 1].Text) / 0.0681) - Convert.ToDouble(listView2.Items[x].Text)) * 0.0681, 2).ToString();
                        }
                        break;
                    case loaddesc:
                        listView2.Items[xsize - 1].Text = Math.Round((256 - Convert.ToDouble(listView2.Items[xsize - 1].Text)) / 20, 2).ToString();
                        for (int x = xsize - 2; x >= 0; x--)
                        {
                            listView2.Items[x].Text = Math.Round(((Convert.ToDouble(listView2.Items[x + 1].Text) * 20) - Convert.ToDouble(listView2.Items[x].Text)) / 20, 2).ToString();
                        }
                        break;
                    case cltdesc:
                        listView2.Items[ysize - 1].Text = Math.Round(((256 - Convert.ToDouble(listView2.Items[ysize - 1].Text)) * 0.75) - 48, 2).ToString();
                        for (int x = xsize - 2; x >= 0; x--)
                        {
                            listView2.Items[x].Text = Math.Round((((Convert.ToDouble(listView2.Items[x + 1].Text) + 48) * 1.333) - Convert.ToDouble(listView2.Items[x].Text)) * 0.75 - 48, 2).ToString();
                        }
                        break;
                    case iatdesc:
                        listView2.Items[ysize - 1].Text = Math.Round(((256 - Convert.ToDouble(listView2.Items[ysize - 1].Text)) * 0.75) - 48, 2).ToString();
                        for (int x = xsize - 2; x >= 0; x--)
                        {
                            listView2.Items[x].Text = Math.Round((((Convert.ToDouble(listView2.Items[x + 1].Text) + 48) * 1.333) - Convert.ToDouble(listView2.Items[x].Text)) * 0.75 - 48, 2).ToString();
                        }
                        break;
                }
            }









        }

        private void button1_Click(object sender, EventArgs e)
        {
            factor = Convert.ToDouble(factortext.Text);
            offset = Convert.ToDouble(offsettext.Text);
            listView2.Items.Clear();
            for (int y = 0; y < ysize; y++)
            {
                string[] arr = new string[xsize];
                ListViewItem itm;
                for (int x = 0; x < xsize; x++)
                {
                    arr[x] = Math.Round(Convert.ToDouble(fileBytes[tableaddr + x + (y * xsize)]) * factor + offset, 2).ToString();

                }
                itm = new ListViewItem(arr);
                listView2.Items.Add(itm);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            factor = 0.007813;
            offset = 0;
            listView2.Items.Clear();
            for (int y = 0; y < ysize; y++)
            {
                string[] arr = new string[xsize];
                ListViewItem itm;
                for (int x = 0; x < xsize; x++)
                {
                    arr[x] = Math.Round(Convert.ToDouble(fileBytes[tableaddr + x + (y * xsize)]) * factor + offset, 2).ToString();

                }
                itm = new ListViewItem(arr);
                listView2.Items.Add(itm);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            factor = 0.75;
            offset = -22.5;
            listView2.Items.Clear();
            for (int y = 0; y < ysize; y++)
            {
                string[] arr = new string[xsize];
                ListViewItem itm;
                for (int x = 0; x < xsize; x++)
                {
                    arr[x] = Math.Round(Convert.ToDouble(fileBytes[tableaddr + x + (y * xsize)]) * factor + offset, 2).ToString();

                }
                itm = new ListViewItem(arr);
                listView2.Items.Add(itm);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            factor = 0.75;
            offset = -48;
            listView2.Items.Clear();
            for (int y = 0; y < ysize; y++)
            {
                string[] arr = new string[xsize];
                ListViewItem itm;
                for (int x = 0; x < xsize; x++)
                {
                    arr[x] = Math.Round(Convert.ToDouble(fileBytes[tableaddr + x + (y * xsize)]) * factor + offset, 2).ToString();

                }
                itm = new ListViewItem(arr);
                listView2.Items.Add(itm);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            factor = 0.0681;
            offset = 0;
            listView2.Items.Clear();
            for (int y = 0; y < ysize; y++)
            {
                string[] arr = new string[xsize];
                ListViewItem itm;
                for (int x = 0; x < xsize; x++)
                {
                    arr[x] = Math.Round(Convert.ToDouble(fileBytes[tableaddr + x + (y * xsize)]) * factor + offset, 2).ToString();

                }
                itm = new ListViewItem(arr);
                listView2.Items.Add(itm);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            factor = 0.01;
            offset = 0;
            listView2.Items.Clear();
            for (int y = 0; y < ysize; y++)
            {
                string[] arr = new string[xsize];
                ListViewItem itm;
                for (int x = 0; x < xsize; x++)
                {
                    arr[x] = Math.Round(Convert.ToDouble(fileBytes[tableaddr + x + (y * xsize)]) * factor + offset, 2).ToString();

                }
                itm = new ListViewItem(arr);
                listView2.Items.Add(itm);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            factor = 40;
            offset = 0;
            listView2.Items.Clear();
            for (int y = 0; y < ysize; y++)
            {
                string[] arr = new string[xsize];
                ListViewItem itm;
                for (int x = 0; x < xsize; x++)
                {
                    arr[x] = Math.Round(Convert.ToDouble(fileBytes[tableaddr + x + (y * xsize)]) * factor + offset, 2).ToString();

                }
                itm = new ListViewItem(arr);
                listView2.Items.Add(itm);
            }
        }
    }
}
