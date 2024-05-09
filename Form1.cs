using System;
using System.IO; // Для читання та запису файлів
using System.Windows.Forms; // Для графічних компонентів користувацького інтерфейсу

namespace ЛР_19_Pract_editor_text_
{
    public partial class Form1 : Form
    {
        // Конструктор для Form1
        public Form1()
        {
            InitializeComponent(); // Ініціалізує компоненти форми
        }

        // Обробник події для пункту меню "Відкрити"
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(); // Показує діалог відкриття файлу
            if (openFileDialog1.FileName == null)
                return; // Вихід із методу, якщо файл не вибрано

            StreamReader MyReader = new StreamReader(openFileDialog1.FileName);
            // Читає текст з вибраного файлу та передає його в textBox
            textBox.Text = MyReader.ReadToEnd();
            MyReader.Close(); // Закриває StreamReader
        }

        // Обробник події для пункту меню "Зберегти як"
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = openFileDialog1.FileName; // Пропонує те саме ім'я файлу, яке було відкрито
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) // Показує діалог збереження файлу
                Save(); // Викликає метод Save, якщо користувач натисне "Зберегти"
        }

        // Метод для збереження тексту з textBox у файл
        void Save()
        {
            StreamWriter MyWriter = new StreamWriter(saveFileDialog1.FileName);
            // Записує текст із textBox у файл
            MyWriter.Write(textBox.Text);
            MyWriter.Close(); // Закриває StreamWriter
            textBox.Modified = false; // Скидає прапорець Modified у textBox
        }

        // Обробник події для пункту меню "Вийти"
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); // Закриває форму
        }

        // Обробник події при закритті форми
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBox.Modified == false)
                return; // Якщо зміни не були зроблені, дозволяє формі закритися

            // Відображає повідомлення з питанням, чи хоче користувач зберегти зміни
            string message = "Текст був змінений. Зберегти зміни?";
            string title = "Закрити вікно";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.No)
                return; // Якщо "Ні", закриває без збереження
            if (result == DialogResult.Cancel)
                e.Cancel = true; // Якщо "Скасувати", зупиняє закриття форми
            if (result == DialogResult.Yes)
            {
                // Якщо "Так", спроба зберегти
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Save(); // Зберігає файл
                    return;
                }
                else
                    e.Cancel = true; // Якщо користувач скасував під час збереження, не закривати форму
            }
        }
    }
}
