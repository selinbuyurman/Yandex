using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using Yandex.Translator;
using java.io;
using System.Text.RegularExpressions;
using Google.Cloud.Translation.V2;


namespace translate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cb_ChooseLanguage.Items.Add("en");
            cb_ChooseLanguage.Items.Add("tr");            
            
        }

        private void btn_OpenPDF_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF Files(*.pdf)|*.pdf";
            
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                string path = ofd.FileName.ToString();
                txtpdfname.Text = path;
            }
        }

        private void btn_Convert_Click(object sender, EventArgs e)
        {
            if (txtpdfname.Text=="")
            {
                MessageBox.Show("No PDF to display. Please, select PDF!");
            }
            else
            {
                PDDocument doc = PDDocument.load(txtpdfname.Text);
                PDFTextStripper stripper = new PDFTextStripper();

                string text = (stripper.getText(doc));
                text = Regex.Replace(text, @"[^a-zA-Z0-9^şŞıİçÇöÖüÜĞğâî\s]", string.Empty);
                text = text.Replace('â', 'a');
                text = text.Replace('î', 'i');
                txt_ShowPDF.Text = text;
            }
        }

        private void btn_txtsave_Click(object sender, EventArgs e)
        {
            if (txt_ShowTranslate.Text == "")
            {
                MessageBox.Show("No text to save!");
            }
            else
            {
                saveFileDialog1.DefaultExt = "*.pdf";
                saveFileDialog1.Filter = "PDF(*.pdf)|*.pdf";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFileDialog1.FileName.Length > 0)
                {
                    new pdfsharp().createPDF(saveFileDialog1.FileName, txt_ShowTranslate.Text);
                }
            }
            
        }

        private void btn_Translate_Click(object sender, EventArgs e)
        {
            if (txt_ShowPDF.Text == "")
            {
                if (cb_ChooseLanguage.SelectedItem == null)
                {
                    MessageBox.Show("Please Open PDF, Convert and Select Language!");
                }
                else
                {
                    MessageBox.Show("Please Open PDF and Convert!");
                }
            }
            else
            {
                if (cb_ChooseLanguage.SelectedItem == null)
                {
                    MessageBox.Show("Select language!");
                }
                else
                {
                    /* string credential_path = @"C:\Users\user\Desktop\selinbuyurman\translate\translate\translate\translate_api.json";
                    System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
                    System.Console.OutputEncoding = System.Text.Encoding.Unicode;
                    TranslationClient client = TranslationClient.Create();
                    var response = client.TranslateText( txt_ShowPDF.Text, cb_ChooseLanguage.SelectedItem.ToString());
                    System.Console.WriteLine(response.TranslatedText); */

                     IYandexTranslator translator = Yandex.Translator.Yandex.Translator(
                     api => api.ApiKey("trnsl.1.1.20171205T210927Z.1672e72d24851492.692b3b66cd77d62884c5e5ab6c2f51a0befa0e17")
                     .Format(ApiDataFormat.Json));
                     IEnumerable<ITranslationPair> translationPairs = translator.TranslationPairs();
                     ITranslation translation = translator.Translate(cb_ChooseLanguage.SelectedItem.ToString(), txt_ShowPDF.Text, "html");
                      txt_ShowTranslate.Text = translation.Text;
                }

            }
        }
       
    }
}
