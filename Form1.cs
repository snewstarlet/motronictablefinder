using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace motronictablefinder
{
    public partial class Form1 : Form
    {
        public string selectedBin;
        public byte[] fileBytes;
        public const byte rpmdesc = 0x3B;
        public const byte loaddesc = 0x40;
        public const byte cltdesc = 0x38;
        public const byte iatdesc = 0x37;
        public const byte voltdesc = 0x36;
        XmlWriter writer;



        public Form1()
        {
            InitializeComponent();

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "Table Address";
            dataGridView1.Columns[1].Name = "Type";
            dataGridView1.Columns[2].Name = "Size";
            dataGridView1.Columns[3].Name = "Y Address";
            dataGridView1.Columns[4].Name = "X Address";
            dataGridView1.Columns[5].Name = "View Table";
            dataGridView1.Columns[6].Name = "Add To Def";

            dataGridView1.Columns[0].Width = 75;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 75;
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].Width = 75;
            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[6].Width = 50;


            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            writer = XmlWriter.Create(@"def.xml", settings);
        }

        public void rpmtable(int addr)
        {

            byte ysize = fileBytes[addr + 1];
            if (ysize != 0x00)
            {
                int yaddr = addr + 2;
                byte xdesc = fileBytes[addr + 1 + ysize + 1];
                byte xsize = fileBytes[addr + 1 + ysize + 2];

                if (xsize != 0x00)
                {
                    int xaddr = addr + 2 + ysize + 2;

                    switch (xdesc)
                    {
                        case loaddesc:
                            int tableaddr = addr + ysize + xsize + 4;
                            string type = "RPM * LOAD";

                            if ((xsize == 6 && ysize == 12) || (xsize == 6 && ysize == 16))
                            {
                                type = "Low Load Table | RPM * LOAD";

                                dataGridView1.Rows.Add(tableaddr.ToString("X")
                                                       , type
                                                       , ysize.ToString() + "x" + xsize.ToString()
                                                       , yaddr.ToString("X")
                                                       , xaddr.ToString("X")
                                                       , "View"
                                                       , "Add"
                                                       );

                            }
                            else if ((xsize == 7 && ysize == 12) || (xsize == 9 && ysize == 16))
                            {
                                type = "High Load Table | RPM * LOAD";

                                dataGridView1.Rows.Add(tableaddr.ToString("X")
                                                       , type
                                                       , ysize.ToString() + "x" + xsize.ToString()
                                                       , yaddr.ToString("X")
                                                       , xaddr.ToString("X")
                                                       , "View"
                                                       , "Add"
                                                       );

                            }
                            else if (xsize == 3 && ysize == 8)
                            {
                                type = "Closed Throttle Ign Table | RPM * LOAD";

                                dataGridView1.Rows.Add(tableaddr.ToString("X")
                                                       , type
                                                       , ysize.ToString() + "x" + xsize.ToString()
                                                       , yaddr.ToString("X")
                                                       , xaddr.ToString("X")
                                                       , "View"
                                                       , "Add"
                                                       );
                            }
                            else
                            {
                                type = "Unknown Table | RPM * LOAD";

                                dataGridView1.Rows.Add(tableaddr.ToString("X")
                                                       , type
                                                       , ysize.ToString() + "x" + xsize.ToString()
                                                       , yaddr.ToString("X")
                                                       , xaddr.ToString("X")
                                                       , "View"
                                                       , "Add"
                                                       );
                            }
                            break;
                        /////////////////////////////////////////////////////////////////////////////////////////////////
                        case cltdesc:
                            tableaddr = addr + ysize + 2;
                            type = "RPM * CLT";
                            dataGridView1.Rows.Add(tableaddr.ToString("X")
                                                   , type
                                                   , ysize.ToString() + "x" + xsize.ToString()
                                                   , yaddr.ToString("X")
                                                   , xaddr.ToString("X")
                                                   , "View"
                                                   , "Add"
                                                   );
                            break;
                        //////////////////////////////////////////////////////////////////////////////////////////////////
                        case iatdesc:
                            tableaddr = addr + ysize + xsize + 4;
                            type = "RPM * IAT";

                            if (xsize == 5 && ysize == 8)
                            {
                                type = "RPM * IAT | Possible IAT Compensation Table";
                            }
                            dataGridView1.Rows.Add(tableaddr.ToString("X")
                                                   , type
                                                   , ysize.ToString() + "x" + xsize.ToString()
                                                   , yaddr.ToString("X")
                                                   , xaddr.ToString("X")
                                                   , "View"
                                                   , "Add"
                                                   );
                            break;
                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                        case voltdesc:
                            tableaddr = addr + ysize + xsize + 4;
                            type = "RPM * VOLT";

                            if (xsize == 7 && ysize == 12)
                            {
                                type = "Dwell Table | RPM * VOLT";
                            }
                            dataGridView1.Rows.Add(tableaddr.ToString("X")
                                                   , type
                                                   , ysize.ToString() + "x" + xsize.ToString()
                                                   , yaddr.ToString("X")
                                                   , xaddr.ToString("X")
                                                   , "View"
                                                   , "Add"
                                                   );
                            break;
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////
                        default:
                            break;
                    }

                }

                if (ysize == 16 || ysize == 4 || ysize == 6 || ysize == 8 || ysize == 7 || ysize == 12)
                {
                    int tableaddr = addr + ysize + 2;
                    string type = "RPM * ?";
                    dataGridView1.Rows.Add(tableaddr.ToString("X")
                                           , type
                                           , ysize.ToString() + "x1"
                                           , yaddr.ToString("X")
                                           , tableaddr.ToString("X")
                                           , "View"
                                           , "Add"
                                           );
                }

            }
        }

        public void clttable(int addr)
        {
            byte ysize = fileBytes[addr + 1];
            if (ysize != 0x00)
            {
                int yaddr = addr + 2;
                byte xdesc = fileBytes[addr + 1 + ysize + 1];
                byte xsize = fileBytes[addr + 1 + ysize + 2];


                if (ysize == 6)
                {
                    int tableaddr = addr + ysize + 2;
                    string type = "CLT * RPM | Possible Fuel Restore Table";

                    int[] tablecontent = new int[ysize];
                    double[] ycontent = new double[ysize + 1];
                    ycontent[6] = 144;
                    for (int y = 5; y >= 0; y--)
                    {
                        tablecontent[y] = fileBytes[tableaddr + y] * 40;
                        ycontent[y] = (fileBytes[yaddr + y] * 0.75);
                        ycontent[y] = ycontent[y + 1] - ycontent[y];
                    }

                    Array.Resize(ref ycontent, ycontent.Length - 1);
                    tablecontent.Reverse();
                    ycontent.Reverse();

                    string a = string.Join(" ", tablecontent.Select(x => x.ToString()));
                    string b = string.Join(" ", ycontent.Select(x => x.ToString()));

                    dataGridView1.Rows.Add(tableaddr.ToString("X")
                       , type
                       , ysize.ToString() + "x1"
                       , yaddr.ToString("X")
                       , tableaddr.ToString("X")
                       , "View"
                       , "Add"
                       );
                }
                else if (ysize == 16 || ysize == 4 || ysize == 8 || ysize == 7 || ysize == 12)
                {
                    int tableaddr = addr + ysize + 2;
                    string type = "CLT * ?";
                    dataGridView1.Rows.Add(tableaddr.ToString("X")
                       , type
                       , ysize.ToString() + "x1"
                       , yaddr.ToString("X")
                       , tableaddr.ToString("X")
                       , "View"
                       , "Add"
                       );
                }
            }
        }

        public void volttable(int addr)
        {
            byte ysize = fileBytes[addr + 1];
            if (ysize != 0x00)
            {
                int yaddr = addr + 2;
                byte xdesc = fileBytes[addr + 1 + ysize + 1];
                byte xsize = fileBytes[addr + 1 + ysize + 2];


                if (ysize == 16 || ysize == 4 || ysize == 6 || ysize == 8 || ysize == 7 || ysize == 12)
                {
                    int tableaddr = addr + ysize + 2;
                    string type = "VOLT * ? | Possible Injector Dead Time Table";
                    dataGridView1.Rows.Add(tableaddr.ToString("X")
                       , type
                       , ysize.ToString() + "x1"
                       , yaddr.ToString("X")
                       , tableaddr.ToString("X")
                       , "View"
                       , "Add"
                       );


                }
            }
        }

        private void selectbin_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "bin files (*.bin)|*.bin";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedBin = openFileDialog.FileName;
                fileBytes = File.ReadAllBytes(selectedBin);

                // desc size x x x x
                // rpm 4 50 60 70 80 load 4 80 60 40 50 
                // 3B 03 0A	14 CE  	40	04	14	28	28	74
               
            }
        }

        private void findtables_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < fileBytes.Length; i++)
            {
                switch (fileBytes[i])
                {
                    case rpmdesc:
                        rpmtable(i);
                        break;
                    case cltdesc:
                        clttable(i);
                        break;
                    case voltdesc:
                        volttable(i);
                        break;
                    default:
                        break;
                }
            }
        }

        private void settings_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                if (e.ColumnIndex == 5)
                {
                    int tableaddr = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), 16);
                    int xaddr =     Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(), 16);
                    int yaddr =     Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(), 16);
                    string size =    (string)dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string type =    (string)dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    //byte tableaddr, byte xaddr, byte yaddr, string size, string type
                    Form2 frm = new Form2(fileBytes,tableaddr,xaddr,yaddr,size,type);
                    frm.Show();
                }
            }
        }
    }
}