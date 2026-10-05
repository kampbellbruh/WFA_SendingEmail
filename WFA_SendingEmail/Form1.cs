using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_SendingEmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            // Проверяем заполнение полей
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text) ||
                string.IsNullOrWhiteSpace(textBoxName.Text) ||
                string.IsNullOrWhiteSpace(textBoxSubject.Text) ||
                string.IsNullOrWhiteSpace(textBoxBody.Text))
            {
                MessageBox.Show("Заполни все поля!");
                return;
            }

            // Настройки официального защищенного сервера Mail.ru
            string smtpServer = "smtp.mail.ru";
            int smtpPort = 587; // Защищенный порт, который пропускают все провайдеры

            try
            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(smtpServer, smtpPort);
                client.EnableSsl = true; // Обязательно включаем шифрование Mail.ru
                client.UseDefaultCredentials = false;

                //моя старая мусорная почта 
                string myMail = "логин@mail.ru";
                string myAppPassword = "ВВЕДИТЕ_ПАРОЛЬ_ПРИЛОЖЕНИЯ";

                client.Credentials = new System.Net.NetworkCredential(myMail, myAppPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.From = new System.Net.Mail.MailAddress(myMail, "Лабораторная работа C#");
                message.To.Add(new System.Net.Mail.MailAddress(textBoxEmail.Text, textBoxName.Text));
                message.Subject = textBoxSubject.Text;

                // Автоматическая генерация даты и имени ПК 
                message.Body = $"Дата и время отправки: {DateTime.Now}\n" +
                               $"Название ПК: {System.Net.Dns.GetHostName()}\n\n" +
                               $"Текст сообщения:\n{textBoxBody.Text}";

                // Отправка
                client.Send(message);
                MessageBox.Show("Письмо успешно улетело!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Блин, ошибка: " + ex.Message);
            }
        }


        }
}    

