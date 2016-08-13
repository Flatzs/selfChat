using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selfChat
{
    public class chatBox
    {
        List<messageClass> messages = new List<messageClass>();

        string chatOutput = null;
        string lastAuthor;
        bool isEveryOtherAuthor = false;

        public void init() {
            saveandload saveAndLoad = new saveandload();

            messages = saveAndLoad.Load();

        }

        public List<messageClass> getMessages()
        {

            return messages;
        }
        
        // When user submits new message
        public void OnCreateMessage(string messageText, string author)
        {
            // create a new message
            messageClass message = new messageClass();

            messageText = messageText.Trim('\n');

            // populate the new message data
            message.text = messageText;
            message.author = author;
            lastAuthor = null;
            isEveryOtherAuthor = true;
          

            // add the new message to the list of messages
            messages.Add(message);

            // Add the last message to the chat box for the user
            // to see!


           

            // formats the chat output and displays to form
            //FormatChatOutput();
        }

        public string GetLastMessage_author()
        {

            return messages.Last<messageClass>().author;
        }
        public string GetLastMessage_text()
        {
            return messages.Last<messageClass>().text;
        }
        

        void FormatChatOutput()
        {
            // clear the formatted chat output
            chatOutput = null;

            // for each message in the chat
            for (int i = 0; i < messages.Count; i++)
            {

                // *~ format chat output here ~*

                if (messages[i].author != lastAuthor)
                {

                    if (isEveryOtherAuthor)
                        isEveryOtherAuthor = false;
                    else
                        isEveryOtherAuthor = true;

                    // add author text to the chat
                    if (isEveryOtherAuthor)
                        chatOutput += "   ";
                    chatOutput += messages[i].author;
                    chatOutput += "\n";
                    lastAuthor = messages[i].author;


                }


                // add message to the chat
                if (isEveryOtherAuthor)
                    chatOutput += "   ";

                chatOutput += messages[i].text;

                try {
                    // space between author's messages
                    if (messages.Count > 0 && messages[i + 1].author != lastAuthor)
                        chatOutput += "\n";

                }
                catch (Exception) {
                    //do nothing for now.. just dont break
                }

               
                
            }

           

        }


        public string GetChatOutput()
        {

            return chatOutput;
        }


        public void DeleteAndSave() {
            saveandload saveAndLoad = new saveandload();

            messages.Clear();
            saveAndLoad.Save(messages);
            
           
        }
       

       




    }
}
