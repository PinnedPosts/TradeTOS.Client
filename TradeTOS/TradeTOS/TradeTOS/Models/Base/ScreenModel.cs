using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTOS.Models
{
    public class ScreenModel
    {
        public ScreenModel()
        {
            Labels = new List<LabelModel>();
            Inputs = new List<EntryModel>();
            PageTitles = new List<LabelModel>();
            Buttons = new List<ButtonModel>();

        }
        public string Id { get; set; }
        public string Title { get; set; }
        public List<LabelModel> Labels { get; set; }
        public List<EntryModel> Inputs { get; set; }
        public List<LabelModel> PageTitles { get; set; }
        public List<ButtonModel> Buttons { get; set; }
    }

    public class LabelModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string NavigateTo { get; set; }
    }

    public class ButtonModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class EntryModel
    {
        public string Id { get; set; }
        public string Placeholder { get; set; }
    }
}
