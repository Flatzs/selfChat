using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace selfChat
{
    public partial class Form1 : Form
    {

        chatBox chat = new chatBox();
        string defaultInputText = "Type a message...";
        RichTextBox newRichText;

        int NumNames = 0;
        Button[] nameSelector = new Button[100];

        string CurrentName = "No name selected";
        string lastAuthorPosted;
        bool isDeletingName = false;

        // flags
        bool isAddingNewName = false;
        bool isRightAligned = false;

        // class that handles loading and saving chat
        saveandload saveAndLoad = new saveandload();

        public Form1()
        {
            InitializeComponent();

            // iniatialize the chat.. load saved messages
            chat.init();


          

            DisplaySavedChat(chat.getMessages());

        //    if (NumNames == 0)
        //        richTextBox2.Text = "Hey man! I hope your day is going well. Here is a little on " +
        //                            "how to use this program.\n\n" +
        //                            "1.Create users at the bottom by clicking \"Add Name\"\n" +
        //                            "2.Click on the name that you would like to use (it will highlight blue)\n" +
        //                            "3.Type your message! (Enter key or send button will submit it to the chat)\n\n" +
        //                            "If you want to delete a name: Click the \"Delete Name\" button then click the name you would like to remove.\n\n" +
        //                            "This program saves and loads the messages when you open and close the program.\n" +
        //                            "There is a total reset button in the settings tab if you want to start fresh.\n";


        //    // scroll to bottom of chat window
        //    richTextBox2.SelectionStart = richTextBox2.TextLength;
        //    richTextBox2.ScrollToCaret();



        }

        void DisplaySavedChat(List<messageClass> messages)
        {
            foreach (messageClass m in messages) {
                //CurrentName = m.author;


                // create the name buttons from save
                bool temp = false;
                for (int i = 0; i < NumNames; i++)
                {
                    if (m.author == nameSelector[i].Text)
                    {
                        temp = true;
                        break;

                    }
    
                }
                if (temp == false)
                    CreateNewNameButton(m.author);





                // display message
                if (m.author != lastAuthorPosted)
                {
                    lastAuthorPosted = m.author;

                    // space between seperate user messages
                    richTextBox2.AppendText("\n");

                    // highlight 
                    int start = richTextBox2.TextLength;
                    richTextBox2.AppendText(m.author);
                    int end = richTextBox2.TextLength;

                    richTextBox2.Select(start, end - start);
                    {

                        richTextBox2.SelectionColor = Color.LightBlue;

                        if (isRightAligned)
                        {
                            richTextBox2.SelectionAlignment = HorizontalAlignment.Right;
                            isRightAligned = false;
                        }
                        else
                        {
                            richTextBox2.SelectionAlignment = HorizontalAlignment.Left;
                            isRightAligned = true;
                        }
                    }

                    richTextBox2.AppendText("\n");

                    richTextBox2.AppendText(m.text);






                }
                else { // same author.. no need to output author again

                    // new message on new line
                    richTextBox2.AppendText("\n");

                    richTextBox2.AppendText(m.text);
                }
            }
                 

        }


        // When user clicks "Send" button on message handler
        private void button1_Click(object sender, EventArgs e)
        {
            OnSendNewMessage(richTextBox1.Text.ToString());
            // scroll to bottom of chat window
            richTextBox2.SelectionStart = richTextBox2.TextLength;
            richTextBox2.ScrollToCaret();


        }

        void OnSendNewMessage(string text)
        {
            if (text == defaultInputText)
                return;


            // add to chat class
            chat.OnCreateMessage(text, CurrentName);

            // save chat to serialized file
            saveAndLoad.Save(chat.getMessages());


            if (chat.GetLastMessage_author() != lastAuthorPosted)
            {
                lastAuthorPosted = chat.GetLastMessage_author();

                // space between seperate user messages
                richTextBox2.AppendText("\n");

                // highlight 
                int start = richTextBox2.TextLength;
                richTextBox2.AppendText(chat.GetLastMessage_author());
                int end = richTextBox2.TextLength;

                richTextBox2.Select(start, end - start);
                {

                    richTextBox2.SelectionColor = Color.LightBlue;

                    if (isRightAligned)
                    {
                        richTextBox2.SelectionAlignment = HorizontalAlignment.Right;
                        isRightAligned = false;
                    }
                    else
                    {
                        richTextBox2.SelectionAlignment = HorizontalAlignment.Left;
                        isRightAligned = true;
                    }
                }

                richTextBox2.AppendText("\n");

                richTextBox2.AppendText(chat.GetLastMessage_text());


                richTextBox2.SelectionStart = richTextBox2.TextLength;
                richTextBox2.ScrollToCaret();





            }
            else { // same author.. no need to output author again

                // new message on new line
                richTextBox2.AppendText("\n");

                richTextBox2.AppendText(chat.GetLastMessage_text());
            }


            

            //richTextBox2.Text = chat.GetChatOutput();

            richTextBox1.Text = null;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string input = richTextBox1.Text.ToString();

            if (input.Length > 0 && input[input.Length - 1] == '\n')
           
            {
                input.Trim('\n');
                OnSendNewMessage(input);
                // scroll to bottom of chat window
                richTextBox2.SelectionStart = richTextBox2.TextLength;
                richTextBox2.ScrollToCaret();
            }
           
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            if (richTextBox1.Text == defaultInputText)
            {
                richTextBox1.Text = null;
                richTextBox1.ForeColor = Color.Black;

            }
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length == 0)
            {
                richTextBox1.Text = defaultInputText;
                richTextBox1.ForeColor = Color.DimGray;
            }
        }


        // New chat button
        private void button2_Click(object sender, EventArgs e)
        {

        }

        // New chat user button
        private void button3_Click(object sender, EventArgs e)
        {

            if (isAddingNewName == false)
            {
                // When user presses this, create a new user in the chat
                isAddingNewName = true;

                // temporary text box to enter name of new user
                newRichText = new RichTextBox();
                newRichText.Dock = DockStyle.Left;
                newRichText.TextChanged += TextBox_TextChanged;
                newRichText.Name = "nameText";
                panel2.Controls.Add(newRichText);

                newRichText.Select();
            }

        }


        // when user types in new chat user name
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            string input = newRichText.Text.ToString();

            if (input.Length > 0 && input[input.Length - 1] == '\n')
            {
                
                // pull the text from the textbox and apply to name
                string name = newRichText.Text;
                // get rid of the enter key press
                name = name.Trim('\n'); 

                // destroy the current text box
                newRichText.Dispose();

                CreateNewNameButton(name);
                
            }
        }


        void CreateNewNameButton(string name)
        {

            // add new user button in replace of this text box
            nameSelector[NumNames] = new Button();
            nameSelector[NumNames].Dock = DockStyle.Left;
            nameSelector[NumNames].Text = name;
            nameSelector[NumNames].Click += OnNameSelection;
            panel2.Controls.Add(nameSelector[NumNames]);
            nameSelector[NumNames].BackColor = Color.LightGray;
            nameSelector[NumNames].Select();
            NumNames++;
            isAddingNewName = false;

        }

        private void OnNameSelection(object sender, EventArgs e)
        {

            for (int i = 0; i < NumNames; i++)
            {
                nameSelector[i].BackColor = Color.LightGray;
            }

                for (int i = 0; i < NumNames; i++)
            {
                if (nameSelector[i].Equals(sender))
                {

                    if (isDeletingName == true)
                    {
                        nameSelector[i].Dispose();
                        isDeletingName = false;
                        return;
                    }

                    nameSelector[i].BackColor = Color.LightSkyBlue;
                    CurrentName = nameSelector[i].Text;
                    return;
                }
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }


        // delete name button
        private void button2_Click_2(object sender, EventArgs e)
        {

            if (!isDeletingName)
            {
                for (int i = 0; i < NumNames; i++)
                {
                    nameSelector[i].BackColor = Color.Red;
                }


                isDeletingName = true;
            }
            else {

                for (int i = 0; i < NumNames; i++)
                {
                    nameSelector[i].BackColor = Color.LightGray;
                }

                isDeletingName = false;

            }
               
        }

        // delete name button loss of focus
        private void button2_Leave(object sender, EventArgs e)
        {
            
        }

        // reset button clicked
        private void button4_Click(object sender, EventArgs e)
        {
            chat.DeleteAndSave();
            richTextBox2.Clear();

            for (int i = 0; i < NumNames; i++)
            {
                nameSelector[i].Dispose();
                
            }

            NumNames = 0;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
