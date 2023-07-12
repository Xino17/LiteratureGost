using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika
{
    public partial class Form1 : Form
    {
        private int lastTextBoxRight = 200;
        private List<string> textBoxContents = new List<string>();
        private int a, b;

        public Form1()
        {
            InitializeComponent();
            CreateButton();

            for (int i = 0; i < 5; i++)
            {
                CreateTextBox(220, 60 + i * 75);
            }

            CreateStaticTextBox(5, 60, "Автор");
            CreateStaticTextBox(5, 135, "Название");
            CreateStaticTextBox(5, 210, "Город:Издание");
            CreateStaticTextBox(5, 285, "Год выпуска");
            CreateStaticTextBox(5, 360, "Число страниц");
            CreateStaticTextBox(5, 0, "Кол-во авторов");
            CreateStaticTextBox(435, 0, "Кол-во произведений");

            CreateInputTextBox(300, 0);
            CreateInputTextBox(800, 0);

        }
        
        private void CreateTextBox(int x, int y)
            
        {
            TextBox textBox = new TextBox
            {
                Location = new Point(x, y),
                Font = new Font(Font.FontFamily, 20),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = BackColor,
                Tag = false,
                ReadOnly = false 
            };
            
            textBox.KeyDown += TextBox_KeyDown;
            textBox.KeyPress += TextBox_KeyPress;

            Controls.Add(textBox);

            lastTextBoxRight = textBox.Right;
            
            
        }

        private void EnableOtherTextBoxes()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox textBox && textBox.Tag != null && !(bool)textBox.Tag && textBox.Left != 300 && textBox.Left != 800)
                {
                    textBox.ReadOnly = false; 
                }
            }
        }

        private void CreateStaticTextBox(int x, int y, string text = "")
        {
            TextBox textBox = new TextBox
            {
                Location = new Point(x, y),
                Font = new Font(Font.FontFamily, 20),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = BackColor,
                Text = text,
                ReadOnly = true,
                Width = TextRenderer.MeasureText(text, new Font(Font.FontFamily, 20)).Width
            };

            Controls.Add(textBox);
        }

        private void CreateInputTextBox(int x, int y)
        {
            TextBox textBox = new TextBox
            {
                Location = new Point(x, y),
                Font = new Font(Font.FontFamily, 20),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = BackColor,
                Tag = false
            };

            textBox.KeyDown += InputTextBox_KeyDown;
            textBox.KeyPress += InputTextBox_KeyPress;

            Controls.Add(textBox);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox currentTextBox = sender as TextBox;

                if (!string.IsNullOrEmpty(currentTextBox.Text) && !(bool)currentTextBox.Tag)
                {
                    e.SuppressKeyPress = true;

                    textBoxContents.Add(currentTextBox.Text);

                    if (a - 1 > 0)
                    {
                        CreateTextBox(currentTextBox.Right + 20, currentTextBox.Top);
                        currentTextBox.Tag = true;
                        a--;
                        
                    }

                    
                }
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;

            if (e.KeyChar == ' ' && string.IsNullOrEmpty(currentTextBox.Text))
            {
                e.Handled = true; 
            }
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox currentTextBox = sender as TextBox;

                if (!string.IsNullOrEmpty(currentTextBox.Text) && !(bool)currentTextBox.Tag)
                {
                    e.SuppressKeyPress = true;

                    if (currentTextBox.Left == 300)
                    {
                        if (int.TryParse(currentTextBox.Text, out int result) && result >= 1 && result <= 6)
                        {
                            a = result;
                            currentTextBox.ReadOnly = true;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a number between 1 and 6.");
                        }
                    }
                    else if (currentTextBox.Left == 800)
                    {
                        if (int.TryParse(currentTextBox.Text, out int result) && result >= 1 && result <= 1)
                        {
                            b = result;
                            currentTextBox.ReadOnly = true;
                        }
                        else
                        {
                            MessageBox.Show("Please enter number 1");
                        }
                    }

                    if (a != 0 && b != 0)
                    {
                        MessageBox.Show("Кол-во введенных авторов и произведений, должно быть таким же, каким вы ввели в численном эквиваленте. Заполнять нужно по порядку буз ошибок. После заполнения полей, нажмите на кнопку, чтобы получить преобразованную литературу по ГОСТУ");
                        EnableOtherTextBoxes();
                    }
                }
            }
        }

        private void InputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }
        private void CreateButton()
        {
            Button button = new Button();
            button.Text = "Преобразовать";
            button.Font = new System.Drawing.Font("Arial", 12); 
            button.AutoSize = true; 
            button.Location = new System.Drawing.Point(5, 435);
            button.Click += new EventHandler(Button_Click);
            this.Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
           
        {
                string message = "";
            if (a == 0)
            {
                message += textBoxContents[0] + " " + textBoxContents[1] + " / ";
                message += textBoxContents[0] + " - ";
            }
                if (a == 1)
            {                
                message += textBoxContents[0] + " " + textBoxContents[1] + " / ";
                message += textBoxContents[0] + " - ";
            }
            if (a == 2)
            {
                message += textBoxContents[0] + " " + textBoxContents[2] + " / ";
                message += textBoxContents[0]  +" " + textBoxContents[1] + " - ";
            }
            if (a == 3)
            {
                message += textBoxContents[0] + " " + textBoxContents[3] + " / ";
                message += textBoxContents[0] + " " + textBoxContents[1] + " " + textBoxContents[2] + " - ";
            }
            if (a == 4)
            {
                message += textBoxContents[0] + " " + textBoxContents[4] + " / ";
                message += textBoxContents[0] + " " + textBoxContents[1] + " " + textBoxContents[2] + " " + textBoxContents[3] + " - ";
            }
            if (a == 5)
            {
                message += textBoxContents[0] + " " + textBoxContents[5] + " / ";
                message += textBoxContents[0] + " " + textBoxContents[1] + " " + textBoxContents[2] + " " + textBoxContents[3] + " " + textBoxContents[4] + " - ";
            }
            if (a == 6)
            {
                message += textBoxContents[0] + " " + textBoxContents[6] + " / ";
                message += textBoxContents[0] + " " + textBoxContents[1] + " " + textBoxContents[2] + " " + textBoxContents[3] + " " + textBoxContents[4] + " " + textBoxContents[5] + " - ";
            }

            
            if (a + 1 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 1] + ", ";
                }
                else
                {
                    message += "нет данных, ";
                }

                
                if (a + 2 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 2] + ".- ";
                }
                else
                {
                    message += "нет данных, ";
                }

                
                if (a + 3 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 3] + " c.";
                }
                else
                {
                    message += "нет данных c.";
                }
            
            TextBox textBox = new TextBox();
            textBox.Location = new Point(200, 435);
            textBox.Font = new Font("Arial", 12);
            textBox.Multiline = true;
            textBox.Width = Math.Min(900, TextRenderer.MeasureText(message, textBox.Font).Width);
            textBox.Height = TextRenderer.MeasureText(message, textBox.Font, new Size(textBox.Width, int.MaxValue), TextFormatFlags.WordBreak).Height + 30;
            textBox.WordWrap = false;
            textBox.Text = message;
            this.Controls.Add(textBox);
        }
    }
}
                    
