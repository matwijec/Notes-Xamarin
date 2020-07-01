using System;
using Xamarin.Forms;
using Notes.Models;

namespace Notes
{
    public partial class NoteEntryPage : ContentPage
    {
        private const int V = 1;
        public int bold = 0;
        public int italic = 0;
        public int OrderedList = 0;
        public int UnorderedList = 0;
        public string changetext = "";
        public int Counter;
        public NoteEntryPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;
            await App.Database.SaveNoteAsync(note);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            await App.Database.DeleteNoteAsync(note);
            await Navigation.PopAsync();
        }


        async void EditorTextChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            var note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;
            await App.Database.SaveNoteAsync(note);
            if (newText.Length > 1)
            {
                if (newText.Substring(newText.Length - 1) == "\n" && UnorderedList % 2 == 1)
                {
                    editorarea.Text += "*";
                }
                if (newText.Substring(newText.Length - 1) == "\n" && OrderedList % 2 == 1)
                {
                    Counter += 1;
                    editorarea.Text += Counter + ". ";
                }
            }

        }

        async void MakeBold(object sender, EventArgs e)
        {
            if (bold % 2 == 0)
            {
                editorarea.FontAttributes = FontAttributes.Bold;
                bold += 1;
            }
            else
            {
                editorarea.FontAttributes = FontAttributes.None;
                bold += 1;
            }
        }

        async void MakeItalic(object sender, EventArgs e)
        {
            if (italic % 2 == 0)
            {
                editorarea.FontAttributes = FontAttributes.Italic;
                italic += 1;
            }
            else
            {
                editorarea.FontAttributes = FontAttributes.None;
                italic += 1;
            }
        }

        async void MakeIndentation(object sender, EventArgs e)
        {
            editorarea.Text += "                ";
        }

        async void MakeUnorderedList(object sender, EventArgs e)
        {

            UnorderedList += 1;

        }

        async void MakeOrderedList(object sender, EventArgs e)
        {
            OrderedList += 1;
            if (OrderedList % 2 == 0)
            {
                Counter = 0;
            }
        }
    }
}
