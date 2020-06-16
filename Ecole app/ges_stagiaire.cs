using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ecole_app
{
    public partial class ges_stagiaire : UserControl
    {

        net d = new net();



        public ges_stagiaire()
        {
            InitializeComponent();
        }
        void chargerCombo()
        {
            string st = "select * from Groupe ";
            d.cmd = new SqlCommand(st, d.con);
            d.dr = d.cmd.ExecuteReader();
            d.dt.Load(d.dr);
            comboBox1.DataSource = d.dt;
            comboBox1.DisplayMember = "Nom_Groupe";
            comboBox1.ValueMember = "Code_groupe";
        }

        string ereurecheck()
        {
            string x = "nan";

            if (textBox1.Text == "")
            {
                return "entrer un code stagiaire ";
            }
            if (imgpecture == "")
            {
                return "inserer une image de type 'jpg'";
            }

            return x;

        }




        string imgpecture = "";




        private void ges_stagiaire_Load(object sender, EventArgs e)
        {
            d.connecter();
            chargerCombo();


        }

        private void button1_Click(object sender, EventArgs e)
        {

            //  Le chargement de l'image au control
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "JPG Files(*.jpg)|*.jpg|all files(*.*)|*.*";
            fd.ShowDialog();
            imgpecture = fd.FileName.ToString();
            pictureBox1.ImageLocation = imgpecture;


        }
        void imagetest(DataTable ddt)
            {
            byte[] img = (Byte[])(ddt.Rows[0][4]);
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);


            byte[] imge = null;
            FileStream fs = new FileStream(imgpecture, FileMode.Open, FileAccess.Read);
            BinaryReader rd = new BinaryReader(fs);
            imge = rd.ReadBytes((int)fs.Length);




        }
        private void button2_Click(object sender, EventArgs e)
        {
            string x = ereurecheck();
            int i = 0;
            if(x!="nan")
            {
                MessageBox.Show(x.ToString());
            }
            else
            {
            try
            {
            byte[] imge = null;
            FileStream fs = new FileStream(imgpecture,FileMode.Open, FileAccess.Read);
            BinaryReader rd = new BinaryReader(fs);
            imge = rd.ReadBytes((int)fs.Length);

            d.cmd.CommandText = "insert into Stagiaire values ("+ textBox1.Text+",'"+textBox2.Text+ "','"+textBox3.Text+ "','"+textBox4.Text+ "',@imgb ,'" + textBox5.Text+ "','"+textBox6.Text+ "','"+textBox7.Text+ "' ,'"+dateTimePicker1.Value.ToShortDateString()+ "',"+textBox8.Text+ ","+comboBox1.SelectedValue.ToString()+")";
            d.cmd.Parameters.AddWithValue("imgb", imge);
            d.cmd.Connection = d.con;
            d.cmd.ExecuteNonQuery();
                    MessageBox.Show("ajoutee");



                }
                catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            }






        }

        private void button3_Click(object sender, EventArgs e)
        {
            string x = ereurecheck();
            if (x != "nan" && x != "inserer une image de type 'jpg'")
            {
                MessageBox.Show(x.ToString());
            }
            else
            {
                try
                {
                    DataTable ddt = new DataTable();

                    d.cmd.CommandText = "select * from Stagiaire where code_stagiaire = " + textBox1.Text;
                    d.cmd.Connection = d.con;
                    d.dr = d.cmd.ExecuteReader();

                    ddt.Load(d.dr);
                    if (ddt.Rows.Count > 0)
                    {


                        textBox2.Text = ddt.Rows[0][1].ToString();
                        textBox3.Text = ddt.Rows[0][2].ToString();
                        textBox4.Text = ddt.Rows[0][3].ToString();

                        byte[] img = (Byte[])(ddt.Rows[0][4]);
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);

                        textBox5.Text = ddt.Rows[0][5].ToString();
                        textBox6.Text = ddt.Rows[0][6].ToString();
                        textBox7.Text = ddt.Rows[0][7].ToString();
                        dateTimePicker1.Value = DateTime.Parse(ddt.Rows[0][8].ToString());
                        textBox8.Text = ddt.Rows[0][9].ToString();
                        comboBox1.SelectedValue = ddt.Rows[0][10].ToString();
                         

                    }
                    else
                    {
                        MessageBox.Show("entrer un code qui existe deja -_-");

                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }    
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string srt = "";
            string x = ereurecheck();
            DialogResult dialogResult = MessageBox.Show("Vouler vous modifier l'image ", "Attension", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                srt="update Stagiaire set  nom_st = '" + textBox2.Text + "', prenom_st =  '" + textBox3.Text + "',  adress_st =  '" + textBox4.Text + "', Photo =  @imgb , email_st =  '" + textBox5.Text + "',  genre = '" + textBox6.Text + "', tel_st =  '" + textBox7.Text + "' , DateNaiss = '" + dateTimePicker1.Value.ToShortDateString() + "',diplome =  " + textBox8.Text + ", codegrp = " + comboBox1.SelectedValue.ToString() + " where code_stagiaire = " + textBox1.Text + " ";
                if(imgpecture=="")
                {
                    x = "inserer une image de type 'jpg'";
                }

            }
            else if (dialogResult == DialogResult.No)
            {
                srt = "update Stagiaire set  nom_st = '" + textBox2.Text + "', prenom_st =  '" + textBox3.Text + "',  adress_st =  '" + textBox4.Text + "',  email_st =  '" + textBox5.Text + "',  genre = '" + textBox6.Text + "', tel_st =  '" + textBox7.Text + "' , DateNaiss = '" + dateTimePicker1.Value.ToShortDateString() + "',diplome =  " + textBox8.Text + ", codegrp = " + comboBox1.SelectedValue.ToString() + " where code_stagiaire = " + textBox1.Text + " ";

            }

            
            if (x != "nan" )
            {
                MessageBox.Show(x.ToString());
            }
            else
            {
                try
                {
                    byte[] imge = null;
                    FileStream fs = new FileStream(imgpecture, FileMode.Open, FileAccess.Read);
                    BinaryReader rd = new BinaryReader(fs);
                    imge = rd.ReadBytes((int)fs.Length);

                    d.cmd.CommandText = srt ;
                    d.cmd.Parameters.AddWithValue("imgb", imge);
                    d.cmd.Connection = d.con;
                    d.cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string x = ereurecheck();
            if (x != "nan" && x != "inserer une image de type 'jpg'")
            {
                MessageBox.Show(x.ToString());
            }
            else
            {
                try
                {
                    string srt = "delete Stagiaire  where code_stagiaire = " + textBox1.Text + " ";
                    d.cmd.CommandText = srt;
                    d.cmd.Connection = d.con;
                    d.cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
    }
}
