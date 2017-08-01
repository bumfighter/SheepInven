using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Sheep> Inventory = new List<Sheep>();
        class Sheep : Livestock
        {
            internal string Breed
            { get; set; }

            internal string Foodtype
            { get; set; }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(path + "\\Sheep Inventory");
            if (!Directory.Exists(path + "\\Sheep Inventory"))
                Directory.CreateDirectory(path + "\\Sheep Inventory");
            if (!File.Exists(path + "\\Sheep Inventory\\log.xml"))
            {
                XmlTextWriter Writer = new XmlTextWriter(path + "\\Sheep Inventory\\log.xml", Encoding.UTF8);
                Writer.WriteStartElement("Inventory");
                Writer.WriteEndElement();
                Writer.Close();
            }

            XmlDocument Document = new XmlDocument();
            Document.Load(path + "\\Sheep Inventory\\log.xml");
            foreach (XmlNode xNode in Document.SelectNodes("Inventory/Sheep"))
            {
                Sheep a = new Sheep();
                a.Name = xNode.SelectSingleNode("Name").InnerText;
                a.Breed = xNode.SelectSingleNode("Breed").InnerText;
                a.Age = Convert.ToDecimal(xNode.SelectSingleNode("Age").InnerText);
                a.Cost = Convert.ToDecimal(xNode.SelectSingleNode("Cost").InnerText);
                a.Weight = Convert.ToDecimal(xNode.SelectSingleNode("Weight").InnerText);
                a.Notes = xNode.SelectSingleNode("Notes").InnerText;

                Inventory.Add(a);
                listView1.Items.Add(a.Name);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sheep total = new Sheep();
            Sheep a = new Sheep();
            a.Name = textBox1.Text;
            a.Breed = textBox2.Text;
            a.Age = numericUpDown1.Value;
            a.Cost = numericUpDown2.Value;
            a.Weight = numericUpDown3.Value;
            a.Notes = textBox6.Text;
            Inventory.Add(a);
            listView1.Items.Add(a.Name);
            textBox1.Text = "";
            textBox2.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            textBox6.Text = "";

            //total.TotalCost += a.Cost;
            //total.TotalWeight += a.Weight;

            //numericUpDown4.Value = total.TotalWeight;
            //numericUpDown5.Value = total.TotalCost;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            textBox1.Text = Inventory[listView1.SelectedItems[0].Index].Name;
            textBox2.Text = Inventory[listView1.SelectedItems[0].Index].Breed;
            numericUpDown1.Value = Inventory[listView1.SelectedItems[0].Index].Age;
            numericUpDown2.Value = Inventory[listView1.SelectedItems[0].Index].Cost;
            numericUpDown3.Value = Inventory[listView1.SelectedItems[0].Index].Weight;
            textBox6.Text = Inventory[listView1.SelectedItems[0].Index].Notes;
        }


        void Remove()
        {
            try
            {
                Inventory.RemoveAt(listView1.SelectedItems[0].Index);
                listView1.Items.Remove(listView1.SelectedItems[0]);   
            }
            catch { }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inventory[listView1.SelectedItems[0].Index].Name = textBox1.Text;
            Inventory[listView1.SelectedItems[0].Index].Breed = textBox2.Text;
            Inventory[listView1.SelectedItems[0].Index].Age = numericUpDown1.Value;
            Inventory[listView1.SelectedItems[0].Index].Cost = numericUpDown2.Value;
            Inventory[listView1.SelectedItems[0].Index].Weight = numericUpDown3.Value;
            Inventory[listView1.SelectedItems[0].Index].Notes = textBox6.Text;
            listView1.SelectedItems[0].Text = textBox1.Text;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            XmlDocument Document = new XmlDocument();
            Document.Load(path + "\\Sheep Inventory\\log.xml");
            XmlNode Node = Document.SelectSingleNode("Inventory");
            Node.RemoveAll();

            foreach (Sheep a in Inventory)
            {
                XmlNode xStart = Document.CreateElement("Sheep");
                XmlNode xName = Document.CreateElement("Name");
                XmlNode xBreed = Document.CreateElement("Breed");
                XmlNode xAge = Document.CreateElement("Age");
                XmlNode xCost = Document.CreateElement("Cost");
                XmlNode xWeight = Document.CreateElement("Weight");
                XmlNode xNotes = Document.CreateElement("Notes");

                xName.InnerText = a.Name;
                xBreed.InnerText = a.Breed;
                xAge.InnerText = a.Age.ToString();
                xCost.InnerText = a.Cost.ToString();
                xWeight.InnerText = a.Weight.ToString();
                xNotes.InnerText = a.Notes;

                xStart.AppendChild(xName);
                xStart.AppendChild(xBreed);
                xStart.AppendChild(xAge);
                xStart.AppendChild(xCost);
                xStart.AppendChild(xWeight);
                xStart.AppendChild(xNotes);

                Document.DocumentElement.AppendChild(xStart);

            }
            Document.Save(path + "\\Sheep Inventory\\log.xml");
        }

    }
}
