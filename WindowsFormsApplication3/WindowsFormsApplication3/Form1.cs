﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        double temperature = 20;
        double pressure = 1013;
        bool polarity = false;
        double space = 1;
        double result, result1 = 0;
        double a, b, c, d, k = 0;
        int index = 0;
        //array z wartościami
        double[,] array = new double[,]
        {
            //polaryzacja ujemna
            {-7.0278, 36.679, 0.9537}, // dla 2 array 0
            {-3.1197, 34.267, 1.0852}, // dla 5
            {-2.5274, 33.751, 0.6446}, // 6,25
            {-1.5805, 32.356, 0.9248}, // 10
            {-1.2233, 31.466,1.4188}, // 12,5
            {-1.0359, 31.261,1.3121}, // 15
            {-0.6224, 30.424, 1.4651}, // 25
            {-0.3158, 29.175, 1.9239}, //50
            {-0.2677, 28.347, 3.6374 }, //75
            {-0.1616, 28.359, 1.3109}, //100
            {-0.109, 27.637, 4.892}, //150
            {-0.0782, 26.234 , 27.778}, //200

            // polaryzacja dodatnia
            {-7.6027, 37.814, 0.4703},
            {-2.4876,34.219,0.6911},
            {-2.1011,33.342,0.8251},
            {-1.2439,31.94,0.8181},
            {-0.9963,31.478,0.7935},
            {-0.8891,31.434,0.4858},
            {-0.5498,30.754,-0.4025},
            {-0.3005,29.831,-2.1153},
            {-0.2037,29.154,-1.8502},
            {-0.1576,28.819,-3.3674},
            {0.107,28.039,27.753},
            {0.0819, 27.753, -5.472},
        };

        double calculateResult(double a, double b, double c, double d)
        {
            result = (a * space * space) + (b * space) + c;
            return result;
        }

        double calculateDiameter(double space, double d)
        {
            result1 = space / d;
            return result1;
        }

        double calculateParameter(double pressure, double temperature)
        {
            double density = ((293*pressure*100)/(1013*100*(temperature+273)));
            double k = 0.9358*density + 0.0664;
            return k;
        }

        public Form1()
        {
            InitializeComponent();
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MaximizeBox = false; //zabezpieczenie przed rozciąganiem okna
            this.MinimizeBox = false; //zabezpieczenie przed rozciąganiem okna
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked)
            {
                bool isChecked = true;
                temperature = 20;
                pressure = 1013;
                textBox2.Text = pressure.ToString();
                textBox1.Text = temperature.ToString();
            }
            else
            {
                bool isChecked = false;
                textBox2.Text = pressure.ToString();
                textBox1.Text = temperature.ToString();
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            polarity = true;
            label5.Text = "Dodatnia";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            polarity = false;
            label5.Text = "Ujemna";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pressure++;
            textBox2.Text = pressure.ToString();
            checkBox1.Checked = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pressure--;
            textBox2.Text = pressure.ToString();
            checkBox1.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temperature--;
            textBox1.Text = temperature.ToString();
            checkBox1.Checked = false;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46)
            {
                e.Handled = true;
                MessageBox.Show("Kropka jest niedozwolonym separatorem dziesiętnym w polskiej wersji systemu. Należy użyć przecinka.","UWAGA!!!");
                return;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            double value = 0.1;
            temperature -= value;
            textBox1.Text = temperature.ToString();
            checkBox1.Checked = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            double value = 0.1;
            temperature += value;
            textBox1.Text = temperature.ToString();
            checkBox1.Checked = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            double value = 0.1;
            pressure += value;
            textBox2.Text = pressure.ToString();
            checkBox1.Checked = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            double value = 0.1;
            pressure -= value;
            textBox2.Text = pressure.ToString();
            checkBox1.Checked = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (polarity == false) {
                calculateResult(array[index, 0], array[index, 1], array[index, 2], d);
            } else
            {
                calculateResult(array[(index+12), 0], array[(index+12), 1], array[(index+12), 2], d);
            }
            
            double value = calculateDiameter(space, d);
            double k = calculateParameter(pressure, temperature);
            double newResult = result * k;
            double neuResult = Math.Round(newResult, 2);
            textBox4.Text = value.ToString();
            textBox5.Text = Math.Round(k,3).ToString();

            if(value > 0.05 && value < 0.75)
            {
                label14.ForeColor = System.Drawing.Color.Black;
                label14.Text = "Jednorodny rozkład pola";
            }
            else
            {
                label14.ForeColor = System.Drawing.Color.Red;
                label14.Text = "Niejednorodny rozkład pola";
            }

            label15.Text = "Napięcie przeskoku:" + neuResult.ToString() + " kV";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = comboBox1.SelectedIndex;
            d = Convert.ToDouble(comboBox1.Items[index]); 
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            temperature++;
            textBox1.Text = temperature.ToString();
            checkBox1.Checked = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            if (textBox3.Text == "")
            {
                MessageBox.Show("Wprowadź wartość do pola 'Odstęp kul'", "UWAGA!!!!");
            }
            else
            {
                bool valid = double.TryParse(textBox3.Text.ToString(), out space);
            }

            if (space < 0)
            {
                MessageBox.Show("Odległość nie może być ujemna !!!!", "UWAGA!!!!");
                button7.Visible = false;
            }
            if (space == 0)
            {
                button7.Visible = false;
            }
            if (space > 0)
            {
                button7.Visible = true;
            }

        }
    }
}
