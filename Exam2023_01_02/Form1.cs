using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam2023_01_02
{
    public partial class Form1 : Form
    {
        private File file;
        class File
        {
            public string fileContent = string.Empty;
            public string filePath = string.Empty;
            public string fileSize = string.Empty;
            public string fileCountWord = string.Empty;
            public string fileCountChar = string.Empty;
        }
        public Form1()
        {
            InitializeComponent();
            file = new File();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                fd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; 
                fd.FilterIndex = 1;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    file.filePath = fd.FileName;

                    comboBox1.Items.Add(file.filePath);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            file.filePath = comboBox1.SelectedItem.ToString(); // 선택한 파일 경로 가져오기

            var fileStream = new FileStream(file.filePath, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                file.fileContent = reader.ReadToEnd();
            }

            textBox1.Clear();
            textBox1.AppendText(file.fileContent);

            // 파일 크기 계산
            FileInfo fileInfo = new FileInfo(file.filePath);
            file.fileSize = fileInfo.Length.ToString();

            // 단어 수 계산
            string[] words = file.fileContent.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            file.fileCountWord = words.Length.ToString();

            // 글자 수 계산
            file.fileCountChar = file.fileContent.Length.ToString();

            // 파일 메타데이터 표시
            label6.Text = file.fileSize + "byte";
            label7.Text = file.fileCountWord + "개";
            label8.Text = file.fileCountChar + "자";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox2.Text;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // 단어 검색
                string[] words = file.fileContent.Split(new[] { searchTerm }, StringSplitOptions.RemoveEmptyEntries);

                int wordCount = words.Length - 1;

                if (wordCount > 0)
                {
                    MessageBox.Show($"단어가 포함되어 있습니다. 개수: {wordCount}", "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("단어가 포함되어 있지 않습니다.", "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("검색어를 입력하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox2.Text;
            string replacementText = textBox3.Text;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                int index = file.fileContent.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase);
                if (index >= 0)
                {
                    string updatedContent = file.fileContent.Remove(index, searchTerm.Length).Insert(index, replacementText);
                    file.fileContent = updatedContent;
                    textBox1.Text = updatedContent;
                    MessageBox.Show("단어가 변경되었습니다.", "변경 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("단어가 포함되어 있지 않습니다.", "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("검색어를 입력하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox2.Text;
            string replacementText = textBox3.Text;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                string updatedContent = file.fileContent.Replace(searchTerm, replacementText);
                if (file.fileContent != updatedContent)
                {
                    file.fileContent = updatedContent;
                    textBox1.Text = updatedContent;
                    MessageBox.Show("단어가 변경되었습니다.", "변경 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("단어가 포함되어 있지 않습니다.", "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("검색어를 입력하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
