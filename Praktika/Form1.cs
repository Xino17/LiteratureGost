using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        private int a;
        private ComboBox selectorComboBox;
        private Button startButton;


        public Form1()
        {
            InitializeSelectorComboBox();
            InitializeComponent();
            InitializeStartButton();
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
        private int k;
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
                    

                    if (a != 0 )
                    {
                        MessageBox.Show("Кол-во введенных авторов и произведений, должно быть таким же, каким вы ввели в численном эквиваленте. Заполнять нужно по порядку без ошибок. После заполнения кажого поля нужно нажать ENTER 1 раз. Нажмите на \"Преобразовать\", чтобы получить литературу в формате ГОСТа");
                        EnableOtherTextBoxes();
                        k = a;
                        for (int i = 0; i < k; i++)
                        {
                            CreateTextBox(220 + i * 125, 60);
                        }
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
            button.Location = new System.Drawing.Point(5, 600);
            button.Click += new EventHandler(Button_Click);
            this.Controls.Add(button);
        }



        private void Button_Click(object sender, EventArgs e)
           
        {
                string message = "";
            if (selectorComboBox.Text == "Книги/Монографии")
            {
                for (int i = 0; i <= a; i++)
                {
                    if (i == a)
                        message += textBoxContents[i] + ". - ";
                    else
                        message += textBoxContents[i] + ", ";
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
            }

            if (selectorComboBox.Text == "Статьи")
            {
                for (int i = 0; i <= a; i++)
                {
                    if (i == a)
                        message += textBoxContents[i] + ". - ";
                    else
                        message += textBoxContents[i] + ", ";
                }


                if (a + 1 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 1] + ".- ";
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
                    message +="N " + textBoxContents[a + 3] + ".- ";
                }
                else
                {
                    message += "нет данных c.";
                }

                if (a + 4 < textBoxContents.Count)
                {
                    message +="C. " + textBoxContents[a + 4] + " - ";
                }
                else
                {
                    message += "нет данных, ";
                }
                
                if (a + 5 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 5] + ". ";
                }
                else
                {
                    message += "нет данных, ";
                }
            }

            if (selectorComboBox.Text == "Патентная документация согласно стандарту ВОИС")
            {
                if (4 <= textBoxContents.Count)
                {
                    message += textBoxContents[0] + " " + textBoxContents[1] + " " + textBoxContents[2] + ", " + textBoxContents[3] + ".";
                }
                else
                {
                    message += "нет данных";
                }

            }

            if (selectorComboBox.Text == "Электронные ресурсы")
            {
                if  (3 <= textBoxContents.Count) 
                {
                    message += textBoxContents[0] + ". - " + "URL: " + textBoxContents[1] + " " + "(дата обращения " + textBoxContents[2] + ").";
                }
                else
                {
                    message += "нет данных";
                }
                
            }
            if (selectorComboBox.Text == "Нормативные документы")
            {
                if (4 <= textBoxContents.Count) 
                {
                    message += textBoxContents[0] + ". - "  + textBoxContents[1] + ", "  + textBoxContents[2] + ". - " + textBoxContents[3] + " с.";
                }
                else
                {
                    message += "нет данных";
                }
            }
            if (selectorComboBox.Text == "Нормативные документы (цифровые)")
            {
                if (3 <= textBoxContents.Count)
                {
                    message += textBoxContents[0] + ". - " + "URL: " + textBoxContents[1] + " " + "(дата обращения " + textBoxContents[2] + ").";
                }
                else
                {
                    message += "нет данных";
                }
            }
            if (selectorComboBox.Text == "Тезисы докладов, материалы конференций")
            {
                for (int i = 0; i <= a; i++)
                {
                    if (i == a)
                        message += textBoxContents[i] + ". - ";
                    else
                        message += textBoxContents[i] + ", ";
                }
                if (a + 1 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 1] + "// ";
                }
                else
                {
                    message += "нет данных";
                }

                if (a + 2 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 2] + ". ";
                }
                else
                {
                    message += "нет данных";
                }

                if (a + 3 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 3] + ". - ";
                }
                else
                {
                    message += "нет данных";
                }

                if (a + 4 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 4] + ", ";
                }
                else
                {
                    message += "нет данных";
                }

                if (a + 5 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 5] + ". -C. ";
                }
                else
                {
                    message += "нет данных";
                }

                if (a + 6 < textBoxContents.Count)
                {
                    message += textBoxContents[a + 6] + ".";
                }
                else
                {
                    message += "нет данных";
                }
            }

            TextBox textBox = new TextBox();
            textBox.Location = new Point(200, 600);
            textBox.Font = new Font("Arial", 12);
            textBox.Multiline = true;
            textBox.Size = new System.Drawing.Size(600, 150);
            textBox.WordWrap = false;
            textBox.Text = message;
            this.Controls.Add(textBox);




        }
        private void InitializeSelectorComboBox()
        {
            selectorComboBox = new ComboBox();
            selectorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            selectorComboBox.Items.AddRange(new object[]
            {
                "Статьи",
                "Книги/Монографии",
                "Нормативные документы",
                "Нормативные документы (цифровые)",
                "Патентная документация согласно стандарту ВОИС",
                "Электронные ресурсы",
                "Тезисы докладов, материалы конференций"
            });
            selectorComboBox.SelectedIndex = 0; 
            selectorComboBox.Location = new System.Drawing.Point(500, 15); 
            selectorComboBox.Size = new System.Drawing.Size(300, 50); 

            Controls.Add(selectorComboBox);
        }

        private void InitializeStartButton()
        {
            startButton = new Button();
            startButton.Text = "Старт";
            startButton.Location = new System.Drawing.Point(850, 15); 
            startButton.Size = new System.Drawing.Size(80, 30); 
            startButton.Click += StartButton_Click;
            Controls.Add(startButton);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            
            string selectedOption = selectorComboBox.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedOption))
            {
                
                switch (selectedOption)
                {
                    case "Статьи":
                        for(int i = 0; i < 7; i++)
                        {
                            CreateTextBox(220, 60 + i * 75);
                        }
                        CreateInputTextBox(300, 0);
                        CreateStaticTextBox(5, 60, "Автор");
                        CreateStaticTextBox(5, 135, "Название");
                        CreateStaticTextBox(5, 210, "Тип Статьи");
                        CreateStaticTextBox(5, 285, "Год выпуска");
                        CreateStaticTextBox(5, 0, "Кол-во авторов");
                        CreateStaticTextBox(5, 360, "Число страниц");
                        CreateStaticTextBox(5, 435, "N");
                        CreateStaticTextBox(5, 510, "C");
                        CreateButton();
                        break;
                    case "Книги/Монографии":
                        for(int i = 0; i < 5; i++)
                        {
                            CreateTextBox(220, 60 + i * 75);
                        }
                        CreateInputTextBox(300, 0);
                        CreateStaticTextBox(5, 0, "Кол-во авторов");
                        CreateStaticTextBox(5, 60, "Автор");
                        CreateStaticTextBox(5, 135, "Название");
                        CreateStaticTextBox(5, 210, "Город:Издание");
                        CreateStaticTextBox(5, 285, "Год выпуска");
                        CreateStaticTextBox(5, 360, "Число страниц");
                        CreateButton();
                        break;
                    case "Патентная документация согласно стандарту ВОИС":
                        MessageBox.Show("Заполнять нужно по порядку без ошибок. После заполнения кажого поля нужно нажать ENTER 1 раз. Нажмите на \"Преобразовать\", чтобы получить литературу в формате ГОСТа");
                        for (int i = 0; i < 4; i++)
                        {
                            CreateTextBox(220, 60 + i * 75);
                        }
                        CreateStaticTextBox(5, 60, "Код страны");
                        CreateStaticTextBox(5, 135, "N патента");
                        CreateStaticTextBox(5, 210, "Код вида");
                        CreateStaticTextBox(5, 285, "Год публикации");
                        CreateButton();
                        break;
                    case "Электронные ресурсы":
                        MessageBox.Show("Заполнять нужно по порядку без ошибок. После заполнения кажого поля нужно нажать ENTER 1 раз. Нажмите на \"Преобразовать\", чтобы получить литературу в формате ГОСТа");
                        for (int i = 0; i < 3; i++)
                        {
                            CreateTextBox(220, 60 + i * 75);
                        }
                        CreateStaticTextBox(5, 60, "Название");
                        CreateStaticTextBox(5, 135, "URL");
                        CreateStaticTextBox(5, 210, "Дата обращения");
                        CreateButton();
                        break;
                    case "Нормативные документы":
                        MessageBox.Show("Заполнять нужно по порядку без ошибок. После заполнения кажого поля нужно нажать ENTER 1 раз. Нажмите на \"Преобразовать\", чтобы получить литературу в формате ГОСТа");
                        for (int i = 0; i < 4; i++)
                        {
                            CreateTextBox(220, 60 + i * 75);
                        }
                        CreateStaticTextBox(5, 60, "Название");
                        CreateStaticTextBox(5, 135, "Город:Издание");
                        CreateStaticTextBox(5, 210, "Дата");
                        CreateStaticTextBox(5, 285, "Кол-во страниц");
                        CreateButton();
                        break;
                    case "Нормативные документы (цифровые)":
                        MessageBox.Show("Заполнять нужно по порядку без ошибок. После заполнения кажого поля нужно нажать ENTER 1 раз. Нажмите на \"Преобразовать\", чтобы получить литературу в формате ГОСТа");
                        for (int i = 0; i < 3; i++)
                        {
                            CreateTextBox(220, 60 + i * 75);
                        }
                        CreateStaticTextBox(5, 60, "Название");
                        CreateStaticTextBox(5, 135, "URL");
                        CreateStaticTextBox(5, 210, "Дата обращения");
                        CreateButton();
                        break;
                    case "Тезисы докладов, материалы конференций":
                        for (int i = 0; i < 8; i++)
                        {
                            CreateTextBox(220, 60 + i * 60);
                        }
                        CreateInputTextBox(300, 0);
                        CreateStaticTextBox(5, 0, "Кол-во авторов");
                        CreateStaticTextBox(5, 60, "Автор");
                        CreateStaticTextBox(5, 120, "Название");
                        CreateStaticTextBox(5, 180, "Доп. инф.");
                        CreateStaticTextBox(5, 240, "Конференция(место)");
                        CreateStaticTextBox(5, 300, "Том(часть)");
                        CreateStaticTextBox(5, 360, "Город:Издание");
                        CreateStaticTextBox(5, 420, "Дата");
                        CreateStaticTextBox(5, 480, "Страницы");
                        CreateButton();
                        break;
                }
            }
            startButton.Visible = false;
            selectorComboBox.Visible = false;
            InitializeResetButton();
        }

        private void InitializeResetButton()
        {
            Button resetButton = new Button();
            resetButton.Text = "Сбросить";
            resetButton.Location = new System.Drawing.Point(850, 15); 
            resetButton.Size = new System.Drawing.Size(80, 30); 
            resetButton.Click += ResetButton_Click;

            Controls.Add(resetButton);
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            textBoxContents.Clear();
            RemoveAllControls();
            InitializeSelectorComboBox();
            InitializeStartButton();
        }
        private void RemoveAllControls()
        {
            while (Controls.Count > 0)
            {
                Control control = Controls[0];
                Controls.Remove(control);
                control.Dispose();
            }
        }
    }
}
                    
